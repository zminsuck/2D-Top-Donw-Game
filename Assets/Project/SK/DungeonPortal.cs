using UnityEngine;

public class DungeonPortal : MonoBehaviour
{
    public int requiredScore = 1500;
    public Transform targetPosition;
    public string targetTag = "Player";

    public GameObject previousEnemyGroup;  // ù ��° �� �� �׷�
    public GameObject nextEnemyGroup;      // ���� �� �� �׷�

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag) &&
            GameManager.Instance != null &&
            GameManager.Instance.GetScore() >= requiredScore)
        {
            // �÷��̾� ��ġ �̵�
            other.transform.position = targetPosition.position;

            // ���ھ� �ʱ�ȭ
            GameManager.Instance.ResetScore();

            // ���ʹ� �׷� ��ȯ
            if (previousEnemyGroup != null)
                previousEnemyGroup.SetActive(false);
            if (nextEnemyGroup != null)
                nextEnemyGroup.SetActive(true);
        }
    }
}
