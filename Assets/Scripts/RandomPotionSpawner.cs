using UnityEngine;

public class RandomPotionSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject healthPotion;

    [SerializeField]
    GameObject invisibilityPotion;

    [SerializeField]
    GameObject cooldownResetPotion;

    public float spawnRadius = 30.0f;
    public float minTimeBetweenSpawns = 5.0f;
    public float maxTimeBetweenSpawns = 20.0f;

    // Probabilities associated with different potions
    public float chanceHealthPotion = 0.6f; // 60%
    public float chanceInvisibilityPotion = 0.3f; // 30%
    public float chanceCooldownResetPotion = 0.1f; // 10%

    void Start()
    {
        Invoke(nameof(SpawnPotion), minTimeBetweenSpawns);
    }

    void SpawnPotion()
    {
        // Generate a random position within the spawn circle
        Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomPosition.x, randomPosition.y, 0) + transform.position;

        // Determine which potion to spawn
        float rand = Random.value;
        GameObject potionToSpawn = null;

        if (rand < chanceHealthPotion)
        {
            potionToSpawn = healthPotion;
        }
        else if (rand < chanceHealthPotion + chanceInvisibilityPotion)
        {
            potionToSpawn = invisibilityPotion;
        }
        else
        {
            potionToSpawn = cooldownResetPotion;
        }

        // Spawn the potion at the random position
        if (potionToSpawn != null)
        {
            Instantiate(potionToSpawn, spawnPosition, Quaternion.identity);
        }

        float nextSpawnTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
        Invoke(nameof(SpawnPotion), nextSpawnTime);
    }
}
