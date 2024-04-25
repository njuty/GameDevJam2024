using UnityEngine;

public class ShieldPower : AbstractPower
{
    [Header("Power props")]
    [SerializeField] private GameObject shieldPrefab;

    private bool isActiveShield = false;

    public override void Activate()
    {
        if (!CanActivate()) return;

        Instantiate(shieldPrefab, parentController.transform);
        
        // Set cooldown before next power use
        cooldown = activationRate;
        isActiveShield = true;
    }

    public override bool CanActivate()
    {
        return cooldown <= 0;
    }

    protected override void Update()
    {
        if (cooldown > 0 && !isActiveShield)
        {
            cooldown -= Time.deltaTime;
        }
    }
}
