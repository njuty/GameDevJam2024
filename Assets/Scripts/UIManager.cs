using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Game Over Screen")]
    [SerializeField] private TMP_Text waveResultText;
    [SerializeField] private UIPowersInventory unlockedPowersList;
    [SerializeField] private UIPowersInventory enemyPowersList;

    private string activeScreenTag = "UI_GameScreen";

    public void ToggleScreen(string screenTag, bool isActive)
    {
        if (screenTag != activeScreenTag)
        {
            // First hide previous screen
            ToggleScreen(activeScreenTag, false);
            activeScreenTag = screenTag;
        }

        var screenObjects = GameObject.FindGameObjectsWithTag(screenTag);
        foreach (var screenObject in screenObjects)
        {
            // Disable child items only
            foreach (Transform child in screenObject.transform)
            {
                child.gameObject.SetActive(isActive);
            }
        }
    }

    public void ShowGameOverScreen(
        int currentWave,
        float currentWaveRemainingTime,
        bool isEndlessWave,
        List<AbstractPower> powersList,
        List<AbstractPower> enemyPowers
    )
    {
        if (isEndlessWave)
        {
            var nbSeconds = Mathf.Max(Mathf.CeilToInt(currentWaveRemainingTime), 0);
            waveResultText.text = string.Format("You reached Endless Wave\n(and survived {0} seconds)", nbSeconds);
        }
        else
        {
            waveResultText.text = string.Format("You reached Wave {0}", currentWave);
        }

        unlockedPowersList.SetPowersList(powersList);
        enemyPowersList.SetPowersList(enemyPowers);

        ToggleScreen("UI_GameOverScreen", true);
    }
}
