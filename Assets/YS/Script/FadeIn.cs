using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    public Image panel;
    float time = 0f;
    float fadeTime = 1f;

    private void Start()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public IEnumerator FadeInCoroutine()
    {
        panel.gameObject.SetActive(true);
        Color alpha = panel.color;

        // 페이드 인
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(1f, 0f, time);
            panel.color = alpha;
            yield return null;
        }

        panel.gameObject.SetActive(false);

        yield return null;
    }
}
