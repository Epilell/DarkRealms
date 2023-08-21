using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment Data", menuName = "ScriptableObject/Equipment Data")]
public class EquipmentData : ScriptableObject
{
    public List<Item> EqItems = new();  // 인벤토리에 있는 아이템 리스트

}
