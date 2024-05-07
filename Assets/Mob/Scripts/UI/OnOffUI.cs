using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnOffUI : MonoBehaviour
{
    private bool uiVisible = true; // UI�� ���� ǥ�õǴ��� ���θ� �����ϱ� ���� ����

    void Update()
    {
        // F1 Ű�� ������ UI�� ���
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleChildrenUI(transform); // Root GameObject�� Transform�� �����մϴ�.

            // UI ǥ�� ���¸� ����մϴ�.
            uiVisible = !uiVisible;
        }
    }

    void ToggleChildrenUI(Transform parent)
    {
        // �θ� Transform�� �ڽ� GameObject�� ��ȸ�մϴ�.
        foreach (Transform child in parent)
        {
            // �ڽ� GameObject�� CanvasRenderer ������Ʈ�� �����ɴϴ�.
            CanvasRenderer canvasRenderer = child.GetComponent<CanvasRenderer>();

            // CanvasRenderer�� �ִ� ���
            if (canvasRenderer != null)
            {
                // ���� UI ǥ�� ���¿� ���� ���� ���� �����մϴ�.
                canvasRenderer.SetAlpha(uiVisible ? 1f : 0f);
            }

            // �ڽ� GameObject�� �ڽ��� �ִ� ��쿡�� ��� ȣ���մϴ�.
            if (child.childCount > 0)
            {
                ToggleChildrenUI(child);
            }
        }

    }
}
