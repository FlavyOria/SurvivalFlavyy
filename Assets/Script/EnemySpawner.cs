using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float groupSpacing = 5f; // Spacing between enemy groups on the x-axis
    [SerializeField] float enemySpacing = 0.3f; // Spacing between enemies within a group on the x-axis

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Spawn three groups of enemies simultaneously
            for (int group = 0; group < 3; group++)
            {
                // Calculate a random position within the scene boundaries for this group
                Vector3 randomPosition = GetRandomSpawnPosition();

                // Spawn the group of enemies at the random position
                SpawnEnemyGroup(randomPosition);
            }

            // Wait for a short delay before restarting the spawning cycle
            yield return new WaitForSeconds(5);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Calculate random x, y, and z coordinates within the scene boundaries
        float randomX = Random.Range(-groupSpacing, groupSpacing);
        float randomY = Random.Range(-groupSpacing, groupSpacing);
        float randomZ = transform.position.z; // Keep the z-position constant

        // Create a vector representing the random position
        Vector3 randomPosition = new Vector3(transform.position.x + randomX, transform.position.y + randomY, randomZ);

        return randomPosition;
    }

    void SpawnEnemyGroup(Vector3 startPosition)
    {
        // Spawn enemies in a group with the same pattern starting from the given position
        for (int i = 0; i < 10; i++)
        {
            // Calculate the spawn position for this enemy within the group
            Vector3 spawnPosition = startPosition + new Vector3(i * enemySpacing, 0f, 0f);

            // Instantiate the enemy at the calculated position
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
