using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/Health")]
public class ItemHealingEfffect : ItemEffect
{
    public int healingPoint = 0;

    public override bool ExecuteRole()
    {
        // HealthBar ��ü ����
        HealthBar healthBar = GameObject.FindObjectOfType<HealthBar>();

        // HealthBar ��ü�� DecreaseHp �޼��� ȣ��
        healthBar.DecreaseHp(healingPoint);
        Debug.Log("�÷��̾� ü�� " + healingPoint + "��ŭ ȸ��");  // üũ�� �α� ���
        return true;
    }
}