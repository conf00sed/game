using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 10.0f;
    public int initialWaveSize = 1;
    public int waveSizeIncrement = 2;
    private int currentWave = 1;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(2.0f); // Delay at the start

        while (true)
        {
            int waveSize = initialWaveSize + (currentWave - 1) * waveSizeIncrement;

            for (int i = 0; i < waveSize; i++) // Progressively increase wave size
            {
                SpawnEnemy();
                yield return new WaitForSeconds(1.0f); // Delay between enemy spawns in a wave
            }

            currentWave++;
            yield return new WaitForSeconds(timeBetweenWaves); // Delay between waves
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity); // Spawn eenemy prefab
    }
}

