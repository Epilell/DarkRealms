using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/Health")]
public class ItemHealingEfffect : ItemEffect
{
    // public Item item;

    public override bool ExecuteRole()
    {
        /*// HealthBar ��ü ����
        HealthBar healthBar = GameObject.FindObjectOfType<HealthBar>();
        // HealthBar ��ü�� IncreaseHp �޼��带 ȣ���Ͽ� ü�� ȸ��
        healthBar.IncreaseHp(item.effectPoint);*/
        Debug.Log("ȸ��");

        return true;
    }
}