using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Potion : MonoBehaviour
{
    [SerializeField]
    float ttlPotion = 10f;

    float restOfTimeAlive = -1;

    Animator animator;

    protected virtual void Start()
    {
        restOfTimeAlive = ttlPotion;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (restOfTimeAlive > -1)
        {
            if (restOfTimeAlive > 0)
            {
                restOfTimeAlive -= Time.deltaTime;
            } else
            {
                SetDisappearanceAnimation();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            ApplyEffect();
            SetCatchAnimation();
        }
    }

    void DestroyPotion()
    {
        Destroy(gameObject);
    }

    void SetCatchAnimation()
    {
        animator.SetBool("isCatch", true);
    }

    void SetDisappearanceAnimation()
    {
        animator.SetBool("mustDisappear", true);
    }

    public abstract void ApplyEffect();
}
