using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float MAX_HEALTH_POINT = 50f;

    private Transform playerTransform;

    public float speed = 1f;

    [SerializeField]
    private float healthPoint;

    Animator animator;

    private List<AbstractPower> powers = new List<AbstractPower>();

    public void AddPower(AbstractPower power)
    {
        var newPower = Instantiate(power.gameObject, transform.Find("Powers")).GetComponent<AbstractPower>();

        powers.Add(newPower);
    }

    void Start()
    {
        healthPoint = MAX_HEALTH_POINT;
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        if (!playerTransform)
        {
            Debug.LogError("Unable to find player");
        }

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!playerTransform) return;

        // Get direction to player
        Vector3 direction = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.y - transform.position.y);

        // Rotate player to face mouse position
        transform.up = direction;

        // Move enemy to player
        if (direction.magnitude > .1f)
        {
            direction.Normalize();
            transform.position += direction * speed * Time.deltaTime;
        }

        ActivatePower();
    }


    void ActivatePower()
    {
        // Search for the first available power
        foreach (var power in powers)
        {
            if (power.CanActivate())
            {
                power.Activate();
                break;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        float newHPValue = healthPoint - amount;
        if (newHPValue <= 0)
        {
            animator.SetBool("isDead", true);
        } else
        {
            animator.SetTrigger("isTouched");
            animator.ResetTrigger("isTouched");
        }
        healthPoint = newHPValue;
    }

    void onDeathAnimationComplete()
    {
        Destroy(gameObject);
    }

}
