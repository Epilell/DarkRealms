using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public Slider progressbar;
    public Text loadText;
    public string scene;
    public FadeOut fadeOut;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;

            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }
            else if (operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if (progressbar.value >= 1f)
            {
                loadText.text = "Press Spacebar";
            }

            if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                loadText.text = "";
                yield return StartCoroutine(fadeOut.FadeFlow());
                operation.allowSceneActivation = true;
            }
        }
    }
}