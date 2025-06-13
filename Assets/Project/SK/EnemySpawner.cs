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
            Debug.LogError("enemyPrefab이 설정되지 않았습니다.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("spawnPoints 배열이 비어 있습니다!");
            return;
        }

        // 플레이어가 죽었거나 적이 최대치에 도달한 경우 스폰하지 않음
        if (playerHealth != null && playerHealth.IsDead) return;

        if (currentEnemyCount >= maxEnemies)
            return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // 플레이어를 자동 연결
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
                Debug.LogWarning("Player 태그가 붙은 오브젝트를 찾을 수 없습니다.");
            }

            enemyScript.OnDeath += HandleEnemyDeath;
        }

        // 스폰 사운드 재생
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
