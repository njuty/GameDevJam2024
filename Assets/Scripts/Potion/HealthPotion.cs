using UnityEngine;

public class HealthPotion : Potion
{
    [SerializeField]
    float healthValue = 25f;

    HealthBar healthBar;

    protected override void Start()
    {
        base.Start();
        healthBar = GameObject.Find("UI Canvas").GetComponent<HealthBar>();
    }

    public override void ApplyEffect()
    {
        if (healthBar)
        {
            healthBar.UpdateHealth(healthValue);
        }
    }
}
