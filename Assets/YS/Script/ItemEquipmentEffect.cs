using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Equipment/Weapon")]
public class ItemEquipmentEffect : ItemEffect
{
    public override bool ExecuteRole()
    {
        // 배낭에서 무기를 클릭하면 호출되어서 아래의 문장이 실행됨
        // Debug.Log("Equip");
        return true;
    }
}