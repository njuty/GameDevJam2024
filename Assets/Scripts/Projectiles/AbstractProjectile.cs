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

    [HideInInspector]
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

    protected virtual void OnCollisionEnter2D(Collision2D collision)
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
        else if (collision.gameObject.CompareTag("Player"))
        {
            bool isUseShield = collision.gameObject.transform.Find("Shield(Clone)");
            if (launcher.CompareTag("Enemy") && !isUseShield)
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
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

    protected virtual void OnTriggerEnter2D(Collider2D collider)
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
        else if (collider.gameObject.CompareTag("Enemy"))
        {
            if (launcher.CompareTag("Player"))
            {
                EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();
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
}
