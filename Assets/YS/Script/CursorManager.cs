using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D aim, hand; // Ŀ�� �̹���
    private Vector2 spot = new Vector2(0.5f, 0.5f); // �̹��� �߽��� ���콺 Ŭ�� ��ġ

    public bool isCursorChange;

    void Start() { Cursor.SetCursor(aim, spot, CursorMode.Auto); } // �⺻ Ŀ�� �̹����� ���ؼ����� ����

    private void Update()
    {
        // �κ��丮�� ����â�� �������� ��
        if (FindObjectOfType<ActiveMenu>().optionMenu.activeSelf == true || FindObjectOfType<ActiveMenu>().sound.alpha == 1
            || FindObjectOfType<ActiveMenu>().display.alpha == 1 || FindObjectOfType<OpenCloseUI>().inven.activeSelf == true)
        {
            if (isCursorChange) Cursor.SetCursor(hand, spot, CursorMode.Auto); // ��ư�� Ŀ�� �ø��� �̹��� ����
            else Cursor.SetCursor(null, spot, CursorMode.Auto); // �ƴϸ� �⺻ Ŀ��
        }
        else Cursor.SetCursor(aim, spot, CursorMode.Auto); // �ƴϸ� ���ؼ�
    }
}