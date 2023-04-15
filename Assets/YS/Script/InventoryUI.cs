using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
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
        inventoryPanel.SetActive(activeInventory);
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.SlotCount)
                slots[i].GetComponent<Button>().interactable = true;  // ���� Ȱ��ȭ
            else
                slots[i].GetComponent<Button>().interactable = false;  // ���� ��Ȱ��ȭ
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))  // Tab Ű�� ������
        {
            // activeInventory ����(���������� ����, ���������� �ݱ�)
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }

    public void AddSlot()
    {
        // ���� Ȯ�� �������� ������ ���� 5ĭ Ȯ��
        if (inventory.SlotCount < 15)
        {
            inventory.SlotCount += 5;
        }
        // �뷮 �ʰ��ϴ��� üũ
        else Debug.Log("�κ��丮 ��ȭ!");

        // �׽�Ʈ ��ư�� ������ ������ Ȯ��Ǵ��� �׽�Ʈ
        /*
        if (inventory.SlotCount < 15)
        {
            inventory.SlotCount += 5;
        }
        */
    }
}