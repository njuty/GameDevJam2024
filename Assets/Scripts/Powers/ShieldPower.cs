using UnityEngine;

public class ShieldPower : AbstractPower
{
    [Header("Power props")]
    [SerializeField] private GameObject shieldPrefab;

    private bool isActiveShield = false;

    public override void Activate()
    {
        if (!CanActivate()) return;

        // Set cooldown before next power use
        cooldown = activationRate;
        isActiveShield = true;

        var shieldObject = Instantiate(shieldPrefab, parentController.transform);
        var shield = shieldObject.GetComponent<Shield>();
        shield.launcher = parentController;

        if (isEnemyPower)
        {
            shield.SetEnemyVariant();
        }
        
        shield.Init();
        shield.onShieldDestroyed += OnShieldDestroyed;
    }

    public override bool CanActivate()
    {
        return cooldown <= 0 && !isActiveShield;
    }

    protected override void Update()
    {
        if (cooldown > 0 && !isActiveShield)
        {
            cooldown -= Time.deltaTime;
        }
    }

    void OnShieldDestroyed(Shield shield)
    {
        Destroy(shield.gameObject);
        isActiveShield = false;
    }
}
