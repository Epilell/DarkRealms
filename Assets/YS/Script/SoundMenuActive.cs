using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenuActive : MonoBehaviour
{
    public GameObject soundMenu; // 사운드 메뉴 UI

    private bool isActive = false;

    private void Start()
    {
        soundMenu.SetActive(isActive); // 처음에 off
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) { // ESC 눌러서 on/off 전환
            isActive = !isActive;
            soundMenu.SetActive(isActive);
        }
    }
}