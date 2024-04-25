using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Indicator : MonoBehaviour
{
    [SerializeField] private GameObject indicatorObject;
    [SerializeField] private string targetTag;

    private SpriteRenderer spriteRenderer;
    private Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        var targetObject = GameObject.FindGameObjectWithTag(targetTag);

        if (!targetObject)
        {
            Debug.LogError("Cannot find target object");
            Destroy(gameObject);
            return;
        }

        targetTransform = targetObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spriteRenderer.isVisible)
        {
            if (!indicatorObject.activeSelf)
            {
                indicatorObject.SetActive(true);
            }

            var distance = Vector2.Distance(transform.position, targetTransform.position);
            Vector3 direction = new Vector2(targetTransform.position.x - transform.position.x, targetTransform.position.y - transform.position.y);

            var hit = Physics2D.Raycast(transform.position, direction, distance, LayerMask.GetMask("CameraBox"));
            if (hit.collider != null)
            {
                indicatorObject.transform.position = hit.point;
            }
        }
        else
        {
            if (indicatorObject.activeSelf)
            {
                indicatorObject.SetActive(false);
            }
        }
    }
}
