using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIPowersInventory powersInventory;

    private List<AbstractPower> powers = new List<AbstractPower>();
    private AbstractPower activePower;
    private int activePowerIndex;

    void Start()
    {
        GetComponentsInChildren(powers);
        if (powers.Count > 0)
        {
            powersInventory.SetPowersList(powers);
            SetActivePower(0);
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

        // Player actions

        if (Input.GetMouseButton(0))
        {
            ActivatePower();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            // Activate previous power
            SetActivePower(activePowerIndex - 1 >= 0 ? activePowerIndex - 1 : powers.Count - 1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            // Activate next power
            SetActivePower(activePowerIndex + 1 < powers.Count ? activePowerIndex + 1 : 0);
        }
    }

    void ActivatePower()
    {
        if (!activePower) return;
        activePower.Activate();
    }

    void SetActivePower(int powerIndex)
    {
        if (powers.Count < powerIndex)
        {
            Debug.LogError("Invalid power index");
            return;
        }

        activePowerIndex = powerIndex;
        activePower = powers[powerIndex];

        // Update UI
        powersInventory.SetActivePower(activePower);
    }

    public void AddPower(AbstractPower power)
    {        
        if (powers.Exists(p => p.powerName == power.powerName))
        {
            Debug.Log("Power is already unlocked for the player");
            return;
        }

        var newPower = Instantiate(power.gameObject, transform.Find("Powers")).GetComponent<AbstractPower>();
        
        powers.Add(newPower);
        powersInventory.AddPowerToList(newPower);
    }
}
