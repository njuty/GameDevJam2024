using System.Collections;
using UnityEngine;

public class RandomPotionSpawner : MonoBehaviour
{
    [SerializeField]
    Potion healthPotion;

    public float spawnRadius = 30.0f;
    public float minTimeBetweenSpawns = 5.0f;
    public float maxTimeBetweenSpawns = 20.0f;

    // Probabilities associated with different potions
    public float chanceHealthPotion = 1f; // 65%

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
            // Uncomment when more than one potion is added
            //float rand = Random.value;
            Potion potionToSpawn = healthPotion;

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
