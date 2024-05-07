using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMousePointer : MonoBehaviour
{
    private bool isMouseVisible = true; // ���콺 �������� ���ü� ���θ� �����ϱ� ���� ����

    void Update()
    {
        // F2 Ű�� ������ ���콺 �������� ���ü��� ����մϴ�.
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ToggleMouseVisibility();
        }
    }

    void ToggleMouseVisibility()
    {
        // ���콺 �������� ���ü��� ����մϴ�.
        isMouseVisible = !isMouseVisible;
        Cursor.visible = isMouseVisible;
    }
}
