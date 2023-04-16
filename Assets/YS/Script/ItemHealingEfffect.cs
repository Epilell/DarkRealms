using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/Health")]
public class ItemHealingEfffect : ItemEffect
{
    public int healingPoint = 0;

    public override bool ExecuteRole()
    {
        Debug.Log("플레이어 체력 " + healingPoint + "만큼 회복");
        return true;
    }
}