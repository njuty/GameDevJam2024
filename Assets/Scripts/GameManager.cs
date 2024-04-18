using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Wave settings")]
    public float waveBaseDuration = 20f;
    public float waveDurationStep = 5f;
    public int maxWave = 20;

    [Header("HUD")]
    [SerializeField]
    TextMeshProUGUI waveText;
    [SerializeField]
    TextMeshProUGUI waveTimer;

    private int currentWave = 1;
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

                // Temp: later we will trigger wave from UI buttons
                StartNextWave();
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
        onWaveStart?.Invoke(currentWave);
    }

    void UpdateWaveHud()
    {
        waveText.text = string.Format("Wave {0}", currentWave);
        waveTimer.text = string.Format("{0}", Mathf.Max(Mathf.CeilToInt(currentWaveRemainingTime), 0));
    }
}
