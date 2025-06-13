using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public int maxEnemies = 10;
    public Transform[] spawnPoints; // ������ ��ġ �迭 ���

    private int currentEnemyCount = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("enemyPrefab�� �������� �ʾҽ��ϴ�.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("spawnPoints �迭�� ��� �ֽ��ϴ�!");
            return;
        }

        if (currentEnemyCount >= maxEnemies)
            return;

        // ? ������ ��ġ �� �ϳ��� ������ ����
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
