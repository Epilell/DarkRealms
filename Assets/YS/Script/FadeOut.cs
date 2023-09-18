using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public string scene; // 씬 가져오기
    public Image panel; // 페이드 아웃에 사용할 이미지
    public Image startImg; // Start 전용 이미지

    float time; // 경과 시간
    float fadeTime = 1f; // 페이드 아웃 시간

    public void Fade() { StartCoroutine(FadeFlow()); }

    public IEnumerator FadeFlow() // 페이드 아웃 코루틴 함수
    {
        if (this.gameObject.name == "StartBtn") // 메인 메뉴에서 Start 버튼을 누르면 추가 실행
        {
            startImg.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.8f);
        }

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

        SceneManager.LoadScene(scene); // 씬 전환
    }

    public IEnumerator BossFadeInOut() // 보스 전용
    {
        time = 0f;

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

        time = 0f;

        yield return new WaitForSeconds(2f); // 카메라 시점 커지는 동안 어둡게 유지

        while (alpha.a > 0f) // 이미지의 알파값이 0보다 크면 반복
        {
            time += Time.deltaTime / fadeTime; // 시간 증가
            alpha.a = Mathf.Lerp(1f, 0f, time); // 알파값을 서서히 감소시킴(서서히 투명해짐)
            panel.color = alpha; // 이미지의 색상 정보변경
            yield return null; // 한 프레임 대기
        }

        panel.gameObject.SetActive(false); // 이미지 비활성화

        yield return null; // 코루틴 종료
    }
}