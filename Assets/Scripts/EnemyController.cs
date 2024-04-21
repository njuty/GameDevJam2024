using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform playerTransform;

    public float speed = 1f;

    private List<AbstractPower> powers = new List<AbstractPower>();
    private AbstractPower activePower;
    private int activePowerIndex;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        if (!playerTransform)
        {
            Debug.LogError("Unable to find player");
        }

        GetComponentsInChildren(powers);
        if (powers.Count > 0)
        {
            SetActivePower(0);
        }
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
        if (!activePower) return;
        activePower.Activate();
    }

    void SetActivePower(int powerIndex)
    {
        if (powers.Count < powerIndex)
        {
            Debug.LogError("Invalid power index");
            return;
        }

        activePowerIndex = powerIndex;
        activePower = powers[powerIndex];
    }
}
