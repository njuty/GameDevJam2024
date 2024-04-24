using UnityEngine;
using UnityEngine.UI;

public class UIPowerItem : MonoBehaviour
{
    [SerializeField] private Image cooldownImage;
    [SerializeField] private GameObject activeBorderObject;
    [SerializeField] private Image powerImage;

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
        linkedPower = power;

        powerImage.sprite = Sprite.Create(
            power.powerIcon,
            new Rect(0, 0, power.powerIcon.width, power.powerIcon.height),
            new Vector2(0.5f, 0.5f)
        );
    }

    public void SetActiveBorder(bool isActive)
    {
        activeBorderObject.SetActive(isActive);
    }
}
