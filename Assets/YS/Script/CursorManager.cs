using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorImage; // Ŀ�� �̹���
    private Vector2 spot = new Vector2(0.5f, 0.5f); // �̹��� �߽��� ���콺 Ŭ�� ��ġ

    void Start() { Cursor.SetCursor(cursorImage, spot, CursorMode.Auto); } // ���� ���� �� Ŀ�� �̹��� ����

    private void Update() // ���콺 �̺�Ʈ ó��
    {
        
    }
}