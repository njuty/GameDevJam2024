using UnityEngine;

public class Shield : AbstractProjectile
{
    HealthBar shieldHealthBar;


    private GameObject shieldBar;


    void Start()
    {
        GameObject shieldBarParent = GameObject.Find("ShieldBarParent");
        shieldBar = shieldBarParent.transform.GetChild(0).gameObject;
        shieldBar.SetActive(true);
        shieldHealthBar = shieldBar.GetComponent<HealthBar>();
        Destroy(gameObject, lifeTime);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        bool isEnnemy = collisionObject.CompareTag("Enemy");
        if (isEnnemy || collisionObject.CompareTag("PowerEffect"))
        {
            var damage = 0f;
            if (isEnnemy) {
                EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                if (enemy)
                {
                    damage = enemy.physicalDamage;
                }
            }
            else {
                AbstractProjectile abstractProjectile = collision.gameObject.GetComponent<AbstractProjectile>();
                if (abstractProjectile)
                {
                    damage = abstractProjectile.damage;
                }
                Destroy(abstractProjectile);
            }

            if(shieldHealthBar.health - damage <= 0f)
            {
                Destroy(gameObject);
            } else
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
