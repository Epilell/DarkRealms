using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Storage Data", menuName = "ScriptableObject/Storage Data")]
public class StorageData : ScriptableObject
{
    public List<Item> items = new();  // 인벤토리에 있는 아이템 리스트
}
