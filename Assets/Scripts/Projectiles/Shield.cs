using UnityEngine;

public class Shield : AbstractProjectile
{
    private HealthBar shieldHealthBar;
    private GameObject shieldBar;
    private bool isDestroyed = false;

    // Events
    public delegate void OnShieldDestroyed(Shield shield);
    public event OnShieldDestroyed onShieldDestroyed;

    public void Init()
    {
        GameObject shieldBarParent;

        Debug.Log(isEnemyVariant);
        if (!isEnemyVariant)
        {
            shieldBarParent = GameObject.Find("ShieldBarParent");
            
        }
        else
        {
            shieldBarParent = transform.parent.Find("Canvas/ShieldBarParent").gameObject;
        }

        if (shieldBarParent)
        {
            shieldBar = shieldBarParent.transform.GetChild(0).gameObject;
            shieldBar.SetActive(true);
            shieldHealthBar = shieldBar.GetComponent<HealthBar>();
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroyed) return;

        GameObject collisionObject = collision.gameObject;
        bool isEnemy = collisionObject.CompareTag("Enemy");
        if (isEnemy || collisionObject.CompareTag("PowerEffect"))
        {
            var damage = 0f;
            if (isEnemy)
            {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                if (enemy)
                {
                    damage = enemy.physicalDamage;
                }
            }
            else
            {
                AbstractProjectile abstractProjectile = collision.gameObject.GetComponent<AbstractProjectile>();
                if (abstractProjectile)
                {
                    damage = abstractProjectile.damage;
                }
                Destroy(abstractProjectile);
            }

            if (shieldHealthBar.health - damage <= 0f)
            {
                isDestroyed = true;
                onShieldDestroyed(this);
            }
            else
            {
                shieldHealthBar.UpdateHealth(-damage);
            }
        }
    }

    void OnDestroy()
    {
        shieldBar.SetActive(false);
    }
}
