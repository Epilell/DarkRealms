using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    public Text text;
    public Image panel;  // ���̵� �ο� ����� �̹��� ������Ʈ
    float time = 0f;  // ���̵� �� ���� �ð�
    float fadeTime = 1f;

    private void Start()
    {
        StartCoroutine(FadeInCoroutine()); // �ڷ�ƾ ����

        if (SceneManager.GetActiveScene().name == "LoadingOut")  // �׽�Ʈ�� ���_230506
        {
            Coin coin = new Coin();
            // Debug.Log(coin.GetCoin());
            text.text = "Coin: " + coin.GetCoin().ToString();
        }
    }

    public IEnumerator FadeInCoroutine()
    {
        panel.gameObject.SetActive(true); // �̹��� ������Ʈ Ȱ��ȭ
        Color alpha = panel.color; // �̹����� ���� ������ ������

        // ���̵� ��
        while (alpha.a > 0f) // �̹����� ���İ��� 0���� ũ�� �ݺ�
        {
            time += Time.deltaTime / fadeTime; // �ð� ����
            alpha.a = Mathf.Lerp(1f, 0f, time); // ���İ��� ������ ���ҽ�Ŵ(������ ��������)
            panel.color = alpha; // �̹����� ���� ��������
            yield return null; // �� ������ ���
        }

        panel.gameObject.SetActive(false); // �̹��� ��Ȱ��ȭ

        yield return null; // �ڷ�ƾ ����
    }
}
