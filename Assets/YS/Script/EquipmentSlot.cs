using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    // ���⿡ new �ٿ��� �� �Ǿ �ϴ� Slot�ʿ� virtual ���̰� ���⼱ override�� �۵� ��Ŵ
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (item != null) // �������� ������
        {
            Debug.Log("������ ����");
            OldInventory.instance.AddItem(this.item);
        }
        else { Debug.Log("������ ����"); } // ������ �ƹ� �͵� �� ��
    }
}