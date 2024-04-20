using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIPowersInventory powersInventory;

    private AbstractPower[] powers;
    private AbstractPower activePower;

    void Start()
    {
        powers = GetComponentsInChildren<AbstractPower>();
        if (powers.Length > 0)
        {
            powersInventory.SetPowersList(powers);
            SetActivePower(powers[0]);
        }
        else
        {
            Debug.LogWarning("No powers found");
        }
    }

    void Update()
    {
        // Rotate player to face mouse position
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;

        if (Input.GetMouseButton(0))
        {
            ActivatePower();
        }
    }

    void ActivatePower()
    {
        if (!activePower) return;
        activePower.Activate();
    }

    void SetActivePower(AbstractPower power)
    {
        activePower = power;

        // Update UI
        powersInventory.SetActivePower(activePower);
    }
}
