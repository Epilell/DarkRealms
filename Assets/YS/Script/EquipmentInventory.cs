using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public static EquipmentInventory instance;  // �κ��丮 �ν��Ͻ�
    //public EquipmentSlot slot, slot2, slot3, slot4, slot5, slot6, slot7, slot8;
    private EquipmentSlot[] slots;
    public P_Data p;//�̰� �������� �ڵ带 ���� ����� �ʿ����
    public EquipmentData Eqdata;
    public Transform slotHolder;
    private void Awake()
    {
        if (instance != null)  // �κ��丮 �ν��Ͻ��� �����ϸ�
        {
            Destroy(gameObject);  // �ߺ� ���� ������ ���� ���� ���� ������Ʈ�� �ı�
            return;  // ����
        }
        instance = this;  // �ν��Ͻ��� �������� ������ ���� �ν��Ͻ��� �Ҵ�
    }
    private void Start() // ��� ���� �ʱ�ȭ
    {
        slots = slotHolder.GetComponentsInChildren<EquipmentSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                slots[i].item = Eqdata.EqItems[i];
            }
        }
    }

    public Slot FindEmptySlot(String itemName)  // �� ���� ã��
    {
        if (itemName == "helmet")
        {
            return slots[0];
        }
        else if (itemName == "armor" /*|| itemName == "armor2"*/)
        {
            return slots[1];
        }
        else if (itemName == "knee")
        {
            return slots[2];
        }
        else if (itemName == "shoes")
        {
            return slots[3];
        }
        else if (itemName == "rifle")
        {
            return slots[4];
        }
        else if (itemName == "shotgun")
        {
            return slots[5];
        }
        else if (itemName == "mobDrop")
        {
            return slots[6];
        }
        else return null;
    }

    public void AddItems(Item item, Slot targetSlot)  // �� ���Կ� ����
    {
        if (targetSlot != null && targetSlot.item == null) // �ش� ���Կ� �̹� �������� ������ �߰����� ����
        {
            targetSlot.item = item;
            targetSlot.UpdateSlotUI();

            if (item.itemName == "helmet")
            {
                Debug.Log("���� ����");
            }
            else if (item.itemName == "armor")
            {
                Debug.Log("���� ����");
                // p.P_MaxHp += item.effectPoint;
                //�÷��̾�� ����ȿ�� �ִ°͵� ���ο� ��ũ��Ʈ�� ���� ���⼭�� ȣ�⸸ �Ͻÿ�
            }
            else if (item.itemName == "knee")
            {
                Debug.Log("���� ��ȣ�� ����");
            }
            else if (item.itemName == "shoes")
            {
                Debug.Log("�Ź� ����");
            }
            else if (item.itemName == "shotgun")
            {
                Debug.Log("���� ����");
            }
            else if (item.itemName == "shotgun")
            {
                Debug.Log("��ź�� ����");
            }
            else if (item.itemName == "mobDrop")
            {
                Debug.Log("���� ����");
            }
        }
        else { }
    }

    public void RemoveItem(int _index)
    {
        Inventory.instance.items.RemoveAt(_index);  // ������ ����Ʈ���� �ش� �ε����� ������ ����
        Inventory.instance.onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
    }
}