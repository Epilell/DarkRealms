using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public OldInventory oldInventory; // 인벤토리 적용한 객체(여기서는 플레이어)
    public Player player; // 플레이어 객체
    private float playTime = 10f; // 지금은 10초지만 나중에 10분으로 바꾸면 됨

    private void Start() { StartCoroutine(GameOverRoutine()); }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(playTime); // 플레이 시간만큼 지나면 아래 코드 실행

        oldInventory.RemoveAllItem(); // 인벤토리의 모든 아이템 제거

        // 게임 오버
        player.CurrentHp = 0;
        FindObjectOfType<HealthBar>().ChangeHP();

        Time.timeScale = 0;

        int count = 0;
        while (count < 1800) // 3초 간 정지 후 페이드 아웃
        {
            count++;
            yield return null; // 없으면 정지 안 함
        }

        Time.timeScale = 1f; // 시간 다시 흐름

        FindObjectOfType<FadeOut>().Fade(); // FadeOut 클래스의 인스턴스를 찾아 Fade() 호출
    }
}