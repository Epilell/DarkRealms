using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Equipment/Weapon")]
public class ItemEquipmentEffect : ItemEffect
{
    public override bool ExecuteRole()
    {
        // �賶���� ���� Ŭ���ϸ� ȣ��Ǿ �Ʒ��� ������ �����. �ϴ� �׽�Ʈ�� �α� ���
        Debug.Log("Equip");
        return true;
    }
}