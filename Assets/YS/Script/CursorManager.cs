using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorImage; // Ŀ�� �̹���
    private Vector2 spot = new Vector2(0.5f, 0.5f); // �̹��� �߽��� ���콺 Ŭ�� ��ġ
    //public Vector2 cursorSize = new Vector2(20f, 20f); // Ŀ�� ũ��

    void Start() // ���� ���� �� Ŀ�� �̹��� ����
    {
        Cursor.SetCursor(cursorImage, spot, CursorMode.Auto);
        /* //Cursor.SetCursor(cursorImage, spot, CursorMode.ForceSoftware); // ũ�� ������ ���� ForceSoftware ��� ���

         // Ŀ�� ũ�� ����
         Texture2D resizedCursorImage = new Texture2D((int)cursorSize.x, (int)cursorSize.y);
         Color[] pixels = cursorImage.GetPixels(0, 0, cursorImage.width, cursorImage.height);
         resizedCursorImage.SetPixels(0, 0, (int)cursorSize.x, (int)cursorSize.y, pixels);
         resizedCursorImage.Apply();

         // Ŀ�� �̹��� ����
         Vector2 cursorHotspot = new Vector2(resizedCursorImage.width * spot.x, resizedCursorImage.height * spot.y);
         Cursor.SetCursor(resizedCursorImage, cursorHotspot, CursorMode.ForceSoftware);

         // Ŀ�� Ȱ��ȭ
         Cursor.visible = true;*/
    }
}

/*
// public Vector2 cursorSize = new Vector2(150f, 150f); // ���ϴ� Ŀ�� ũ�� ����
 
void Start()
{
    // Ŀ�� ũ�� ����
    Cursor.SetCursor(cursorImage, spot, CursorMode.Auto);
    Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware); // ũ�� ������ ���� ForceSoftware ��� ���
    Cursor.visible = true; // Ŀ�� Ȱ��ȭ

    // Ŀ�� ũ�� ����
    Vector2 cursorHotspot = new Vector2(cursorImage.width * spot.x, cursorImage.height * spot.y);
    Cursor.SetCursor(cursorImage, cursorHotspot, CursorMode.ForceSoftware);
    Cursor.lockState = CursorLockMode.None;
    Cursor.SetCursor(cursorImage, cursorHotspot, CursorMode.Auto); // �ٽ� Auto ���� �����Ͽ� �ϵ���� ���� ����
}*/


/*using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorImage; // Ŀ�� �̹���
    public Vector2 cursorSize = new Vector2(32f, 32f); // ���ϴ� Ŀ�� ũ�� ����

    // ���� ���� �� Ŀ�� �̹��� ����
    void Start()
    {
        // Ŀ�� ũ�� ����
        Texture2D resizedCursorImage = new Texture2D((int)cursorSize.x, (int)cursorSize.y);
        Color[] pixels = cursorImage.GetPixels(0, 0, cursorImage.width, cursorImage.height);
        resizedCursorImage.SetPixels(0, 0, (int)cursorSize.x, (int)cursorSize.y, pixels);
        resizedCursorImage.Apply();

        // Ŀ�� �̹��� ����
        Vector2 cursorHotspot = new Vector2(resizedCursorImage.width * 0.5f, resizedCursorImage.height * 0.5f);
        Cursor.SetCursor(resizedCursorImage, cursorHotspot, CursorMode.ForceSoftware);

        // Ŀ�� Ȱ��ȭ
        Cursor.visible = true;
    }
}*/