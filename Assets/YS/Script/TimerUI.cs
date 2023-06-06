using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public float totalTime = 600.0f; // 타이머의 총 시간: 10분
    private float currentTime; // 현재 시간
    public Text timerText; // 타이머UI
    public Button goMainBtn;

    private bool isGameOver = false; // 게임 오버 여부

    private void Start()
    {
        goMainBtn.gameObject.SetActive(false);
        currentTime = totalTime;
    }

    private void Update()
    {
        if (GetComponent<GameOver>().isGameOver) // 게임 오버 상태인 경우 Update() 실행하지 않음
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0) // 플레이 시간 지나면 타이머 종료 및 게임 오버
        {
            currentTime = 0;
            GetComponent<GameOver>().PlayerGameOver();
        }

        UpdateTimerText();
    }

    private void UpdateTimerText() // MM:SS 형식으로 시간 출력
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timeText = minutes.ToString("00") + ":" + seconds.ToString("00");

        timerText.text = timeText;
    }
}
