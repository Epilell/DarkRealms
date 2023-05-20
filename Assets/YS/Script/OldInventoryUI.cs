using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldInventoryUI : MonoBehaviour
{
    protected OldInventory inventory;  // Inventory Ŭ������ �ν��ϸ� �����ϴ� ����
    public GameObject inventoryPanel;  // �κ��丮 UI �г� ����

    //�������
    protected bool activeInventory = false;  // �κ��丮 UI �г��� �����ִ���?
    protected bool activeProfile = false;
    protected bool activeSkillGroup = false;
    // �������. ActiveUI�� �ϴ� ������µ�, ���߿� �������. EscapeInventoryUI���� ���� �߻�: �̰͵� ���ο� ��ũ��Ʈ�� �� ����?

    protected Slot[] slots;  // �κ��丮�� ���Ե��� �����ϴ� �迭 ����
    public Transform slotHolder;  // ���Ե��� ��� �ִ� �θ� ������Ʈ ����

    public void Start()
    {
        inventory = OldInventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();  // ���� �ʱ�ȭ
        inventory.onSlotCountChange += SlotChange;  // ���� ������ ����� ������ SlotChange() �Լ� ȣ��
        inventory.onChangeItem += RedrawSlotUI;  // �������� �߰��ǰų� ���ŵ� ������ RedrawSlotUI() �Լ� ȣ��
        inventoryPanel.SetActive(false);  // �κ��丮 ��Ȱ��ȭ
    }

    public void SlotChange(int val)
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

    public void RedrawSlotUI()
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
}