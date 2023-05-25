using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeInventoryUI : OldInventoryUI
{
    new protected void Start()
    {
        inventory = OldInventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();  // 슬롯 초기화
        inventory.onSlotCountChange += SlotChange;  // 슬롯 개수가 변경될 때마다 SlotChange() 함수 호출
        inventory.onChangeItem += RedrawSlotUI;  // 아이템이 추가되거나 제거될 때마다 RedrawSlotUI() 함수 호출
        inventoryPanel.SetActive(activeInventory);  // 인벤토리 비활성화
    }
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))  // Tab 키가 눌리면
        {
            // activeInventory 반전(닫혀있으면 열고, 열려있으면 닫기)
            activeInventory = !activeInventory;
            activeProfile = !activeProfile;
            activeSkillGroup = !activeSkillGroup;
            inventoryPanel.SetActive(activeInventory);
        }
    }
}
