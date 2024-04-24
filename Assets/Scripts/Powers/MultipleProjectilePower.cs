using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class ProjectileItem
{
    public GameObject projectilePrefab;
    public float angle = 0f;
}

public class MultipleProjectilePower : AbstractPower
{
    [Header("Power props")]
    [SerializeField] private List<ProjectileItem> projectilesList = new List<ProjectileItem>();

    private Transform shootPoint;

    protected override void Start()
    {
        base.Start();
        shootPoint = parentController.transform.Find("ShootPoint").GetComponent<Transform>();
    }

    public override void Activate()
    {
        if (!CanActivate()) return;

        foreach (var projectileItem in projectilesList)
        {
            // Get sprite size to correctly place the projectile in front of the player
            var spriteBounds = projectileItem.projectilePrefab.GetComponent<SpriteRenderer>().bounds;
            var projectile = Instantiate(
                projectileItem.projectilePrefab,
                shootPoint.position + (shootPoint.up * spriteBounds.size.y / 2),
                shootPoint.rotation * Quaternion.Euler(0f, 0f, projectileItem.angle)
            )
                .GetComponent<AbstractProjectile>();

            if (isEnemyPower)
            {
                // Apply enemy variant for projectiles
                projectile.SetEnemyVariant();
            }
        }
        
        // Set cooldown before next power use
        cooldown = activationRate;
    }

    public override bool CanActivate()
    {
        return cooldown <= 0;
    }
}
