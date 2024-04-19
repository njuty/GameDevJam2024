using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Wave settings")]
    [SerializeField]
    private float waveBaseDuration = 20f;
    [SerializeField]
    private float waveDurationStep = 5f;
    [SerializeField]
    private int maxWave = 20;

    [Header("Enemy settings")]
    [SerializeField]
    private EnemySpawner spawner;
    [SerializeField, Tooltip("For each wave decrease spawn interval by this value")]
    private float spawnFrequencyStep = 0.25f;

    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI waveTimer;
    [SerializeField] private Texture2D waveCursorTexture;

    private int currentWave = 0;
    private float currentWaveRemainingTime;
    private bool isWaveActive = false;

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

        // For test purposes, immediately start first wave
        StartNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaveActive)
        {
            if (currentWaveRemainingTime > 0)
            {
                currentWaveRemainingTime -= Time.deltaTime;
                UpdateWaveHud();
            }
            else
            {
                isWaveActive = false;
                onWaveEnd?.Invoke(currentWave);

                // Restore default cursor
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

                // Temp: later we will trigger wave from UI buttons
                StartNextWave();

                // TODO: Kill all spawns at the end of the wave
                // spawner.StopSpawner();
            }
        }
    }

    void StartNextWave()
    {
        if (currentWave + 1 > maxWave)
        {
            Debug.LogError("Invalid next wave");
            return;
        }

        currentWave += 1;
        currentWaveRemainingTime = waveBaseDuration + (waveDurationStep * (currentWave - 1));
        isWaveActive = true;

        // Configure UI
        var cursorOffset = new Vector2(waveCursorTexture.width / 2, waveCursorTexture.height / 2);
        Cursor.SetCursor(waveCursorTexture, cursorOffset, CursorMode.Auto);

        // Configure spawner for new wave
        if (currentWave > 1)
        {
            spawner.spawnInterval -= spawnFrequencyStep;
        }
        spawner.StartSpawner();

        onWaveStart?.Invoke(currentWave);
    }

    void UpdateWaveHud()
    {
        waveText.text = string.Format("Wave {0}", currentWave);

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
}
