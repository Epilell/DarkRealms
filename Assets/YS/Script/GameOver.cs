using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rito.InventorySystem;

public class GameOver : MonoBehaviour
{
    public FadeOut fadeOut;
    public Player player;
    public Inventory Inventory;
    public GameObject UIHitEffect;

    public Button goMainBtn;
    public Image gameOverImg, screen;
    public TextMeshProUGUI goMainBtnTxt;

    public bool isGameOver = false;

    private void Start() { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }

    public void PlayerGameOver()
    {
        if (isGameOver) return; // �ߺ� ������� �ʰ� ��

        FindObjectOfType<Player>().P_TakeDamage(10000); // �÷��̾� ���

        StartCoroutine(GameOverCoroutine());
    }

    public IEnumerator GameOverCoroutine() // ���� ���� ��Ÿ�� �ڷ�ƾ
    {
        if (isGameOver) yield break; // �ߺ� ������� �ʰ� ��

        isGameOver = true;

        UIHitEffect.SetActive(false); // ��� �� �ǰ� ȿ�� �� ����

        for (int i = 0; i < Inventory._Items.Length; i++) { Inventory.Remove(i); } // ������ �κ��丮 �� ��� ������ ����

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
        float fadeTime = 2.5f;

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

        goMainBtn.onClick.AddListener(() =>
        {
            FindObjectOfType<SoundManager>().PlaySound("Play");
            fadeOut.Fade();
        }); 
    }
}