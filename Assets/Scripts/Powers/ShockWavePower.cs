using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWavePower : AbstractPower
{
    [Header("Power props")]
    [SerializeField] private GameObject wavePrefab;

    public override void Activate()
    {
        if (!CanActivate()) return;

        Instantiate(wavePrefab, parentController.transform);

        // Set cooldown before next power use
        cooldown = activationRate;
    }

    public override bool CanActivate()
    {
        return cooldown <= 0;
    }
}
