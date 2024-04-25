using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public delegate void OnDeath();
    public event OnDeath onDeath;

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

    Animator animator;

    [Header("Player settings")]
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float maxSpeed = 5f;

    private Rigidbody2D rb;

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
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

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
        if (Time.timeScale == 0) return;

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

        var movement = new Vector3(x, y, 0);

        rb.AddForce(movement * speed * Time.deltaTime, ForceMode2D.Impulse);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
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

    public void TakeDamage(float amount)
    {
        healthBar.UpdateHealth(-amount);
        animator.SetTrigger("isTouched");
        if (healthBar.health <= 0f)
        {
            onDeath();
        }
    }

    public List<AbstractPower> GetPowers()
    {
        return powers;
    }
}
