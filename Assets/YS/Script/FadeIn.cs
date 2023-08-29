using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image panel;  // ���̵� �ο� ����� �̹��� ������Ʈ
    float time = 0f;
    float fadeTime = 1f;

    private void Start() { StartCoroutine(FadeInCoroutine()); }

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