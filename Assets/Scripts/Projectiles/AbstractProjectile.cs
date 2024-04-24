using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectile : MonoBehaviour
{
    [Header("Base properties")]
    public float speed = 10f;
    public float lifeTime = 3f;

    [Header("Enemy variant")]
    [SerializeField] protected float enemySpeed = 5f;
    [SerializeField] protected float enemyLifeTime = 3f;

    public virtual void SetEnemyVariant()
    {
        speed = enemySpeed;
        lifeTime = enemyLifeTime;
    }
}
