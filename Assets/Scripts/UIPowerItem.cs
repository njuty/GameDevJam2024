using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIPowerItem : MonoBehaviour
{
    [SerializeField] private TMP_Text powerText;
    [SerializeField] private Image cooldownImage;
    [SerializeField] private GameObject activeBorderObject;

    private Image powerImage;
    private AbstractPower linkedPower;

    void Update()
    {
        if (!linkedPower) return;

        if (linkedPower.cooldown > 0)
        {
            cooldownImage.fillAmount = linkedPower.cooldown / linkedPower.activationRate;
        }
        else
        {
            // Be sure that if there is no cooldown image is hidden
            cooldownImage.fillAmount = 0;
        }
    }

    public void Init(AbstractPower power)
    {
        if (!powerImage)
        {
            powerImage = GetComponent<Image>();
        }

        linkedPower = power;

        powerImage.sprite = Sprite.Create(
            power.powerIcon,
            new Rect(0, 0, power.powerIcon.width, power.powerIcon.height),
            new Vector2(0.5f, 0.5f)
        );

        powerText.text = power.powerName;
    }

    public void SetActiveBorder(bool isActive)
    {
        activeBorderObject.SetActive(isActive);
    }
}
