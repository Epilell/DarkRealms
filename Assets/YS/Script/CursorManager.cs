using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D aim, hand; // 커서 이미지
    private Vector2 spot = new Vector2(0.5f, 0.5f); // 이미지 중심이 마우스 클릭 위치

    public bool isCursorChange;

    void Start() { Cursor.SetCursor(aim, spot, CursorMode.Auto); } // 기본 커서 이미지를 조준선으로 변경

    private void Update()
    {
        // 인벤토리나 설정창이 켜져있을 때
        if (FindObjectOfType<ActiveMenu>().optionMenu.activeSelf == true || FindObjectOfType<ActiveMenu>().sound.alpha == 1
            || FindObjectOfType<ActiveMenu>().display.alpha == 1 || FindObjectOfType<OpenCloseUI>().inven.activeSelf == true)
        {
            if (isCursorChange) Cursor.SetCursor(hand, spot, CursorMode.Auto); // 버튼에 커서 올리면 이미지 변경
            else Cursor.SetCursor(null, spot, CursorMode.Auto); // 아니면 기본 커서
        }
        else Cursor.SetCursor(aim, spot, CursorMode.Auto); // 아니면 조준선
    }
}