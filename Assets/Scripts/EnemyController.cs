using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform playerTransform;

    public float speed = 2f;

    public float minDistance = 20f;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (playerTransform)
        {
            Vector3 direction = playerTransform.position - transform.position;
            if(direction.magnitude > .1f)
            {
                direction.Normalize();

                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
