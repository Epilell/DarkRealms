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
        slots = slotHolder.GetComponentsInChildren<Slot>();  // ���� �ʱ�ȭ
        inventory.onSlotCountChange += SlotChange;  // ���� ������ ����� ������ SlotChange() �Լ� ȣ��
        inventory.onChangeItem += RedrawSlotUI;  // �������� �߰��ǰų� ���ŵ� ������ RedrawSlotUI() �Լ� ȣ��
        inventoryPanel.SetActive(activeInventory);  // �κ��丮 ��Ȱ��ȭ
    }
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))  // Tab Ű�� ������
        {
            // activeInventory ����(���������� ����, ���������� �ݱ�)
            activeInventory = !activeInventory;
            activeProfile = !activeProfile;
            activeSkillGroup = !activeSkillGroup;
            inventoryPanel.SetActive(activeInventory);
        }
    }
}
