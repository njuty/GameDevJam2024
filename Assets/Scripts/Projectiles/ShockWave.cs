using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShockWave : AbstractProjectile
{
    [Header("Projectile props")]
    [SerializeField] private float scaleTo = 20f;

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        var sequence = DOTween.Sequence()
            .Append(transform.DOScale(scaleTo, lifeTime))
            .Append(sprite.DOFade(0f, lifeTime));

        sequence
            .OnComplete(OnAnimationComplete);
    }

    void OnAnimationComplete()
    {
        Destroy(gameObject);
    }
}
