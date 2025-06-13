using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public int maxEnemies = 10;
    public Transform[] spawnPoints;
    public PlayerHealth playerHealth;
    public AudioClip spawnSound;
    public AudioSource audioSource;

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

        // �÷��̾ �׾��ų� ���� �ִ�ġ�� ������ ��� �������� ����
        if (playerHealth != null && playerHealth.IsDead) return;

        if (currentEnemyCount >= maxEnemies)
            return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // �÷��̾ �ڵ� ����
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                enemyScript.player = playerObject.transform;
            }
            else
            {
                Debug.LogWarning("Player �±װ� ���� ������Ʈ�� ã�� �� �����ϴ�.");
            }

            enemyScript.OnDeath += HandleEnemyDeath;
        }

        // ���� ���� ���
        if (spawnSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }

        currentEnemyCount++;
    }

    void HandleEnemyDeath()
    {
        currentEnemyCount--;
    }
}
