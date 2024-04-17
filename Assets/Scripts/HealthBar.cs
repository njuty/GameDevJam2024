using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    private const float MAX_HEALTH = 100f;

    private float health;

    [SerializeField]
    Image healthBarImageToFill;

    [SerializeField]
    Gradient gradient;


    void Start()
    {
        health = MAX_HEALTH;
    }

    public void UpdateHealth(float amount)
    {
        float newAmount = health + amount;
        health = Mathf.Clamp(newAmount, 0, MAX_HEALTH);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float fillAmount = health / MAX_HEALTH;
        healthBarImageToFill.DOFillAmount(fillAmount, 0.5f);
        healthBarImageToFill.DOColor(gradient.Evaluate(fillAmount), 0.5f);
    }
}
