using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private float totalTime = 900.0f; // 타이머의 총 시간: 15분
    private float currentTime; // 현재 시간

    public Text timerText; // 타이머UI
    public Button goMainBtn;

    private void Start()
    {
        goMainBtn.gameObject.SetActive(false);
        currentTime = totalTime;
    }

    private void Update()
    {
        // 인벤토리, 설정창 중 하나라도 켜져있으면 시간 정지, 스킬 사용 불가
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