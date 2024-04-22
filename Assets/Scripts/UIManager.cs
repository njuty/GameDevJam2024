using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private string activeScreenTag;

    void Start()
    {
        // TODO: Later, fix with home screen tag
        activeScreenTag = "UI_GameScreen";
    }

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
}
