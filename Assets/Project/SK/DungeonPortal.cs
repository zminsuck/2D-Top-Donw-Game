using UnityEngine;

public class DungeonPortal : MonoBehaviour
{
    public int requiredScore = 1500;
    public Transform targetPosition;
    public string targetTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag) &&
            GameManager.Instance != null &&
            GameManager.Instance.GetScore() >= requiredScore)
        {
            other.transform.position = targetPosition.position;
        }
    }
}
