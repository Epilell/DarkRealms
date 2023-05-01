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
        // HealthBar 객체 생성
        HealthBar healthBar = GameObject.FindObjectOfType<HealthBar>();
        // HealthBar 객체의 IncreaseHp 메서드를 호출하여 체력 회복
        healthBar.IncreaseHp(healingPoint);

        return true;
    }
}