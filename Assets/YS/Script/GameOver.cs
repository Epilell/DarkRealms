using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Rito.InventorySystem;

public class GameOver : MonoBehaviour
{
    public Inventory Inventory;
    public Player player;
    public P_Data p_data;
    public Image gameOverImg, screen;
    public Button goMainBtn;

    private float time = 0f;
    private float fadeTime = 5f;

    // 코루틴으로 자체 동작에서 TimeUI에서 호출만 하도록 변경
    public void PlayerGameOver()
    {
        player.CurrentHp =0; // 죽음
        FindObjectOfType<HealthBar>().ChangeHP(); // 체력바 변경

        for (int i = 0; i < Inventory._Items.Length; i++) { Inventory.Remove(i); } // 죽으면 인벤토리 내 모든 아이템 제거

        StartCoroutine(GameOverCoroutine());
    }

    public IEnumerator GameOverCoroutine() // 게임 오버 나타낼 코루틴
    {
        gameOverImg.gameObject.SetActive(true); // 이미지 활성화
        screen.gameObject.SetActive(true); // 이미지 활성화
        goMainBtn.gameObject.SetActive(true); // 버튼 활성화

        Color gameOverAlpha = gameOverImg.color; // 색상값
        Color screenAlpha = screen.color; // 색상값

        // 페이드 아웃
        while (gameOverAlpha.a < fadeTime)
        {
            time += Time.deltaTime / fadeTime; // 시간 계산

            // 투명도 증가
            gameOverAlpha.a = Mathf.Lerp(0f, 1f, time);
            screenAlpha.a = Mathf.Lerp(0f, 0.7f, time);

            // 이미지의 색상 정보 변경
            gameOverImg.color = gameOverAlpha;
            screen.color = screenAlpha;

            // 버튼
            float buttonAlpha = Mathf.Lerp(0f, 0.8f, time);
            Color buttonColor = goMainBtn.image.color;
            buttonColor.a = buttonAlpha;
            goMainBtn.image.color = buttonColor;

            yield return null; // 한 프레임 대기
        }

        yield return null;
    }
}