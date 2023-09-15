using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Rito.InventorySystem;

public class GameOver : MonoBehaviour
{
    public FadeOut fadeOut;
    public Player player;
    public Inventory Inventory;
    public GameObject UIHitEffect;
    public Button goMainBtn; // screen 대신 버튼으로
    public Image gameOverImg;

    public bool isGameOver = false;

    private void Start() { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }

    public void PlayerGameOver()
    {
        if (isGameOver) return; // 중복 실행되지 않게 함

        FindObjectOfType<Player>().P_TakeDamage(10000); // 플레이어 사망

        StartCoroutine(GameOverCoroutine());
    }

    public IEnumerator GameOverCoroutine() // 게임 오버 나타낼 코루틴
    {
        if (isGameOver) yield break; // 중복 실행되지 않게 함

        isGameOver = true;

        // for (int i = 0; i < Inventory._Items.Length; i++) { Inventory.Remove(i); } // 죽으면 인벤토리 내 모든 아이템 제거

        gameOverImg.gameObject.SetActive(true); // 이미지 활성화
        goMainBtn.gameObject.SetActive(true); // 버튼 활성화

        // 색상값
        Color gameOverAlpha = gameOverImg.color;
        Color buttonAlpha = goMainBtn.GetComponent<Image>().color;

        // 시간 초기화
        float time = 0f;
        float fadeTime = 2.5f;

        // 페이드 아웃
        while (gameOverAlpha.a < 1f)
        {
            time += Time.deltaTime / fadeTime; // 시간 계산

            // 투명도 증가
            gameOverAlpha.a = Mathf.Lerp(0f, 1f, time);
            buttonAlpha.a = Mathf.Lerp(0f, 0.7f, time);

            // 이미지의 색상 정보 변경
            gameOverImg.color = gameOverAlpha;
            goMainBtn.GetComponent<Image>().color = buttonAlpha;

            yield return null; // 한 프레임 대기
        }

        goMainBtn.onClick.AddListener(() =>
        {
            FindObjectOfType<SoundManager>().PlaySound("Play");
            fadeOut.Fade();
        });
    }
}