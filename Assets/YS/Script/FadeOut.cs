using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image Panel;
    public string scene;
    float time = 0f;
    float F_time = 1f;

    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color alpha = Panel.color;

        // 페이드 아웃
        while(alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0f, 1f, time);
            Panel.color = alpha;
            yield return null;
        }

        SceneManager.LoadScene(scene);

        /*time = 0f;

        yield return new WaitForSeconds(1f);

        // 페이드 인
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1f, 0f, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);

        yield return null;*/
    }
}