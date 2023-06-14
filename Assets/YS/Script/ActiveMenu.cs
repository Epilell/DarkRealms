using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMenu : MonoBehaviour
{
    public GameObject optionMenu;
    public CanvasGroup sound, display;

    private void Start() { optionMenu.SetActive(false); }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionMenu.activeSelf == false && sound.alpha == 0 && display.alpha == 0) { optionMenu.SetActive(true); } // 설정창이 모두 꺼져있을 때 메인 옵션창 켜기
            else if (optionMenu.activeSelf == true && sound.alpha == 0 && display.alpha == 0) { optionMenu.SetActive(false); } // 메인 옵션창만 켜져있으면 끄기
            else { }
        }

        if (optionMenu.activeSelf == true || sound.alpha == 1 || display.alpha == 1) { Time.timeScale = 0; } // 설정창이 하나라도 켜져있으면 시간 정지
        else { Time.timeScale = 1; }
    }
}