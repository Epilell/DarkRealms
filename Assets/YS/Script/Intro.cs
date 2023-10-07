using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Image fadeImg;

    private float time = 0f;
    private float fadeTime = 1.2f;

    void Start() { StartCoroutine(IntroCoroutine()); }

    public IEnumerator IntroCoroutine()
    {
        fadeImg.gameObject.SetActive(true);
        Color alpha = fadeImg.color;

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(1f, 0f, time);
            fadeImg.color = alpha;
            yield return null;
        }

        yield return new WaitForSeconds(1.2f);

        time = 0f;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime;
            alpha.a = Mathf.Lerp(0f, 1f, time);
            fadeImg.color = alpha;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}