using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sequence = DOTween.Sequence()
            .Append(transform.DOMoveY(transform.position.y - 0.5f, 5f / 2))
            .Append(transform.DOMoveY(transform.position.y + 0.5f, 5f / 2));

        sequence
            .SetLoops(-1, LoopType.Yoyo);
    }
}
