using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickBack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform btnScale;  // 버튼 크기를 조정할 Transform
    public CanvasGroup onGroup, offGroup;  // 활성화, 비활성화 할 대상 UI
    Vector3 defaultScale;  // 기본 크기

    public void Start()
    {
        defaultScale = btnScale.localScale;  // 버튼의 기본 스케일을 저장
    }

    public void OnClick()
    {
        CanvasGroupOn(onGroup);  // 활성화할 UI 활성화
        CanvasGroupOff(offGroup);  // 비활성화할 UI 비활성화
    }

    public void CanvasGroupOn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;  // UI의 투명도를 1로 설정하여 활성화
        canvasGroup.interactable = true;  // UI의 상호작용을 가능하게 함
        canvasGroup.blocksRaycasts = true;  // UI가 마우스 클릭 이벤트를 받을 수 있게 함
    }

    public void CanvasGroupOff(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;  // UI의 투명도를 0으로 설정하여 비활성화
        canvasGroup.interactable = false;  // UI의 상호작용을 불가능하게 함
        canvasGroup.blocksRaycasts = false;  // UI가 마우스 클릭 이벤트를 받지 못하게 함
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스 포인터가 버튼 위에 있으면 버튼 크기를 1.2배로
        btnScale.localScale = defaultScale * 1.2f;
        FindObjectOfType<CursorManager>().isCursorChange = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스 포인터가 버튼을 벗어나면 버튼의 크기를 원래대로 함
        btnScale.localScale = defaultScale;
        FindObjectOfType<CursorManager>().isCursorChange = true;
    }
}