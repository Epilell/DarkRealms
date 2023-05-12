using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public Slot slot, slot2, slot3, slot4, slot5, slot6, slot7, slot8;
    public P_Data p;

    private void Start() // ��� ���� �ʱ�ȭ
    {
        slot.item = null;
        slot2.item = null;
        slot3.item = null;
        slot4.item = null;
        slot5.item = null;
        slot6.item = null;
        slot7.item = null;
        slot8.item = null;
    }

    public Slot FindEmptySlot(String itemName)  // �� ���� ã��
    {
        if (itemName == "helmet")
        {
            return slot;
        }
        else if (itemName == "armor" /*|| itemName == "armor2"*/)
        {
            return slot2;
        }
        else if (itemName == "knee")
        {
            return slot3;
        }
        else if (itemName == "shoes")
        {
            return slot4;
        }
        else if (itemName == "rifle")
        {
            return slot6;
        }
        else if (itemName == "shotgun")
        {
            return slot7;
        }
        else if (itemName == "mobDrop")
        {
            return slot8;
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