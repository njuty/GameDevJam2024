using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        // Rotate player to face mouse position
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }
}
