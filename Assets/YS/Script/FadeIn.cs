using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    public Text text;
    public Image panel;  // 페이드 인에 사용할 이미지 컴포넌트
    float time = 0f;  // 페이드 인 실행 시간
    float fadeTime = 1f;

    private void Start()
    {
        StartCoroutine(FadeInCoroutine()); // 코루틴 시작

        if (SceneManager.GetActiveScene().name == "LoadingOut")  // 테스트용 출력_230506
        {
            Coin coin = new Coin();
            // Debug.Log(coin.GetCoin());
            text.text = "Coin: " + coin.GetCoin().ToString();
        }
    }

    public IEnumerator FadeInCoroutine()
    {
        panel.gameObject.SetActive(true); // 이미지 컴포넌트 활성화
        Color alpha = panel.color; // 이미지의 색상 정보를 가져옴

        // 페이드 인
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
