using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/Health")]
public class ItemHealingEfffect : ItemEffect
{
    public int healingPoint = 0;

    public override bool ExecuteRole()
    {
        Debug.Log("�÷��̾� ü�� " + healingPoint + "��ŭ ȸ��");
        return true;
    }
}