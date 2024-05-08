using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float enemySpacing = 0.5f;

    private int currentLevel = 1; // Current level starts at 1

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
            // Calculate the number of groups based on the level
            int groupsToSpawn = Mathf.Max(currentLevel, 2); // Ensure at least 2 groups are spawned

            // Spawn groups of enemies simultaneously
            for (int group = 0; group < groupsToSpawn; group++)
            {
                Vector3 randomPosition = GetRandomSpawnPosition();

                SpawnEnemyGroup(randomPosition);
            }

            // Wait before spawning the next set of enemies
            yield return new WaitForSeconds(3);
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

    // Method to update the current level
    public void UpdateLevel(int newLevel)
    {
        currentLevel = newLevel;
    }
}
