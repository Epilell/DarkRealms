using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/Health")]
public class ItemHealingEfffect : ItemEffect
{
    public Item item;
    public int healingPoint = 0;

    public override bool ExecuteRole()
    {
        // HealthBar ��ü ����
        HealthBar healthBar = GameObject.FindObjectOfType<HealthBar>();
        // HealthBar ��ü�� IncreaseHp �޼��带 ȣ���Ͽ� ü�� ȸ��
        healthBar.IncreaseHp(healingPoint);

        return true;
    }
}