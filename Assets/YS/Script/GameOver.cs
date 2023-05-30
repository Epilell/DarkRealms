using System.Collections;
using UnityEngine;
using Rito.InventorySystem;

public class GameOver : MonoBehaviour
{
    public Inventory Inventory;
    public Player player;
    private float playTime = 10f; // �׽�Ʈ�� 10��

    private void Start() { StartCoroutine(GameOverRoutine()); }

    private IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(playTime); // �÷��� �ð���ŭ ������ �Ʒ� �ڵ� ����

        player.CurrentHp = 0; // ����: ü�� 0
        FindObjectOfType<HealthBar>().ChangeHP(); // ü�¹� ����
        for(int i = 0; i < Inventory._Items.Length; i++)
        {
            Inventory.Remove(i); // ������ �κ��丮 �� ��� ������ ����
        }
        

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