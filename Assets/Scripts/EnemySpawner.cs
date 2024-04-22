using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public float spawnInterval = 2f;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<AbstractPower> enemyPowers = new List<AbstractPower>();

    private Camera mainCamera;
    private IEnumerator spawnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    public void StartSpawner()
    {
        if (spawnCoroutine != null)
        {
            Debug.Log("Spawner is already running");
            return;
        }
        spawnCoroutine = SpawnEntities();
        StartCoroutine(spawnCoroutine);
    }

    public void StopSpawner()
    {
        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }

    public void ClearSpawns()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void AddPower(AbstractPower power)
    {
        if (enemyPowers.Exists(p => p.powerName == power.powerName))
        {
            Debug.Log("Power is already assigned for this spawner");
            return;
        }

        enemyPowers.Add(power);
    }

    IEnumerator SpawnEntities()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            Vector2 mainCameraPosition = Camera.main.transform.position;

            float farClipPlane = mainCamera.farClipPlane;

            // Random angle from 0° to 360° (2pi radian = 360°)
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);

            // farClipPlane with multiplier permit to take into consideration camera settings
            float x = mainCameraPosition.x + Mathf.Cos(randomAngle) * (farClipPlane * 0.06f);
            float y = mainCameraPosition.y + Mathf.Sin(randomAngle) * (farClipPlane * 0.06f);

            Vector2 spawnPosition = new Vector2(x, y);
            var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            var enemyController = enemy.GetComponent<EnemyController>();

            foreach (var power in enemyPowers)
            {
                enemyController.AddPower(power);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
