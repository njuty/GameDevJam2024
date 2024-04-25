using DG.Tweening;

public class Laser : AbstractProjectile
{
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
}
