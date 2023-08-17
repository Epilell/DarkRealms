using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorImage; // 커서 이미지
    private Vector2 spot = new Vector2(0.5f, 0.5f); // 이미지 중심이 마우스 클릭 위치

    void Start() { Cursor.SetCursor(cursorImage, spot, CursorMode.Auto); } // 게임 시작 시 커서 이미지 설정

    private void Update() // 마우스 이벤트 처리
    {
        
    }
}