using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIPowerChoice : MonoBehaviour
{
    [SerializeField] private GameObject powerCardPrefab;

    public delegate void OnSelectPower(AbstractPower selectedPower, AbstractPower omittedPower);
    public event OnSelectPower onSelectPower;

    public void ShowPowerChoice(AbstractPower firstPower, AbstractPower secondPower)
    {
        AddPowerCard(firstPower, secondPower);
        AddPowerCard(secondPower, firstPower);

        gameObject.SetActive(true);
    }

    public void ClearChoices()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void AddPowerCard(AbstractPower power, AbstractPower omittedPower)
    {
        var powerCard = Instantiate(powerCardPrefab, transform);
        var powerImage = powerCard.transform.Find("PowerImage").GetComponent<RawImage>();
        var powerName = powerCard.transform.Find("PowerName").GetComponent<TMP_Text>();
        var powerSelectButton = powerCard.GetComponentInChildren<Button>();

        powerImage.texture = power.powerIcon;
        powerName.text = power.powerName;
        powerSelectButton.onClick.AddListener(() => {
            onSelectPower(power, omittedPower);
        });
    }
}
