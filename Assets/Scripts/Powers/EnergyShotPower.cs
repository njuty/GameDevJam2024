using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShotPower : AbstractPower
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float activationRate = 0.5f;

    private Transform shootPoint;
    private float cooldown = 0f;

    void Start()
    {
        shootPoint = GameObject.Find("/Player/ShootPoint").GetComponent<Transform>();
    }

    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    public override void Activate()
    {
        if (cooldown > 0) return;

        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Set cooldown before next power use
        cooldown = activationRate;
    }
}
