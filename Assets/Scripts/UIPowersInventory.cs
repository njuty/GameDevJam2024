using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPowersInventory : MonoBehaviour
{
    [SerializeField] private GameObject prefabPowerItem;

    private Dictionary<AbstractPower, UIPowerItem> powerItems = new();
    private UIPowerItem activePowerItem;

    public void SetPowersList(AbstractPower[] powers)
    {
        foreach (var power in powers)
        {
            AddPowerToList(power);
        }
    }

    public void AddPowerToList(AbstractPower power)
    {
        var powerItem = Instantiate(prefabPowerItem, transform);
        powerItem.name = power.powerName;

        var uiPowerItem = powerItem.GetComponent<UIPowerItem>();
        uiPowerItem.Init(power);

        powerItems.Add(power, uiPowerItem);
    }

    public void SetActivePower(AbstractPower activePower)
    {
        // First disable previous active power item
        if (activePowerItem)
        {
            activePowerItem.SetActiveBorder(false);
        }

        // Then set new power item as active
        if (powerItems.TryGetValue(activePower, out var uiPowerItem))
        {
            uiPowerItem.SetActiveBorder(true);
            activePowerItem = uiPowerItem;
        }
        else
        {
            Debug.LogError(string.Format("Unknown power: {0}", activePower.name));
            return;
        }
    }
}
