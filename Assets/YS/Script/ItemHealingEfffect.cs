using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/Health")]
public class ItemHealingEfffect : ItemEffect
{
    public int healingPoint = 0;

    public override bool ExecuteRole()
    {
        // HealthBar 객체 생성
        HealthBar healthBar = GameObject.FindObjectOfType<HealthBar>();

        // HealthBar 객체의 DecreaseHp 메서드 호출
        healthBar.DecreaseHp(healingPoint);
        Debug.Log("플레이어 체력 " + healingPoint + "만큼 회복");  // 체크용 로그 출력
        return true;
    }
}