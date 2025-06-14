using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Score")]
    public int score = 0;
    public TextMeshProUGUI scoreText; // UI ����
    public int requiredScoreForNextDungeon = 1500;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        Debug.Log($"Score: {score}");
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    public bool HasReachedRequirement()
    {
        return score >= requiredScoreForNextDungeon;
    }

    public void GameOver()
    {
        // ���� ���� ������ �ʿ��ϸ� �߰�
        Debug.Log("Game Over!");
    }
}
