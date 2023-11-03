using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab; // Assign the power-up prefab in the Inspector
    public float spawnInterval = 10.0f; // Adjust the spawn interval 
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnPowerUps());

        // optimisation (unused objects deleted after 30s)
        Destroy(gameObject, 30f);
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            // while game is running spawn powerups randomly across the map
            Vector3 randomSpawnPoint = GetRandomSpawnPoint();
            Instantiate(powerUpPrefab, randomSpawnPoint, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Make sure you have a camera in the scene.");
            return Vector3.zero;
        }

        // camera inforamtion
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 randomSpawnPoint = new Vector3(Random.Range(-cameraWidth, cameraWidth), Random.Range(-cameraHeight, cameraHeight), 0f);

        // Ensure the spawn point is outside the camera's view
        randomSpawnPoint.x += Mathf.Sign(randomSpawnPoint.x) * cameraWidth * 1.5f;
        randomSpawnPoint.y += Mathf.Sign(randomSpawnPoint.y) * cameraHeight * 1.5f;

        
        return randomSpawnPoint;
    }
}
