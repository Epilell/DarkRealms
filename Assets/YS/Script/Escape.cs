using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public Image panel; // ���̵� �ƿ��� ����� �̹���
    float time = 0f; // ��� �ð�
    float fadeTime = 1f; // ���̵� �ƿ� �ð�
    public string scene;

    // Ż�ⱸ���� F Ű�� ������ ���̵� �ƿ� �� ���� �� �ε�
    /*void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Escape") && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(FadeOut());
        }
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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

        SceneManager.LoadScene(scene); // �� ��ȯ
    }
}