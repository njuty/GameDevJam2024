using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;

    public float health;

    [SerializeField]
    Image healthBarImageToFill;

    [SerializeField]
    Gradient gradient;


    void Start()
    {
        health = maxHealth;
        UpdateHealthBar();
    }

    public void UpdateHealth(float amount)
    {
        float newAmount = health + amount;
        health = Mathf.Clamp(newAmount, 0, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float fillAmount = health / maxHealth;
        healthBarImageToFill.DOFillAmount(fillAmount, 0.5f);
        healthBarImageToFill.DOColor(gradient.Evaluate(fillAmount), 0.5f);
    }
}
