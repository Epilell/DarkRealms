using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment Data", menuName = "ScriptableObject/Equipment Data")]
public class EquipmentData : ScriptableObject
{
    public List<Item> EqItems = new();  // �κ��丮�� �ִ� ������ ����Ʈ

}
