using UnityEngine;

public abstract class AbstractPower : MonoBehaviour
{
    [Header("Power information")]
    public string powerName;
    public Texture2D powerIcon;

    [Header("Cooldown")]
    public float activationRate = 0.5f;
    public float cooldown = 0f;

    [Header("Enemy variant")]
    [SerializeField] private float enemyActivationRate = 0.5f;

    protected GameObject parentController;
    protected bool isEnemyPower = false;

    protected virtual void Start()
    {
        // Powers should always be placed in a "Powers" empty object
        // ex. Player/Powers/AwesomePower
        // ex. Enemy/Powers/AwesomePower
        parentController = GetControllerGameObject();
        if (isEnemyPower)
        {
            activationRate = enemyActivationRate;
        }
    }

    protected virtual void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    protected GameObject GetControllerGameObject()
    {
        var playerController = GetComponentInParent<PlayerController>();

        if (playerController)
        {
            return playerController.gameObject;
        }

        var enemyController = GetComponentInParent<EnemyController>();

        if (enemyController)
        {
            isEnemyPower = true;
            return enemyController.gameObject;
        }
        
        return gameObject.transform.parent.gameObject;
    }
    
    public abstract void Activate();
    public abstract bool CanActivate();
}
