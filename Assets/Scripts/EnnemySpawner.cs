using UnityEngine;
using System.Collections;

public class EnnemySpawner : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    int numberOfEntities = 10;

    [SerializeField]
    float spawnInterval = 2f;

    [SerializeField]
    GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnEntities());
    }

    IEnumerator SpawnEntities()
    {
        yield return new WaitForSeconds(1f);

        for(int i =0 ; i<numberOfEntities; i++)
        {
            Vector2 mainCameraPosition = Camera.main.transform.position;

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
