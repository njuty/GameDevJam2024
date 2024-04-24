using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bomb : AbstractProjectile
{
    [Header("Projectile props")]
    [SerializeField] private float activationDelay = 1f;
    [SerializeField] private SpriteRenderer effectZoneSprite;

    private float createdFrom = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        createdFrom += Time.deltaTime;

        if (createdFrom > activationDelay)
        {
            // Activate the bomb, ready to explode
            effectZoneSprite.gameObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }
}
