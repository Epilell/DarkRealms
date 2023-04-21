using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
  public Slider progressbar; // 로딩바로 사용할 슬라이더
  public Text loadText;
  public string scene; // 로드할 씬
  public FadeOut fadeOut; // 페이드 아웃 스크립트

  private void Start()
  {
    StartCoroutine(LoadScene()); // 코루틴 시작
  }

  IEnumerator LoadScene()
  {
    yield return null;
    AsyncOperation operation = SceneManager.LoadSceneAsync(scene); // 비동기 씬 로드 시작
    operation.allowSceneActivation = false; // 씬 로드가 완료되면 바로 전환하지 않도록 설정

    while (!operation.isDone) // 씬 로드가 완료될 때까지 반복
    {
      yield return null;

      if (progressbar.value < 0.9f) // 로딩 바가 90% 미만일 때
      {
        progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime); // 로딩 바 증가
      }
      else if (operation.progress >= 0.9f) // 씬 로드가 90% 이상일 때
      {
        progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime); // 로딩 바 증가
      }

      if (progressbar.value >= 1f) // 로딩 바가 100%일 때
      {
        loadText.text = "Press Spacebar"; // 로딩 텍스트 변경
      }

      if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && operation.progress >= 0.9f) // 로딩이 완료되었을 때 스페이스바를 누르면
      {
        loadText.text = ""; // 로딩 텍스트 숨기기
        yield return StartCoroutine(fadeOut.FadeFlow()); // 페이드 아웃 실행
        operation.allowSceneActivation = true; // 씬 전환 허용
      }
    }
  }
}