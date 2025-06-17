using UnityEngine;

public class ClearPortal : MonoBehaviour
{
    public int requiredScore = 1500;
    public string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) &&
            GameManager.Instance != null &&
            GameManager.Instance.GetScore() >= requiredScore)
        {
            GameManager.Instance.GameClear(); // 게임 클리어 호출
        }
    }
}
