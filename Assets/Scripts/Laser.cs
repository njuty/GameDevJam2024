using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.5f;

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
