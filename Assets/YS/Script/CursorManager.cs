using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public Texture2D aim, hand; // Ŀ�� �̹���
    private Vector2 spot = Vector2.zero; // �̹��� ���� ����� ���콺 Ŭ�� ��ġ

    public bool isCursorChange;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "InGame")
        {
            // �κ��丮�� ����â�� �������� ��
            if (FindObjectOfType<ActiveMenu>().optionMenu.activeSelf == true || FindObjectOfType<ActiveMenu>().sound.alpha == 1
                || FindObjectOfType<ActiveMenu>().display.alpha == 1 || FindObjectOfType<OpenCloseUI>().inven.activeSelf == true)
            {
                if (isCursorChange) Cursor.SetCursor(hand, spot, CursorMode.Auto); // ��ư�� Ŀ�� �ø��� �̹��� ����
                else Cursor.SetCursor(null, spot, CursorMode.Auto); // �ƴϸ� �⺻ Ŀ��
            }
            else Cursor.SetCursor(aim, new Vector2(250, 350), CursorMode.Auto); // �ƴϸ� ���ؼ�
        }
        else
        {
            if (isCursorChange) Cursor.SetCursor(hand, spot, CursorMode.Auto); // ��ư�� Ŀ�� �ø��� �̹��� ����
            else Cursor.SetCursor(null, spot, CursorMode.Auto); // �ƴϸ� �⺻ Ŀ��
        }
    }
}