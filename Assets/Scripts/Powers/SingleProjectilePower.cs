using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleProjectilePower : AbstractPower
{
    [SerializeField] private GameObject projectilePrefab;

    private Transform shootPoint;

    void Start()
    {
        shootPoint = GameObject.Find("/Player/ShootPoint").GetComponent<Transform>();
    }

    public override void Activate()
    {
        if (cooldown > 0) return;

        // Get sprite size to correctly place the projectile in front of the player
        var spriteBounds = projectilePrefab.GetComponent<SpriteRenderer>().bounds;
        Instantiate(projectilePrefab, shootPoint.position + (shootPoint.up * spriteBounds.size.y / 2), shootPoint.rotation);

        // Set cooldown before next power use
        cooldown = activationRate;
    }
}
