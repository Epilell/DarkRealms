using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMousePointer : MonoBehaviour
{
    private bool isMouseVisible = true; // 마우스 포인터의 가시성 여부를 추적하기 위한 변수

    void Update()
    {
        // F2 키를 누르면 마우스 포인터의 가시성을 토글합니다.
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ToggleMouseVisibility();
        }
    }

    void ToggleMouseVisibility()
    {
        // 마우스 포인터의 가시성을 토글합니다.
        isMouseVisible = !isMouseVisible;
        Cursor.visible = isMouseVisible;
    }
}
