using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    private Camera mainCamera;

    public float spawnInterval = 2f;

    [SerializeField]
    private GameObject enemyPrefab;

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
    }

    IEnumerator SpawnEntities()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            Vector2 mainCameraPosition = mainCamera.transform.position;

            float farClipPlane = mainCamera.farClipPlane;

            // Random angle from 0° to 360° (2pi radian = 360°)
            float randomAngle = Random.Range(0f, Mathf.PI * 2f);

            // farClipPlane with multiplier permit to take into consideration camera settings
            float x = mainCameraPosition.x + Mathf.Cos(randomAngle) * (farClipPlane * 0.06f);
            float y = mainCameraPosition.y + Mathf.Sin(randomAngle) * (farClipPlane * 0.06f);

            Vector2 spawnPosition = new Vector2(x, y);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
