using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [Header("Wave settings")]
    [SerializeField]
    private float waveBaseDuration = 40f;
    [SerializeField]
    private float waveDurationStep = 5f;
    [SerializeField]
    private int maxWave = 20;

    [Header("Enemy settings")]
    [SerializeField]
    private EnemySpawner spawner;
    [SerializeField, Tooltip("For each wave decrease spawn interval by this value")]
    private float spawnFrequencyStep = 0.25f;

    [Header("Power settings")]
    [SerializeField]
    private List<AbstractPower> availablePowersList = new List<AbstractPower>();

    [Header("HUD")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI waveTimer;
    [SerializeField] private UIPowerChoice powerChoiceScreen;

    private int currentWave = 0;
    private float currentWaveRemainingTime;
    private bool isWaveActive = false;
    private bool isEndlessWave = false;

    // Events
    public delegate void OnWaveStart(int wave);
    public event OnWaveStart onWaveStart;
    public delegate void OnWaveEnd(int wave);
    public event OnWaveEnd onWaveEnd;

    // Start is called before the first frame update
    void Start()
    {
        currentWaveRemainingTime = waveBaseDuration;
        isWaveActive = true;

        if (!spawner)
        {
            Debug.LogError("Enemy spawner should be referenced");
            return;
        }

        // Register event for game over
        playerController.onDeath += OnDeath;
        // Register event for power selection
        powerChoiceScreen.onSelectPower += OnSelectPower;

        // For test purposes, immediately start first wave
        StartNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaveActive)
        {
            if (currentWaveRemainingTime > 0 || isEndlessWave)
            {
                if (isEndlessWave)
                {
                    currentWaveRemainingTime += Time.deltaTime;
                }
                else
                {
                    currentWaveRemainingTime -= Time.deltaTime;
                }

                UpdateWaveHud();
            }
            else
            {
                EndCurrentWave();
                ShowPowerChoice();
            }
        }
    }

    void StartNextWave(bool endless = false)
    {
        isEndlessWave = endless;

        if (currentWave + 1 > maxWave && !isEndlessWave)
        {
            Debug.LogError("Invalid next wave");
            return;
        }

        currentWave += 1;
        currentWaveRemainingTime = !isEndlessWave ? waveBaseDuration + (waveDurationStep * (currentWave - 1)) : 0;
        isWaveActive = true;

        // Configure spawner for new wave
        if (currentWave > 1)
        {
            spawner.spawnInterval -= spawnFrequencyStep;
        }
        spawner.StartSpawner();

        onWaveStart?.Invoke(currentWave);
    }

    void EndCurrentWave()
    {
        isWaveActive = false;
        onWaveEnd?.Invoke(currentWave);

        // Stop spawner and destroy all spawns at the end of the wave
        spawner.StopSpawner();
        spawner.ClearSpawns();

        // Clear all objects limited to game screen (ex. projectiles)
        var gameScreenObjects = GameObject.FindGameObjectsWithTag("GameScreenLimitedObject");
        foreach (var gameScreenObject in gameScreenObjects)
        {
            Destroy(gameScreenObject);
        }
    }

    void UpdateWaveHud()
    {
        if (isEndlessWave)
        {
            waveText.text = "Endless Wave";
        }
        else
        {
            waveText.text = string.Format("Wave {0}", currentWave);
        }

        var remainingSeconds = Mathf.Max(Mathf.CeilToInt(currentWaveRemainingTime), 0);
        waveTimer.text = string.Format("{0}", remainingSeconds);
        if (remainingSeconds <= 5)
        {
            waveTimer.color = Color.red;
        }
        else
        {
            waveTimer.color = Color.white;
        }
    }

    void ShowPowerChoice()
    {
        SetGamePaused(true);

        // Hide player
        playerController.gameObject.SetActive(false);

        // Pick-up two random powers
        int firstPowerIndex;
        int secondPowerIndex;

        if (availablePowersList.Count < 2)
        {
            Debug.Log("Not enough powers. Show endless wave screen.");
            uiManager.ToggleScreen("UI_EndlessWaveScreen", true);
            return;
        }
        else if (availablePowersList.Count == 2)
        {
            firstPowerIndex = 0;
            secondPowerIndex = 1;
        }
        else
        {
            do
            {
                firstPowerIndex = Random.Range(0, availablePowersList.Count);
                secondPowerIndex = Random.Range(0, availablePowersList.Count);
            } while (firstPowerIndex == secondPowerIndex);
        }

        AudioManager.instance.PlaySFX("showSelectPower");

        // Display PowerChoice screen
        uiManager.ToggleScreen("UI_PowerChoiceScreen", true);
        powerChoiceScreen.ShowPowerChoice(
            availablePowersList[firstPowerIndex],
            availablePowersList[secondPowerIndex]
        );
    }

    void OnSelectPower(AbstractPower selectedPower, AbstractPower omittedPower)
    {
        AudioManager.instance.PlaySFX("selectPower");

        // Add selected power to player
        playerController.AddPower(selectedPower);

        // Add omitted power to enemies
        var spawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
        foreach (var spawner in spawners)
        {
            spawner.GetComponent<EnemySpawner>().AddPower(omittedPower);
        }

        // Remove powers from available powers
        availablePowersList.Remove(selectedPower);
        availablePowersList.Remove(omittedPower);

        // Restore game screen and player
        uiManager.ToggleScreen("UI_GameScreen", true);
        playerController.gameObject.SetActive(true);

        // Clear power choices for next time
        powerChoiceScreen.ClearChoices();

        SetGamePaused(false);

        StartNextWave();
    }

    void SetGamePaused(bool isPaused)
    {
        Time.timeScale = isPaused ? 0 : 1;
    }

    void OnDeath()
    {
        SetGamePaused(true);
        EndCurrentWave();

        // Hide player
        playerController.gameObject.SetActive(false);

        ShowGameOverScreen();
    }

    public void ShowGameOverScreen()
    {
        uiManager.ShowGameOverScreen(
            currentWave,
            currentWaveRemainingTime,
            isEndlessWave,
            playerController.GetPowers(),
            spawner.GetPowers()
        );
    }

    public void ResetScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        GoToScene(scene.name);
    }

    public void GoToScene(string sceneName)
    {
        SetGamePaused(false);
        SceneManager.LoadScene(sceneName);
    }
    
    public void StartEndlessWave()
    {
        uiManager.ToggleScreen("UI_GameScreen", true);
        playerController.gameObject.SetActive(true);
        SetGamePaused(false);

        StartNextWave(true);
    }
}
