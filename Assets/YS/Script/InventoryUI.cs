using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;  // Inventory 클래스의 인스턴를 저장하는 변수
    public GameObject inventoryPanel;  // 인벤토리 UI 패널 변수
    bool activeInventory = false;  // 인벤토리 UI 패널이 열려있는지?

    public Slot[] slots;  // 인벤토리의 슬롯들을 저장하는 배열 생성
    public Transform slotHolder;  // 슬롯들을 담고 있는 부모 오브젝트 생성

    public Slot[] slots2;  // 인벤토리의 슬롯들을 저장하는 배열 생성//p
    public Transform slotHolder2;  // 슬롯들을 담고 있는 부모 오브젝트 생성//p

    private void Start()
    {
        inventory = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();  // 슬롯 초기화
        inventory.onSlotCountChange += SlotChange;  // 슬롯 개수가 변경될 때마다 SlotChange() 함수 호출
        inventory.onChangeItem += RedrawSlotUI;  // 아이템이 추가되거나 제거될 때마다 RedrawSlotUI() 함수 호출
        slots2 = slotHolder2.GetComponentsInChildren<Slot>();  // 슬롯 초기화//p
        inventory.onChangeItem2 += RedrawSlotUI2;  // 아이템이 추가되거나 제거될 때마다 RedrawSlotUI2() 함수 호출//p
        inventoryPanel.SetActive(activeInventory);  // 인벤토리 비활성화
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;  // 슬롯 번호 설정

            if (i < inventory.SlotCount)
                slots[i].GetComponent<Button>().interactable = true;  // 슬롯 활성화
            else
                slots[i].GetComponent<Button>().interactable = false;  // 슬롯 비활성화
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))  // Tab 키가 눌리면
        {
            // activeInventory 반전(닫혀있으면 열고, 열려있으면 닫기)
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();  // 슬롯 초기화
        }

        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];  // 슬롯에 아이템 추가
            slots[i].UpdateSlotUI();  // 슬롯 UI 업데이트
        }
    }

    void RedrawSlotUI2()//p
    {

        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots2[i].item = inventory.items[i];  // 슬롯에 아이템 추가
            slots2[i].UpdateSlotUI();  // 슬롯 UI 업데이트
        }
    }
}