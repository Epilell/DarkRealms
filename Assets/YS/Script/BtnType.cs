using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform btnScale;
    public CanvasGroup mainGroup, optionGroup, soundGroup;
    Vector3 defaultScale;

    public void Start()
    {
        defaultScale = btnScale.localScale;
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            /*case BTNType.Start:
                SceneManager.LoadScene("Loading");
                break;*/
            case BTNType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNType.Sound:
                CanvasGroupOn(soundGroup);
                CanvasGroupOff(optionGroup);
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case BTNType.Quit:
                Debug.Log("종료");
                Application.Quit();
                break;
        }
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

    // 마우스가 올라가면 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        btnScale.localScale = defaultScale * 1.2f;

        /*if (ColorUtility.TryParseHtmlString("#570016", out newColor))
        {
            textComponent.color = newColor;
        }*/
    }

    // 마우스가 벗어나면 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        btnScale.localScale = defaultScale;
    }
}