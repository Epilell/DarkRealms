using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public Slot slot, slot2, slot3, slot4/*, slot5*/;
    public P_Data p;

    private void Start() // ��� ���� �ʱ�ȭ
    {
        slot.item = null;
        slot2.item = null;
        slot3.item = null;
        slot4.item = null;
        // slot5.item = null;
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
        else if (itemName == "rifle" || itemName == "shotgun")
        {
            return slot3;
        }
        else if (itemName == "shoes")
        {
            return slot4;
        }
        /*else if (itemName == "mobDrop")
        {
            return slot5;
        }*/
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
            else if (item.itemName == "armor" /*|| item.itemName == "armor2"*/)
            {
                Debug.Log("���� ����");
                p.P_MaxHp += item.effectPoint;
/*                HealthBar healthBar = GameObject.FindObjectOfType<HealthBar>();
                healthBar.maxHp += item.effectPoint;  // effectPoint��ŭ �ִ� ü�� ����
                healthBar.currentHp += item.effectPoint / 2;  // effectPoint�� ���ݸ�ŭ ���� ü�� ����*/
            }
            else if (item.itemName == "sword")
            {
                Debug.Log("���� ����");
            }
            else if (item.itemName == "shoes")
            {
                Debug.Log("�Ź� ����");
            }
            else if (item.itemName == "mobDrop")
            {
                Debug.Log("���� ����");
            }
        }
        else
        {
            Debug.Log("�̹� �����Ǿ� �ֽ��ϴ�. �κ��丮���� ����");
        }
    }

    public void RemoveItem(int _index)
    {
        Inventory.instance.items.RemoveAt(_index);  // ������ ����Ʈ���� �ش� �ε����� ������ ����
        Inventory.instance.onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
    }
}