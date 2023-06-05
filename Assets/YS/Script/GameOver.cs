using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Rito.InventorySystem;

public class GameOver : MonoBehaviour
{
    public Inventory Inventory;
    public Player player;
    public P_Data p_data;
    public Image gameOverImg, screen;
    public Button goMainBtn;

    private float time = 0f;
    private float fadeTime = 5f;

    // �ڷ�ƾ���� ��ü ���ۿ��� TimeUI���� ȣ�⸸ �ϵ��� ����
    public void PlayerGameOver()
    {
        player.CurrentHp =0; // ����
        FindObjectOfType<HealthBar>().ChangeHP(); // ü�¹� ����

        for (int i = 0; i < Inventory._Items.Length; i++) { Inventory.Remove(i); } // ������ �κ��丮 �� ��� ������ ����

        StartCoroutine(GameOverCoroutine());
    }

    public IEnumerator GameOverCoroutine() // ���� ���� ��Ÿ�� �ڷ�ƾ
    {
        gameOverImg.gameObject.SetActive(true); // �̹��� Ȱ��ȭ
        screen.gameObject.SetActive(true); // �̹��� Ȱ��ȭ
        goMainBtn.gameObject.SetActive(true); // ��ư Ȱ��ȭ

        Color gameOverAlpha = gameOverImg.color; // ����
        Color screenAlpha = screen.color; // ����

        // ���̵� �ƿ�
        while (gameOverAlpha.a < fadeTime)
        {
            time += Time.deltaTime / fadeTime; // �ð� ���

            // ���� ����
            gameOverAlpha.a = Mathf.Lerp(0f, 1f, time);
            screenAlpha.a = Mathf.Lerp(0f, 0.7f, time);

            // �̹����� ���� ���� ����
            gameOverImg.color = gameOverAlpha;
            screen.color = screenAlpha;

            // ��ư
            float buttonAlpha = Mathf.Lerp(0f, 0.8f, time);
            Color buttonColor = goMainBtn.image.color;
            buttonColor.a = buttonAlpha;
            goMainBtn.image.color = buttonColor;

            yield return null; // �� ������ ���
        }

        yield return null;
    }
}