using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Data", menuName = "ScriptableObject/Inventroy Data")]
public class InventoryData : ScriptableObject
{
    public List<Item> items = new();  // 인벤토리에 있는 아이템 리스트

}
