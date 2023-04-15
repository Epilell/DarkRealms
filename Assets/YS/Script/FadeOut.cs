using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public Image panel;
    float time = 0f;
    float fadeTime = 1f;
    public string scene;

    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }
    public IEnumerator FadeFlow()
    {
        panel.gameObject.SetActive(true);
        // time = 0f;
        Color alpha = panel.color;

        // ���̵� �ƿ�
        while(alpha.a < fadeTime)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(0f, 1f, time);
            panel.color = alpha;
            yield return null;
        }

        SceneManager.LoadScene(scene);
    }
}