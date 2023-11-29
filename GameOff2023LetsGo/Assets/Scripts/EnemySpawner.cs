using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;
    public float minSpawnInterval = 3f;
    public float maxSpawnInterval = 7f;
    public int maxEnemies = 10; // Adjust this based on your desired limit

    private int spawnedEnemies = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (spawnedEnemies < maxEnemies)
        {
            GameObject selectedEnemyPrefab = GetNextEnemyPrefab();
            Instantiate(selectedEnemyPrefab, spawnPoint.position, spawnPoint.rotation);

            spawnedEnemies++;
            float randomSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(randomSpawnInterval);
        }
    }

    GameObject GetNextEnemyPrefab()
    {
        // Implement logic here to determine which enemy prefab to spawn next
        // You can use spawnedEnemies as an index or any other criteria
        // For simplicity, let's just cycle through the array
        int index = spawnedEnemies % enemyPrefabs.Length;
        return enemyPrefabs[index];
    }
}