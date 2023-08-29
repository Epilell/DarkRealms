using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType; // 버튼의 종류를 나타내는 열거형 변수
    public CanvasGroup mainGroup, optionGroup, soundGroup, displayGroup, onGroup, offGroup; // 각각의 화면을 나타내는 CanvasGroup 컴포넌트
    public Transform btnScale; // 버튼의 크기를 조절할 Transform 컴포넌트
    Vector3 defaultScale; // 버튼의 기본 크기를 저장할 변수
    // public TextMeshProUGUI buttonText; // 버튼의 텍스트 컴포넌트
    // Color defaultColor; // 버튼의 기본 색상을 저장

    public void Start()
    {
        defaultScale = btnScale.localScale; // 버튼의 기본 크기를 저장
        // defaultColor = buttonText.color; // 버튼의 기본 색상을 저장
    }

    public void OnBtnClick()
    {
        switch (currentType) // 버튼의 종류에 따라 실행할 동작을 결정
        {
            case BTNType.Option: // 옵션 버튼을 눌렀을 때
                CanvasGroupOn(optionGroup); // 옵션 화면을 켜고
                CanvasGroupOff(mainGroup); // 메인 화면을 끈다
                break;
            case BTNType.Sound: // 사운드 버튼을 눌렀을 때
                CanvasGroupOn(soundGroup); // 사운드 화면을 켜고
                CanvasGroupOff(optionGroup); // 옵션 화면을 끈다
                break;
            case BTNType.Display: // 디스플레이 버튼을 눌렀을 때
                CanvasGroupOn(displayGroup); // 디스플레이 화면을 켜고
                CanvasGroupOff(optionGroup); // 옵션 화면을 끈다
                break;
            case BTNType.Quit: // 종료 버튼을 눌렀을 때
                Debug.Log("게임 종료"); // 테스트 로그 출력
                Application.Quit(); // 어플리케이션을 종료
                break;
        }
    }

    public void CanvasGroupOn(CanvasGroup canvasGroup) // CanvasGroup을 켜는 함수
    {
        canvasGroup.alpha = 1; // 투명도를 1로 설정(보임)
        canvasGroup.interactable = true; // 상호작용 가능
        canvasGroup.blocksRaycasts = true; // 마우스 클릭 이벤트를 받을 수 있도록 설정
    }

    public void CanvasGroupOff(CanvasGroup canvasGroup) // CanvasGroup을 끄는 함수
    {
        canvasGroup.alpha = 0; // 투명도를 0으로 설정(안 보임)
        canvasGroup.interactable = false; // 상호작용 불가능
        canvasGroup.blocksRaycasts = false; // 마우스 클릭 이벤트를 받지 않도록 설정
    }

    // 마우스가 버튼 위에 올라갔을 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<CursorManager>().isCursorChange = true;
        btnScale.localScale = defaultScale * 1.2f; // 버튼의 크기를 1.2배 확장
        // buttonText.color = Color.red; // 버튼의 텍스트 색상 변경
    }

    // 마우스가 버튼에서 벗어났을 때
    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<CursorManager>().isCursorChange = false;
        btnScale.localScale = defaultScale; // 버튼의 크기를 기본 크기로 복구
        // buttonText.color = defaultColor; // 버튼의 텍스트 색상을 기본 색상으로
    }
}