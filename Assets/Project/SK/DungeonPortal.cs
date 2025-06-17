using UnityEngine;

public class DungeonPortal : MonoBehaviour
{
    public int requiredScore = 1500;            // 포탈 통과를 위한 최소 점수
    public Transform targetPosition;            // 이동할 위치
    public string targetTag = "Player";         // 포탈을 통과할 대상 태그

    public GameObject previousEnemyGroup;       // 이전 맵 적 그룹
    public GameObject nextEnemyGroup;           // 다음 맵 적 그룹

    [Header("다음 포탈 오브젝트")]
    public GameObject nextPortalObject;         // 다음 포탈의 GameObject (처음엔 비활성화)

    private bool hasActivated = false;          // 중복 실행 방지

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasActivated) return;

        if (other.CompareTag(targetTag) &&
            GameManager.Instance != null &&
            GameManager.Instance.GetScore() >= requiredScore)
        {
            // 플레이어 이동
            other.transform.position = targetPosition.position;

            // 점수 초기화
            GameManager.Instance.ResetScore();

            // 적 그룹 전환
            if (previousEnemyGroup != null)
                previousEnemyGroup.SetActive(false);
            if (nextEnemyGroup != null)
                nextEnemyGroup.SetActive(true);

            // 다음 포탈 활성화
            if (nextPortalObject != null)
                nextPortalObject.SetActive(true);

            hasActivated = true;
        }
    }
}
