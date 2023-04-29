using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;  // Inventory Ŭ������ �ν��ϸ� �����ϴ� ����
    public GameObject inventoryPanel;  // �κ��丮 UI �г� ����
    bool activeInventory = false;  // �κ��丮 UI �г��� �����ִ���?

    public Slot[] slots;  // �κ��丮�� ���Ե��� �����ϴ� �迭 ����
    public Transform slotHolder;  // ���Ե��� ��� �ִ� �θ� ������Ʈ ����

    public Slot[] slots2;  // �κ��丮�� ���Ե��� �����ϴ� �迭 ����//p
    public Transform slotHolder2;  // ���Ե��� ��� �ִ� �θ� ������Ʈ ����//p

    private void Start()
    {
        inventory = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();  // ���� �ʱ�ȭ
        inventory.onSlotCountChange += SlotChange;  // ���� ������ ����� ������ SlotChange() �Լ� ȣ��
        inventory.onChangeItem += RedrawSlotUI;  // �������� �߰��ǰų� ���ŵ� ������ RedrawSlotUI() �Լ� ȣ��
        slots2 = slotHolder2.GetComponentsInChildren<Slot>();  // ���� �ʱ�ȭ//p
        inventory.onChangeItem2 += RedrawSlotUI2;  // �������� �߰��ǰų� ���ŵ� ������ RedrawSlotUI2() �Լ� ȣ��//p
        inventoryPanel.SetActive(activeInventory);  // �κ��丮 ��Ȱ��ȭ
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;  // ���� ��ȣ ����

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

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();  // ���� �ʱ�ȭ
        }

        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];  // ���Կ� ������ �߰�
            slots[i].UpdateSlotUI();  // ���� UI ������Ʈ
        }
    }

    void RedrawSlotUI2()//p
    {

        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots2[i].item = inventory.items[i];  // ���Կ� ������ �߰�
            slots2[i].UpdateSlotUI();  // ���� UI ������Ʈ
        }
    }
}