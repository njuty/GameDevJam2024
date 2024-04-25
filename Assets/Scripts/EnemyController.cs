using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform playerTransform;

    public float speed = 1f;
    public float maxDistance = 20f;
    public float minDistanceForPower = 10f;

    private List<AbstractPower> powers = new List<AbstractPower>();

    public void AddPower(AbstractPower power)
    {
        var newPower = Instantiate(power.gameObject, transform.Find("Powers")).GetComponent<AbstractPower>();

        powers.Add(newPower);
    }

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        if (!playerTransform)
        {
            Debug.LogError("Unable to find player");
        }
    }

    void Update()
    {
        if (!playerTransform) return;

        var distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > maxDistance)
        {   
            // Kill enemy if too far away from player
            Destroy(gameObject);
            return;
        }

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

        if (distanceToPlayer < minDistanceForPower)
        {
            ActivatePower();
        }
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
}
