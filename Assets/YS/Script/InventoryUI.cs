using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    HealthBar hp;
    Inventory inventory;
    public GameObject inventoryPanel;
    bool activeInventory = false;

    public Slot[] slots;
    public Transform slotHolder;

    private void Start()
    {
        inventory = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inventory.onSlotCountChange += SlotChange;
        inventory.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;

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

    public void AddSlot()
    {
        // 슬롯 확장 아이템을 먹으면 슬롯 5칸 확장
        if (inventory.SlotCount < 15)
        {
            hp.IncreaseHp(30f);
            inventory.SlotCount += 5;
        }
        // 용량 초과하는지 체크
        else Debug.Log("인벤토리 포화!");
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }

        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}