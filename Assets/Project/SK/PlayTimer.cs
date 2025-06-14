using UnityEngine;
using TMPro;

public class PlayTimer : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timeText.text = $"Time: {minutes}:{seconds:00}";
    }
}
