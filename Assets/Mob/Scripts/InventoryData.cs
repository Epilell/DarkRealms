using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Data", menuName = "ScriptableObject/Inventroy Data")]
public class InventoryData : ScriptableObject
{
    public List<Item> items = new();  // �κ��丮�� �ִ� ������ ����Ʈ

}
