using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float enemySpacing = 0.5f;

    [SerializeField] float minX = -10f; // Min
    [SerializeField] float maxX = 10f; // Max
    [SerializeField] float minY = -10f; // Min
    [SerializeField] float maxY = 10f; // Max 

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Spawn  groups of enemies simultaneously
            for (int group = 0; group < 2; group++)
            {
                // Calculate a random position within the scene boundaries for this group
                Vector3 randomPosition = GetRandomSpawnPosition();

                // Spawn the group of enemies at the random position
                SpawnEnemyGroup(randomPosition);
            }

            // Spawn delay
            yield return new WaitForSeconds(5);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Calculate random x and y coordinates within the specified range
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);


        float randomZ = transform.position.z;

        // Create a vector representing the random position
        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

        return randomPosition;
    }

    void SpawnEnemyGroup(Vector3 startPosition)
    {
        // Spawn enemies in a group with the same pattern starting from the given position
        for (int i = 0; i < 5; i++)
        {
            // Calculate the spawn position for this enemy within the group
            Vector3 spawnPosition = startPosition + new Vector3(i * enemySpacing, 0f, 0f);

            // Instantiate the enemy at the calculated position
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}