using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public OldInventory oldInventory; // �κ��丮 ������ ��ü(���⼭�� �÷��̾�)
    public Player player; // �÷��̾� ��ü
    private float playTime = 10f; // ������ 10������ ���߿� 10������ �ٲٸ� ��

    private void Start() { StartCoroutine(GameOverRoutine()); }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(playTime); // �÷��� �ð���ŭ ������ �Ʒ� �ڵ� ����

        oldInventory.RemoveAllItem(); // �κ��丮�� ��� ������ ����

        // ���� ����
        player.CurrentHp = 0;
        FindObjectOfType<HealthBar>().ChangeHP();

        Time.timeScale = 0;

        int count = 0;
        while (count < 1800) // 3�� �� ���� �� ���̵� �ƿ�
        {
            count++;
            yield return null; // ������ ���� �� ��
        }

        Time.timeScale = 1f; // �ð� �ٽ� �帧

        FindObjectOfType<FadeOut>().Fade(); // FadeOut Ŭ������ �ν��Ͻ��� ã�� Fade() ȣ��
    }
}