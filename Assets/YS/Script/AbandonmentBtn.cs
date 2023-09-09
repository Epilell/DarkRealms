using UnityEngine;
using UnityEngine.EventSystems;
using Rito.InventorySystem;

public class AbandonmentBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Inventory Inventory;
    public Transform btnScale;
    public CanvasGroup onGroup, offGroup;

    Vector3 defaultScale;

    public void Start() { defaultScale = btnScale.localScale; }

    public void OnClick()
    {
        CanvasGroupOn(onGroup);
        CanvasGroupOff(offGroup);
    }

    public void OnYesClick() // 나가기 버튼을 누르면
    {
        for (int i = 0; i < Inventory._Items.Length; i++) { Inventory.Remove(i); } // 인벤토리 내 모든 아이템 소멸
        FindObjectOfType<TimerUI>().isAbandonment = true; // 시간 흐르고
        StartCoroutine(FindObjectOfType<FadeOut>().FadeFlow()); // 메인화면으로 나감
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
        FindObjectOfType<CursorManager>().isCursorChange = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        btnScale.localScale = defaultScale;
        FindObjectOfType<CursorManager>().isCursorChange = true;
    }
}