using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    //����ٰ� �������� ���� �ڵ带 ���� ��������?
    /*
    new public void OnPointerUp(PointerEventData eventData)
    {
        
        if (item != null)  // �������� ������
        {
            bool isUse = item.Use();  // ������ ���

            if (isUse)  // ���������
            {
                if (item.itemType != ItemType.Equipment)
                {
                    if (item.itemName == "apple" || item.itemName == "cherry")
                    {
                        p.P_CurrentHp += item.effectPoint;
                    }
                    Inventory.instance.RemoveItem(slotNum);  // �κ��丮���� ����
                }
                else
                {
                    EquipmentInventory equipmentInventory = GameObject.FindObjectOfType<EquipmentInventory>();  // �ν��Ͻ�
                    // Debug.Log(equipmentInventory); // == Player
                    Slot targetSlot = equipmentInventory.FindEmptySlot(item.itemName);  // ��� ������ ã�Ƽ� �ҷ���

                    if (targetSlot != null) // ������ ��ȯ ������
                    {
                        equipmentInventory.AddItems(item, targetSlot);  // ��� ���Կ� ������ �߰�
                        equipmentInventory.RemoveItem(slotNum);  // ���� ���Կ��� ������ ����
                    }
                }
            }
        }
        else  // �������� ������
        {
            Debug.Log("��ĭ!");  // �α� ���
        }
}*/
}