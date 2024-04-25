using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{
    [Header("Base properties")]
    public float speed = 10f;
    public float lifeTime = 3f;
    public float damage = 10f;

    public bool destroyOnHit = true;

    //[HideInInspector]
    public GameObject launcher;

    [Header("Enemy variant")]
    [SerializeField] protected float enemySpeed = 5f;
    [SerializeField] protected float enemyLifeTime = 3f;
    [SerializeField] protected float enemyDamage = 5f;

    public virtual void SetEnemyVariant()
    {
        speed = enemySpeed;
        lifeTime = enemyLifeTime;
        damage = enemyDamage;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (launcher.CompareTag("Player"))
            {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                if (enemy)
                {
                    enemy.TakeDamage(damage);
                }
                if (destroyOnHit)
                {
                    Destroy(gameObject);
                }
            }

        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (launcher.CompareTag("Enemy"))
            {
                PlayerController player = collider.gameObject.GetComponent<PlayerController>();
                if (player)
                {
                    player.TakeDamage(damage);
                }
                if (destroyOnHit)
                {
                    Destroy(gameObject);
                }
            }

        }
       
    }
}
