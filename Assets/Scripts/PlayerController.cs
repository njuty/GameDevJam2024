using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly KeyCode[] alphaKeyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
    };

    [Header("UI")]
    [SerializeField] private UIPowersInventory powersInventory;

    private List<AbstractPower> powers = new List<AbstractPower>();
    private AbstractPower activePower;
    private int activePowerIndex;

    [Header("Player settings")]
    [SerializeField]
    private float speed = 2.5f;

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

        HandlePlayerInput();
    }

    void HandlePlayerInput()
    {
        ManagePosition();
        if (Input.GetMouseButton(0))
        {
            ActivatePower();
        }

        // Allow to choose active power with alphanumeric keys
        for (var i = 0; i < powers.Count; i++)
        {
            if (i >= alphaKeyCodes.Length) break;
            
            if (Input.GetKeyDown(alphaKeyCodes[i]))
            {
                SetActivePower(i);
            }
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

    void ManagePosition()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(x, y);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
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
}
