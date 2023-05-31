using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenuActive : MonoBehaviour
{
    public GameObject soundMenu; // ���� �޴� UI

    private bool isActive = false;

    private void Start()
    {
        soundMenu.SetActive(isActive); // ó���� off
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) { // ESC ������ on/off ��ȯ
            isActive = !isActive;
            soundMenu.SetActive(isActive);
        }
    }
}