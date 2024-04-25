using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleProjectilePower : AbstractPower
{
    [Header("Power props")]
    [SerializeField] private GameObject projectilePrefab;

    private Transform shootPoint;

    protected override void Start()
    {
        base.Start();
        shootPoint = parentController.transform.Find("ShootPoint").GetComponent<Transform>();
    }

    public override void Activate()
    {
        if (!CanActivate()) return;

        // Get sprite size to correctly place the projectile in front of the player
        var spriteBounds = projectilePrefab.GetComponent<SpriteRenderer>().bounds;
        var projectile = Instantiate(projectilePrefab, shootPoint.position + (shootPoint.up * spriteBounds.size.y / 2), shootPoint.rotation)
            .GetComponent<AbstractProjectile>();

        if (isEnemyPower)
        {
            // Apply enemy variant for projectiles
            projectile.SetEnemyVariant();
        }

        // Set cooldown before next power use
        cooldown = activationRate;
    }

    public override bool CanActivate()
    {
        return cooldown <= 0;
    }
}
