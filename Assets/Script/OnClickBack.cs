using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickBack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform btnScale;
    public CanvasGroup optionGroup, soundGroup;
    Vector3 defaultScale;

    public void Start()
    {
        defaultScale = btnScale.localScale;
    }

    public void OnClick()
    {
        CanvasGroupOn(optionGroup);
        CanvasGroupOff(soundGroup);
    }

    public void CanvasGroupOn(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void CanvasGroupOff(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        btnScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        btnScale.localScale = defaultScale;
    }
}