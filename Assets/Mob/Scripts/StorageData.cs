using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Storage Data", menuName = "ScriptableObject/Storage Data")]
public class StorageData : ScriptableObject
{
    public List<Item> items = new();  // �κ��丮�� �ִ� ������ ����Ʈ
}
