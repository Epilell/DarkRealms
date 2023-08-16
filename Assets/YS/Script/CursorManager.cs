using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorImage; // 커서 이미지
    private Vector2 spot = new Vector2(0.5f, 0.5f); // 이미지 중심이 마우스 클릭 위치
    //public Vector2 cursorSize = new Vector2(20f, 20f); // 커서 크기

    void Start() // 게임 시작 시 커서 이미지 설정
    {
        Cursor.SetCursor(cursorImage, spot, CursorMode.Auto);
        /* //Cursor.SetCursor(cursorImage, spot, CursorMode.ForceSoftware); // 크기 변경을 위해 ForceSoftware 모드 사용

         // 커서 크기 변경
         Texture2D resizedCursorImage = new Texture2D((int)cursorSize.x, (int)cursorSize.y);
         Color[] pixels = cursorImage.GetPixels(0, 0, cursorImage.width, cursorImage.height);
         resizedCursorImage.SetPixels(0, 0, (int)cursorSize.x, (int)cursorSize.y, pixels);
         resizedCursorImage.Apply();

         // 커서 이미지 설정
         Vector2 cursorHotspot = new Vector2(resizedCursorImage.width * spot.x, resizedCursorImage.height * spot.y);
         Cursor.SetCursor(resizedCursorImage, cursorHotspot, CursorMode.ForceSoftware);

         // 커서 활성화
         Cursor.visible = true;*/
    }
}

/*
// public Vector2 cursorSize = new Vector2(150f, 150f); // 원하는 커서 크기 설정
 
void Start()
{
    // 커서 크기 설정
    Cursor.SetCursor(cursorImage, spot, CursorMode.Auto);
    Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware); // 크기 변경을 위해 ForceSoftware 모드 사용
    Cursor.visible = true; // 커서 활성화

    // 커서 크기 변경
    Vector2 cursorHotspot = new Vector2(cursorImage.width * spot.x, cursorImage.height * spot.y);
    Cursor.SetCursor(cursorImage, cursorHotspot, CursorMode.ForceSoftware);
    Cursor.lockState = CursorLockMode.None;
    Cursor.SetCursor(cursorImage, cursorHotspot, CursorMode.Auto); // 다시 Auto 모드로 변경하여 하드웨어 가속 적용
}*/


/*using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorImage; // 커서 이미지
    public Vector2 cursorSize = new Vector2(32f, 32f); // 원하는 커서 크기 설정

    // 게임 시작 시 커서 이미지 설정
    void Start()
    {
        // 커서 크기 변경
        Texture2D resizedCursorImage = new Texture2D((int)cursorSize.x, (int)cursorSize.y);
        Color[] pixels = cursorImage.GetPixels(0, 0, cursorImage.width, cursorImage.height);
        resizedCursorImage.SetPixels(0, 0, (int)cursorSize.x, (int)cursorSize.y, pixels);
        resizedCursorImage.Apply();

        // 커서 이미지 설정
        Vector2 cursorHotspot = new Vector2(resizedCursorImage.width * 0.5f, resizedCursorImage.height * 0.5f);
        Cursor.SetCursor(resizedCursorImage, cursorHotspot, CursorMode.ForceSoftware);

        // 커서 활성화
        Cursor.visible = true;
    }
}*/