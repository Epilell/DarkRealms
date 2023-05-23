using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    // 여기에 new 붙여도 안 되어서 일단 Slot쪽에 virtual 붙이고 여기선 override로 작동 시킴
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (item != null) // 아이템이 있으면
        {
            Debug.Log("아이템 있음");
            OldInventory.instance.AddItem(this.item);
        }
        else { Debug.Log("아이템 없음"); } // 없으면 아무 것도 안 함
    }
}