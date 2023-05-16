using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;  // Inventory Ŭ������ �ν��ϸ� �����ϴ� ����
    [SerializeField]
    private GameObject SkillGroup;  // ��ų UI
    //[SerializeField]
    //private GameObject profile_in, profile_out;  // ������ UI
    [SerializeField]
    private GameObject profile_out;
    public GameObject inventoryPanel;  // �κ��丮 UI �г� ����
    protected bool activeInventory = false;  // �κ��丮 UI �г��� �����ִ���?
    protected bool activeProfile = false;
    protected bool activeSkillGroup = false;

    protected Slot[] slots;  // �κ��丮�� ���Ե��� �����ϴ� �迭 ����
    public Transform slotHolder;  // ���Ե��� ��� �ִ� �θ� ������Ʈ ����

    protected void Start()
    {
        inventory = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();  // ���� �ʱ�ȭ
        inventory.onSlotCountChange += SlotChange;  // ���� ������ ����� ������ SlotChange() �Լ� ȣ��
        inventory.onChangeItem += RedrawSlotUI;  // �������� �߰��ǰų� ���ŵ� ������ RedrawSlotUI() �Լ� ȣ��
        inventoryPanel.SetActive(activeInventory);  // �κ��丮 ��Ȱ��ȭ
        //profile_in.SetActive(activeProfile);
    }

    protected void SlotChange(int val)
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

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))  // Tab Ű�� ������
        {
            // activeInventory ����(���������� ����, ���������� �ݱ�)
            activeInventory = !activeInventory;
            activeProfile = !activeProfile;
            activeSkillGroup = !activeSkillGroup;

            inventoryPanel.SetActive(activeInventory);
            //profile_in.SetActive(activeProfile);
            profile_out.SetActive(!activeProfile);
            SkillGroup.SetActive(!activeSkillGroup);
        }
    }

    protected void RedrawSlotUI()
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