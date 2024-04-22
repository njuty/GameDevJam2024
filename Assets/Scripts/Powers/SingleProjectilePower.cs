using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleProjectilePower : AbstractPower
{
    [SerializeField] private GameObject projectilePrefab;

    private Transform shootPoint;

    void Start()
    {
        // Powers should always be placed in a "Powers" empty object
        // ex. Player/Powers/AwesomePower
        // ex. Enemy/Powers/AwesomePower
        var parent = GetControllerGameObject();
        shootPoint = parent.transform.Find("ShootPoint").GetComponent<Transform>();
    }

    public override void Activate()
    {
        if (!CanActivate()) return;

        // Get sprite size to correctly place the projectile in front of the player
        var spriteBounds = projectilePrefab.GetComponent<SpriteRenderer>().bounds;
        Instantiate(projectilePrefab, shootPoint.position + (shootPoint.up * spriteBounds.size.y / 2), shootPoint.rotation);

        // Set cooldown before next power use
        cooldown = activationRate;
    }

    public override bool CanActivate()
    {
        return cooldown <= 0;
    }

    GameObject GetControllerGameObject()
    {
        var playerController = GetComponentInParent<PlayerController>();

        if (playerController)
        {
            return playerController.gameObject;
        }

        return GetComponentInParent<EnemyController>().gameObject;
    }
}
