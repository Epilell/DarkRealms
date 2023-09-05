using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TimerUI : MonoBehaviour
{
    private float totalTime = 900.0f; // 타이머의 총 시간: 15분
    private float currentTime; // 현재 시간
    private float timing = 75f;
    /// <summary>
    /// 남은 시간 phase(자기장 데미지에 적용될수도?)
    /// </summary>
    public int phase = 1;

    public Text timerText; // 타이머UI
    public Button goMainBtn;

    [SerializeField]
    private GameObject TimerUIobj;
    private Animator timerAnimator;
    [SerializeField]
    private List<GameObject> lefts;
    [SerializeField]
    private List<GameObject> rights;
    Image img;

    private void Start()
    {
        goMainBtn.gameObject.SetActive(false);
        currentTime = totalTime;
        timerAnimator = TimerUIobj.GetComponent<Animator>();
    }

    private void Update()
    {
        // 인벤토리, 설정창 중 하나라도 켜져있으면 시간 느리게, 스킬 사용 불가
        if (FindObjectOfType<ActiveMenu>().optionMenu.activeSelf == true || FindObjectOfType<ActiveMenu>().sound.alpha == 1
            || FindObjectOfType<ActiveMenu>().display.alpha == 1 || FindObjectOfType<OpenCloseUI>().inven.activeSelf == true)
        {
            Time.timeScale = 0.0025f;
            FindObjectOfType<SkillManager>().isSkillCanUse = false;
        }
        else
        {
            Time.timeScale = 1;
            FindObjectOfType<SkillManager>().isSkillCanUse = true;
        }

        if (GetComponent<GameOver>().isGameOver) return; // 게임 오버 상태인 경우 Update() 실행하지 않음

        currentTime -= Time.deltaTime;

        if (currentTime <= 0) // 플레이 시간 지나면 타이머 종료 및 게임 오버
        {
            currentTime = 0;
            GetComponent<GameOver>().PlayerGameOver();
        }

        UpdateTimerText();
        UpdateFallenUI();
    }

    private void UpdateTimerText() // MM:SS 형식으로 시간 출력
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timeText = minutes.ToString("00") + ":" + seconds.ToString("00");

        timerText.text = timeText;
    }
    private void UpdateFallenUI()
    {
        if (currentTime < totalTime - timing && phase == 1)
        {
            PhaseChanger(phase);
            phase = 2;
        }
        else if (currentTime < totalTime - (timing * phase) && phase == 2)
        {
            PhaseChanger(phase);
            phase = 3;
        }
        else if (currentTime < totalTime - (timing * phase) && phase == 3)
        {
            PhaseChanger(phase);
            phase = 4;
        }
        else if (currentTime < totalTime - (timing * phase) && phase == 4)
        {
            PhaseChanger(phase);
            phase = 5;
        }
        else if (currentTime < totalTime - (timing * phase) && phase == 5)
        {
            PhaseChanger(phase);
            phase = 6;
        }
        else if (currentTime < totalTime - (timing * phase) && phase == 6)
        {
            PhaseChanger(phase);
            phase = 7;
        }
        else if (currentTime < totalTime - (timing * phase) && phase == 7)
        {
            PhaseChanger(phase);
            phase = 8;
        }
        else if (currentTime < totalTime - (timing * phase) && phase == 8)
        {
            PhaseChanger(phase);
            phase = 9;
        }
        else if (currentTime < totalTime - (timing * phase) && phase == 9)
        {
            PhaseChanger(phase);
            phase = 10;
        }
        else if (currentTime < totalTime - (timing * phase) && phase == 10)
        {
            PhaseChanger(phase);
            phase = 11;
        }
        else if (currentTime < totalTime - (timing * phase) && phase == 11)
        {
            PhaseChanger(phase);
            phase = 12;
        }
        else if (currentTime < totalTime - (timing * 11.95f) && phase == 12)
        {
            //시간이 끝났을 경우
            Animating(phase - 1);
            UIImageAlphaChanger(TimerUIobj, 0f);
            phase = 13;
        }
    }
    private void PhaseChanger(int PhaseNum)
    {
        timerAnimator.SetInteger("phase", PhaseNum + 1);
        Animating(PhaseNum - 1);
        StartCoroutine(RemoveBreak(PhaseNum - 1));

    }
    private void Animating(int num)
    {
        UIImageAlphaChanger(lefts[num], 1f);
        UIImageAlphaChanger(rights[num], 1f);
        lefts[num].GetComponent<Animator>().SetBool("On", true);
        rights[num].GetComponent<Animator>().SetBool("On", true);
    }
    private void UIImageAlphaChanger(GameObject obj, float alpha)
    {
        img = obj.GetComponent<Image>();
        Color color = img.color;
        color.a = alpha;
        img.color = color;
    }
    private IEnumerator RemoveBreak(int num)
    {
        yield return new WaitForSeconds(1f);
        lefts[0].SetActive(false);
        rights[0].SetActive(false);/*
        UIImageAlphaChanger(lefts[num], 0f);
        UIImageAlphaChanger(rights[num], 0f);
        Destroy(lefts[num]);*/

    }
}