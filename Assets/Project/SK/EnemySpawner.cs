using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public int maxEnemies = 10;
    public Transform[] spawnPoints; // 고정된 위치 배열 사용

    private int currentEnemyCount = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("enemyPrefab이 설정되지 않았습니다.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("spawnPoints 배열이 비어 있습니다!");
            return;
        }

        if (currentEnemyCount >= maxEnemies)
            return;

        // ? 지정된 위치 중 하나를 무작위 선택
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
            enemyScript.OnDeath += HandleEnemyDeath;

        currentEnemyCount++;
    }

    void HandleEnemyDeath()
    {
        currentEnemyCount--;
    }
}
