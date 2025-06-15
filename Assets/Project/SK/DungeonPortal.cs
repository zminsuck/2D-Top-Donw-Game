using UnityEngine;

public class DungeonPortal : MonoBehaviour
{
    public int requiredScore = 1500;
    public Transform targetPosition;
    public string targetTag = "Player";

    public GameObject previousEnemyGroup;  // 첫 번째 맵 적 그룹
    public GameObject nextEnemyGroup;      // 다음 맵 적 그룹

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag) &&
            GameManager.Instance != null &&
            GameManager.Instance.GetScore() >= requiredScore)
        {
            // 플레이어 위치 이동
            other.transform.position = targetPosition.position;

            // 스코어 초기화
            GameManager.Instance.ResetScore();

            // 에너미 그룹 전환
            if (previousEnemyGroup != null)
                previousEnemyGroup.SetActive(false);
            if (nextEnemyGroup != null)
                nextEnemyGroup.SetActive(true);
        }
    }
}
