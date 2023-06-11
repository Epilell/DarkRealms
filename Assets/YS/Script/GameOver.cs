using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rito.InventorySystem;

public class GameOver : MonoBehaviour
{
    public FadeOut fadeOut;
    public Inventory Inventory;
    public Player player;
    public Image gameOverImg, screen;
    public Button goMainBtn;
    public TextMeshProUGUI goMainBtnTxt;

    public bool isGameOver = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // �ڷ�ƾ���� ��ü ���ۿ��� TimeUI���� ȣ�⸸ �ϵ��� ����
    public void PlayerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        player.CurrentHp -= 10000; // ����
        FindObjectOfType<HealthBar>().ChangeHP(); // ü�¹� ����

        for (int i = 0; i < Inventory._Items.Length; i++) { Inventory.Remove(i); } // ������ �κ��丮 �� ��� ������ ����

        StartCoroutine(GameOverCoroutine());
    }

    public IEnumerator GameOverCoroutine() // ���� ���� ��Ÿ�� �ڷ�ƾ
    {
        gameOverImg.gameObject.SetActive(true); // �̹��� Ȱ��ȭ
        screen.gameObject.SetActive(true); // �̹��� Ȱ��ȭ
        goMainBtn.gameObject.SetActive(true); // ��ư Ȱ��ȭ

        // ����
        Color gameOverAlpha = gameOverImg.color;
        Color screenAlpha = screen.color;
        Color buttonAlpha = goMainBtn.GetComponent<Image>().color;
        Color textAlpha = goMainBtnTxt.color;

        // �ð� �ʱ�ȭ
        float time = 0f;
        float fadeTime = 2.4f;

        // ���̵� �ƿ�
        while (gameOverAlpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime; // �ð� ���

            // ���� ����
            gameOverAlpha.a = Mathf.Lerp(0f, 1f, time);
            screenAlpha.a = Mathf.Lerp(0f, 0.7f, time);
            buttonAlpha.a = Mathf.Lerp(0f, 1f, time);
            textAlpha.a = Mathf.Lerp(0f, 1f, time);

            // �̹����� ���� ���� ����
            gameOverImg.color = gameOverAlpha;
            screen.color = screenAlpha;
            goMainBtn.GetComponent<Image>().color = buttonAlpha;
            goMainBtnTxt.color = textAlpha;

            yield return null; // �� ������ ���
        }

        // Time.timeScale = 0;

        goMainBtn.onClick.AddListener(GoToNextScene); // ��ư Ŭ�� �̺�Ʈ�� GoToMainMenu �Լ��� �߰�
    }

    private void GoToNextScene()
    {
        // Time.timeScale = 1;
        fadeOut.Fade();
    }
}