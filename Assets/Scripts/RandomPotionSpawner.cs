using System.Collections;
using UnityEngine;

public class RandomPotionSpawner : MonoBehaviour
{
    [SerializeField]
    Potion healthPotion;

    [SerializeField]
    Potion invisibilityPotion;

    public float spawnRadius = 30.0f;
    public float minTimeBetweenSpawns = 5.0f;
    public float maxTimeBetweenSpawns = 20.0f;

    // Probabilities associated with different potions
    public float chanceHealthPotion = 0.65f; // 65%
    public float chanceInvisibilityPotion = 0.35f; // 35%

    private IEnumerator spawnCoroutine;

    void Start()
    {
        spawnCoroutine = SpawnPotion();
        StartCoroutine(spawnCoroutine);
    }

    public void RestartPotionSpawner()
    {
        StartCoroutine(spawnCoroutine);
    }

    public void StopPotionSpawner()
    {
        StopCoroutine(spawnCoroutine);
    }

    IEnumerator SpawnPotion()
    {
        yield return new WaitForSeconds(minTimeBetweenSpawns);

        while (true)
        {
            // Generate a random position within the spawn circle
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomPosition.x, randomPosition.y, 0) + transform.position;

            // Determine which potion to spawn
            float rand = Random.value;
            Potion potionToSpawn;

            if (rand < chanceHealthPotion)
            {
                potionToSpawn = healthPotion;
            }
            else
            {
                potionToSpawn = invisibilityPotion;
            }

            // Spawn the potion at the random position
            if (potionToSpawn != null)
            {
                Instantiate(potionToSpawn.gameObject, spawnPosition, Quaternion.identity);
            }

            float nextSpawnTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);

            yield return new WaitForSeconds(nextSpawnTime);
        }
       
    }
}
