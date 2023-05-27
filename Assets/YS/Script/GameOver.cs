using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public OldInventory oldInventory;
    public Player player;
    private float playTime = 10f; // �׽�Ʈ�� 10��

    private void Start() { StartCoroutine(GameOverRoutine()); }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(playTime); // �÷��� �ð���ŭ ������ �Ʒ� �ڵ� ����

        player.CurrentHp = 0; // ����: ü�� 0
        FindObjectOfType<HealthBar>().ChangeHP(); // ü�¹� ����

        oldInventory.RemoveAllItem(); // ������ �κ��丮 �� ��� ������ ����

        // 3�� �� ���� �� ���̵� �ƿ�
        Time.timeScale = 0;

        int count = 0;
        while (count < 1800)
        {
            count++;
            yield return null; // ������ ���� �� ��
        }

        // �ð� �ٽ� �帣�� ���̵� �ƿ�
        Time.timeScale = 1f;

        FindObjectOfType<FadeOut>().Fade(); // FadeOut Ŭ������ �ν��Ͻ��� ã�� Fade() ȣ��
    }
}