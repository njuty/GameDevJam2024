using UnityEngine;

public class ShockWavePower : AbstractPower
{
    [Header("Power props")]
    [SerializeField] private GameObject wavePrefab;

    public override void Activate()
    {
        if (!CanActivate()) return;

        var projectile = Instantiate(wavePrefab, parentController.transform).GetComponent<AbstractProjectile>();
        projectile.launcher = parentController;

        // Set cooldown before next power use
        cooldown = activationRate;
    }

    public override bool CanActivate()
    {
        return cooldown <= 0;
    }
}
