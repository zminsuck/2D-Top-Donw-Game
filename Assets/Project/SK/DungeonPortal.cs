using UnityEngine;

public class DungeonPortal : MonoBehaviour
{
    public int requiredScore = 1500;            // ��Ż ����� ���� �ּ� ����
    public Transform targetPosition;            // �̵��� ��ġ
    public string targetTag = "Player";         // ��Ż�� ����� ��� �±�

    public GameObject previousEnemyGroup;       // ���� �� �� �׷�
    public GameObject nextEnemyGroup;           // ���� �� �� �׷�

    [Header("���� ��Ż ������Ʈ")]
    public GameObject nextPortalObject;         // ���� ��Ż�� GameObject (ó���� ��Ȱ��ȭ)

    private bool hasActivated = false;          // �ߺ� ���� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasActivated) return;

        if (other.CompareTag(targetTag) &&
            GameManager.Instance != null &&
            GameManager.Instance.GetScore() >= requiredScore)
        {
            // �÷��̾� �̵�
            other.transform.position = targetPosition.position;

            // ���� �ʱ�ȭ
            GameManager.Instance.ResetScore();

            // �� �׷� ��ȯ
            if (previousEnemyGroup != null)
                previousEnemyGroup.SetActive(false);
            if (nextEnemyGroup != null)
                nextEnemyGroup.SetActive(true);

            // ���� ��Ż Ȱ��ȭ
            if (nextPortalObject != null)
                nextPortalObject.SetActive(true);

            hasActivated = true;
        }
    }
}
