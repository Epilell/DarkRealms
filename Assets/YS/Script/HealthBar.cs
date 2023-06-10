using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hpbar, hpbar2; // ü�¹�
    public float maxHp;
    public float currentHp;
    public Text textObj, textObj2; // ü�� �ؽ�Ʈ
    public Player player; // �÷��̾� ��ü

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        // ü�¹� �ʱ�ȭ
        hpbar.value = (float)player.CurrentHp / (float)player.MaxHP;
        hpbar2.value = (float)player.CurrentHp / (float)player.MaxHP;
    }

    private void Update()
    {
        if (player != null)
        {
            maxHp = player.MaxHP;
            currentHp = player.CurrentHp;

            // ���� ü���� �ִ� ü���� ���� �ʰ�
            if (currentHp > maxHp)
            {
                currentHp = maxHp;
            }

            // ���� ü���� 0 �Ʒ��� �������� �ʰ�
            if (currentHp < 0)
            {
                currentHp = 0;
            }

            ChangeHP();
        }
        else
        {
            Debug.LogError("Player ��ũ��Ʈ ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }


    /*private void Start()
    {
        Debug.Log("��ŸƮ!");
        // ü�¹� �ʱ�ȭ
        GameObject playerObject = GameObject.Find("Player(Clone)");
        player = FindObjectOfType<PlayerSpawnSystem>().playerObject.GetComponent<Player>();
        Debug.Log("�÷��̾�:" + player);
        hpbar.value = (float)currentHp / (float)maxHp;
        hpbar2.value = (float)currentHp / (float)maxHp;
        Debug.Log("ü��:" + hpbar2.value);
    }

    private void Update()
    {
        maxHp = player.MaxHP;
        currentHp = player.CurrentHp;
        Debug.Log("���� ü��:" + currentHp);
        if (currentHp > maxHp) // ���� ü���� �ִ� ü���� ���� �ʰ�
        {
            currentHp = maxHp;
        }

        if (currentHp < 0) // ���� ü���� 0 �Ʒ��� �������� �ʰ�
        {
            currentHp = 0;
        }

        ChangeHP();
    }*/

    public void ChangeHP() // ü�¹� ����
    {
        textObj.text = currentHp.ToString() + "/" + maxHp.ToString(); // ü�� �ؽ�Ʈ ����
        hpbar.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);  // ü�� ����

        textObj2.text = currentHp.ToString() + "/" + maxHp.ToString(); // ü�� �ؽ�Ʈ ����
        hpbar2.value = Mathf.Lerp(hpbar.value, (float)currentHp / (float)maxHp, Time.deltaTime * 100);  // ü�� ����
    }
}