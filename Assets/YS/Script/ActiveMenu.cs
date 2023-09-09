using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMenu : MonoBehaviour
{
    public GameObject optionMenu;
    public CanvasGroup sound, display, warning;

    private void Start() { optionMenu.SetActive(false); }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionMenu.activeSelf == false && sound.alpha == 0 && display.alpha == 0 && warning.alpha == 0) { optionMenu.SetActive(true); } // 설정창이 모두 꺼져있을 때 메인 옵션창 켜기
            else if (optionMenu.activeSelf == true && sound.alpha == 0 && display.alpha == 0 && warning.alpha == 0) { optionMenu.SetActive(false); } // 메인 옵션창만 켜져있으면 끄기
            else { }
        }
    }
}