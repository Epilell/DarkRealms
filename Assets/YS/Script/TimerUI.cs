using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public float totalTime = 600.0f; // Ÿ�̸��� �� �ð�: 10��
    private float currentTime; // ���� �ð�
    public Text timerText; // Ÿ�̸�UI

    private void Start() { currentTime = totalTime; }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0) // �÷��� �ð� ������ Ÿ�̸� ���� �� ���� ����
        {
            currentTime = 0;
            GetComponent<GameOver>().PlayerGameOver();
        }

        UpdateTimerText();
    }

    private void UpdateTimerText() // MM:SS �������� �ð� ���
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timeText = minutes.ToString("00") + ":" + seconds.ToString("00");

        timerText.text = timeText;
    }
}
