using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag.Equals("Player"))
        {
            animator.SetTrigger("isCatch");
            var item = collision.gameObject;

            Debug.Log(item);
        }
    }

    void ConsumeItem()
    {
        Destroy(gameObject);
    }
}
