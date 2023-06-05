using System.Collections;
using UnityEngine;
using Rito.InventorySystem;

public class GameOver : MonoBehaviour
{
    public Inventory Inventory;
    public Player player;

    // 코루틴으로 자체 동작에서 TimeUI에서 호출만 하도록 변경
    public void PlayerGameOver()
    {
        player.CurrentHp -= 10000; // 죽음
        FindObjectOfType<HealthBar>().ChangeHP(); // 체력바 변경

        for (int i = 0; i < Inventory._Items.Length; i++) { Inventory.Remove(i); } // 죽으면 인벤토리 내 모든 아이템 제거

        Time.timeScale = 0;

        // FindObjectOfType<FadeOut>().Fade(); // FadeOut 클래스의 인스턴스를 찾아 Fade() 호출
    }

    /*private void Start() { StartCoroutine(GameOverRoutine()); }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(playTime); // 플레이 시간만큼 지나면 아래 코드 실행

        player.CurrentHp = 0; // 죽음: 체력 0
        FindObjectOfType<HealthBar>().ChangeHP(); // 체력바 변경

        for (int i = 0; i < Inventory._Items.Length; i++)
        {
            Inventory.Remove(i); // 죽으면 인벤토리 내 모든 아이템 제거
        }

        // 3초 간 정지 후 페이드 아웃
        Time.timeScale = 0;

        int count = 0;
        while (count < 1800)
        {
            count++;
            yield return null; // 없으면 정지 안 함
        }

        // 시간 다시 흐르며 페이드 아웃
        Time.timeScale = 1f;

        FindObjectOfType<FadeOut>().Fade(); // FadeOut 클래스의 인스턴스를 찾아 Fade() 호출
    }*/
}