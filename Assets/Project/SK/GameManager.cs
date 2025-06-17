using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject gameOverPanel;
    public TMP_Text scoreText;
    public Image scoreBarFill;

    [Header("Score")]
    public int maxScore = 1000;
    private int score = 0;

    [Header("Bar Animation")]
    public float fillSmoothSpeed = 5f;
    private float targetFillAmount = 0f;

    [Header("GameClear")]
    private float playTime = 0f;
    public TMP_Text playTimeText;
    public GameObject gameClearPanel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        UpdateScoreUI(); // 초기값 반영
    }

    void Update()
    {
        // 부드럽게 bar 따라가도록 처리
        if (scoreBarFill != null)
        {
            scoreBarFill.fillAmount = Mathf.Lerp(scoreBarFill.fillAmount, targetFillAmount, Time.deltaTime * fillSmoothSpeed);
        }

        // 플레이 시간 누적
        if (Time.timeScale > 0f)
        {
            playTime += Time.deltaTime;
            UpdatePlayTimeUI();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        score = Mathf.Clamp(score, 0, maxScore);
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;

        // 목표 fillAmount 갱신
        if (scoreBarFill != null)
            targetFillAmount = (float)score / maxScore;
    }
    void UpdatePlayTimeUI()
    {
        if (playTimeText != null)
            playTimeText.text = $"PLAYERTIME {Mathf.FloorToInt(playTime)}";
    }
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        UpdateFinalUI();
        gameOverPanel.SetActive(true);
    }
    public void GameClear()
    {
        Time.timeScale = 0f;
        UpdateFinalUI(); // <- 추가
        gameClearPanel.SetActive(true);
    }
    void UpdateFinalUI()
    {
        UpdateScoreUI();
        UpdatePlayTimeUI();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // 에디터 종료
        #else
            Application.Quit(); // 실제 빌드 종료
        #endif
    }
}
