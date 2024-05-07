using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnOffUI : MonoBehaviour
{
    private bool uiVisible = true; // UI가 현재 표시되는지 여부를 추적하기 위한 변수

    void Update()
    {
        // F1 키를 누르면 UI를 토글
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleChildrenUI(transform); // Root GameObject의 Transform을 전달합니다.

            // UI 표시 상태를 토글합니다.
            uiVisible = !uiVisible;
        }
    }

    void ToggleChildrenUI(Transform parent)
    {
        // 부모 Transform의 자식 GameObject를 순회합니다.
        foreach (Transform child in parent)
        {
            // 자식 GameObject의 CanvasRenderer 컴포넌트를 가져옵니다.
            CanvasRenderer canvasRenderer = child.GetComponent<CanvasRenderer>();

            // CanvasRenderer가 있는 경우
            if (canvasRenderer != null)
            {
                // 현재 UI 표시 상태에 따라 알파 값을 설정합니다.
                canvasRenderer.SetAlpha(uiVisible ? 1f : 0f);
            }

            // 자식 GameObject에 자식이 있는 경우에만 재귀 호출합니다.
            if (child.childCount > 0)
            {
                ToggleChildrenUI(child);
            }
        }

    }
}
