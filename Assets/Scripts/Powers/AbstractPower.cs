using UnityEngine;

public abstract class AbstractPower : MonoBehaviour
{
    [Header("Power information")]
    public string powerName;
    public Texture2D powerIcon;

    [Header("Cooldown")]
    public float activationRate = 0.5f;

    public float cooldown { get; protected set; } = 0f;

    protected void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }
    
    public abstract void Activate();
    public abstract bool CanActivate();
}
