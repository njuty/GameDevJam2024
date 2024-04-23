using DG.Tweening;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.5f;
    [SerializeField] private float damage = 30f;


    // Start is called before the first frame update
    void Start()
    {
        var sequence = DOTween.Sequence()
            .Append(transform.DOScaleX(1f, lifeTime / 2));

        sequence
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(OnAnimationComplete);
    }

    private void OnAnimationComplete()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage(damage);
        }
    }
}
