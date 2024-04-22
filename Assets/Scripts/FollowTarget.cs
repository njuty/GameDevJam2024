using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float damping;

    Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 movePosition = target.position;

        //Fix z value to keep asset visible
        movePosition.z = -10f;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }
}
