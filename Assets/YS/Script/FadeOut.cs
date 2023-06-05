using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class FadeOut : MonoBehaviour
{
    public Image panel; // 페이드 아웃에 사용할 이미지
    float time = 0f; // 경과 시간
    float fadeTime = 1f; // 페이드 아웃 시간
    public SceneAsset scene; // 씬 가져오기

    public void Fade() // 페이드 아웃 함수
    {
        StartCoroutine(FadeFlow()); // 코루틴 시작
    }

    public IEnumerator FadeFlow() // 페이드 아웃 코루틴 함수
    {
        panel.gameObject.SetActive(true); // 이미지 활성화
        Color alpha = panel.color; // 색상값

        // 페이드 아웃
        while (alpha.a < fadeTime)
        {
            time += Time.deltaTime / fadeTime; // 시간 계산
            alpha.a = Mathf.Lerp(0f, 1f, time); // 알파값을 서서히 증가시킴(서서히 어두워짐)
            panel.color = alpha; // 이미지의 색상 정보변경
            yield return null; // 한 프레임 대기
        }

        SceneManager.LoadScene(scene.name); // 씬 전환
    }
    
    public void OnClickFade(Scene scene) // 페이드 아웃 함수
    {
        StartCoroutine(OnClickFadeFlow(scene)); // 코루틴 시작
    }

    public IEnumerator OnClickFadeFlow(Scene targetScene) // 페이드 아웃 코루틴 함수
    {
        panel.gameObject.SetActive(true); // 이미지 활성화
        Color alpha = panel.color; // 색상값

        // 페이드 아웃
        while (alpha.a < fadeTime)
        {
            time += Time.deltaTime / fadeTime; // 시간 계산
            alpha.a = Mathf.Lerp(0f, 1f, time); // 알파값을 서서히 증가시킴(서서히 어두워짐)
            panel.color = alpha; // 이미지의 색상 정보변경
            yield return null; // 한 프레임 대기
        }

        SceneManager.LoadScene(targetScene.name); // 씬 전환
    }
}