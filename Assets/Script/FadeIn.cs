using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    public void Fade()
    {
        StartCoroutine(FadeInFlow());
    }
    IEnumerator FadeInFlow()
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;

        // 페이드 인
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1f, 0f, time);
            Panel.color = alpha;
            yield return null;
        }

        SceneManager.LoadScene("Loading_Out");
    }
}