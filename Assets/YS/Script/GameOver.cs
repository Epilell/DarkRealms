using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Rito.InventorySystem;

public class GameOver : MonoBehaviour
{
    public FadeOut fadeOut;
    public Inventory Inventory;
    public Player player;
    public Image gameOverImg, screen;
    public Button goMainBtn;
    public TextMeshProUGUI goMainBtnTxt;

    public bool isGameOver = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // 코루틴으로 자체 동작에서 TimeUI에서 호출만 하도록 변경
    public void PlayerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        player.CurrentHp -= 10000; // 죽음
        FindObjectOfType<HealthBar>().ChangeHP(); // 체력바 변경

        for (int i = 0; i < Inventory._Items.Length; i++) { Inventory.Remove(i); } // 죽으면 인벤토리 내 모든 아이템 제거

        StartCoroutine(GameOverCoroutine());
    }

    public IEnumerator GameOverCoroutine() // 게임 오버 나타낼 코루틴
    {
        gameOverImg.gameObject.SetActive(true); // 이미지 활성화
        screen.gameObject.SetActive(true); // 이미지 활성화
        goMainBtn.gameObject.SetActive(true); // 버튼 활성화

        // 색상값
        Color gameOverAlpha = gameOverImg.color;
        Color screenAlpha = screen.color;
        Color buttonAlpha = goMainBtn.GetComponent<Image>().color;
        Color textAlpha = goMainBtnTxt.color;

        // 시간 초기화
        float time = 0f;
        float fadeTime = 2.4f;

        // 페이드 아웃
        while (gameOverAlpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime; // 시간 계산

            // 투명도 증가
            gameOverAlpha.a = Mathf.Lerp(0f, 1f, time);
            screenAlpha.a = Mathf.Lerp(0f, 0.7f, time);
            buttonAlpha.a = Mathf.Lerp(0f, 1f, time);
            textAlpha.a = Mathf.Lerp(0f, 1f, time);

            // 이미지의 색상 정보 변경
            gameOverImg.color = gameOverAlpha;
            screen.color = screenAlpha;
            goMainBtn.GetComponent<Image>().color = buttonAlpha;
            goMainBtnTxt.color = textAlpha;

            yield return null; // 한 프레임 대기
        }

        // Time.timeScale = 0;

        goMainBtn.onClick.AddListener(GoToNextScene); // 버튼 클릭 이벤트에 GoToMainMenu 함수를 추가
    }

    private void GoToNextScene()
    {
        // Time.timeScale = 1;
        fadeOut.Fade();
    }
}