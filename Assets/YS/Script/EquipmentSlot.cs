using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        bool isUse = item.Use();  // 아이템 사용
        // 사용했으면
        if (isUse)
        {
            if (item.itemType != ItemType.Equipment)
            {
                Inventory.instance.RemoveItem(slotNum); // 인벤토리에서 제거
            }
            else
            {
                // Debug.Log("장비 아이템, 오버라이드 구현");
                EquipmentInventory equipmentInventory = GameObject.FindObjectOfType<EquipmentInventory>(); // 인스턴스
                // Debug.Log(equipmentInventory); // Player
                Slot targetSlot = equipmentInventory.FindEmptySlot(item.itemName); // 대상 슬롯을 찾아서 불러옴
                if (targetSlot != null) // 슬롯을 반환 받으면
                {
                    equipmentInventory.AddItems(item, targetSlot); // 대상 슬롯에 아이템 추가
                    equipmentInventory.RemoveItem(slotNum); // 현재 슬롯에서 아이템 제거
                }
            }
        }
    }
}