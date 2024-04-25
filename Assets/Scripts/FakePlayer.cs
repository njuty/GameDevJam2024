using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FakePlayer : MonoBehaviour
{
    [SerializeField] private List<GameObject> checkpoints = new List<GameObject>();
    [SerializeField] private float speedMultiplier = 10f;
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private AbstractPower power;

    private Rigidbody2D rb;
    private int currentCheckpointIndex = 0;
    private float timeBeforePower = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var nextPosition = checkpoints[currentCheckpointIndex].transform.position;
        var direction = new Vector2(nextPosition.x - transform.position.x, nextPosition.y - transform.position.y);
        direction.Normalize();

        // Rotate and move fake player
        transform.up = direction;

        rb.AddForce(speedMultiplier * Time.deltaTime * direction, ForceMode2D.Impulse);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        // Validate checkpoints
        var distance = Vector2.Distance(nextPosition, transform.position);
        if (distance <= 1f)
        {
            currentCheckpointIndex++;
        }

        if (currentCheckpointIndex >= checkpoints.Count)
        {
            currentCheckpointIndex = 0;
        }

        if (power)
        {
            // Wait x seconds before using power
            if (timeBeforePower > 0)
            {
                timeBeforePower -= Time.deltaTime;
            }
            else
            {
                // Activate power
                power.Activate();
            }
        }

    }
}
