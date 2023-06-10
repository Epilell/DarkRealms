using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Escape : MonoBehaviour
{
    public Image panel; // ���̵� �ƿ��� ����� �̹���
    float time = 0f; // ��� �ð�
    float fadeTime = 1f; // ���̵� �ƿ� �ð�
    public SceneAsset scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �÷��̾�� �浹�ϸ� ���̵� �ƿ�
        {
            StartCoroutine(FadeOut());
        }
    }

    // ���̵� �ƿ� ȿ���� �����ϰ� ���� ���� �ε��ϴ� �ڷ�ƾ
    public IEnumerator FadeOut() // ���̵� �ƿ� �ڷ�ƾ �Լ�
    {
        panel.gameObject.SetActive(true); // �̹��� Ȱ��ȭ
        Color alpha = panel.color; // ����

        // ���̵� �ƿ�
        while (alpha.a < fadeTime)
        {
            time += Time.deltaTime / fadeTime; // �ð� ���
            alpha.a = Mathf.Lerp(0f, 1f, time); // ���İ��� ������ ������Ŵ(������ ��ο���)
            panel.color = alpha; // �̹����� ���� ��������
            yield return null; // �� ������ ���
        }

        SceneManager.LoadScene(scene.name);
    }
}