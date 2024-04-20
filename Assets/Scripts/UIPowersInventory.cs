using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPowersInventory : MonoBehaviour
{
    [SerializeField] private GameObject prefabPowerItem;

    private Dictionary<AbstractPower, GameObject> powerItems = new();
    private GameObject activePowerItem;

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
        powerItem.GetComponent<Image>().sprite = Sprite.Create(
            power.powerIcon,
            new Rect(0, 0, power.powerIcon.width, power.powerIcon.height),
            new Vector2(0.5f, 0.5f)
        );

        var textMesh = powerItem.transform.Find("PowerText").GetComponent<TMP_Text>();
        textMesh.text = power.powerName;

        powerItems.Add(power, powerItem);
    }

    public void SetActivePower(AbstractPower activePower)
    {
        // First disable previous active power item
        if (activePowerItem)
        {
            activePowerItem.transform.Find("PowerActiveBorder").gameObject.SetActive(false);
        }

        // Then set new power item as active
        if (powerItems.TryGetValue(activePower, out var powerItem))
        {
            powerItem.transform.Find("PowerActiveBorder").gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError(string.Format("Unknown power: {0}", activePower.name));
            return;
        }
    }
}
