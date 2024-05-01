using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 2f);
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(3f, 3f, 0), Quaternion.identity);
    }
}