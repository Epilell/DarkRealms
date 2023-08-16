using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private float totalTime = 900.0f; // Ÿ�̸��� �� �ð�: 15��
    private float currentTime; // ���� �ð�

    public Text timerText; // Ÿ�̸�UI
    public Button goMainBtn;

    private void Start()
    {
        goMainBtn.gameObject.SetActive(false);
        currentTime = totalTime;
    }

    private void Update()
    {
        // �κ��丮, ����â �� �ϳ��� ���������� �ð� ����, ��ų ��� �Ұ�
        if (FindObjectOfType<ActiveMenu>().optionMenu.activeSelf == true || FindObjectOfType<ActiveMenu>().sound.alpha == 1
            || FindObjectOfType<ActiveMenu>().display.alpha == 1 || FindObjectOfType<OpenCloseUI>().inven.activeSelf == true)
        {
            Time.timeScale = 0;
            FindObjectOfType<SkillManager>().molotovdata.CanUse = false;
            FindObjectOfType<SkillManager>().siegemodedata.CanUse = false;
            FindObjectOfType<SkillManager>().dodgedata.CanUse = false;
            FindObjectOfType<SkillManager>().evdshotdata.CanUse = false;
        }
        else { Time.timeScale = 1; }

        if (GetComponent<GameOver>().isGameOver) // ���� ���� ������ ��� Update() �������� ����
            return;

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