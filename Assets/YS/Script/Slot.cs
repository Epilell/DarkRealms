using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ���콺 Ŭ�� �̺�Ʈ�� ó���ϱ� ���� IPointerUpHandler �������̽� ����
public class Slot : MonoBehaviour, IPointerUpHandler
{
    public int slotNum;  // ���� ��ȣ
    public Item item;
    public Image itemIcon;  // �������� ǥ�õ� ������(���Ծȿ�����)
    //public P_Data p;

    public void UpdateSlotUI()
    {/*
        itemIcon.sprite = item.itemImage;  // ������ �̹��� ����
        itemIcon.enabled = true;  // ������ Ȱ��ȭ
        itemIcon.gameObject.SetActive(true);  // ������ ������Ʈ Ȱ��ȭ*/
        if (item != null && itemIcon != null)
        {
            itemIcon.sprite = item.itemImage;  // ������ �̹��� ����
            itemIcon.enabled = true;  // ������ Ȱ��ȭ
            itemIcon.gameObject.SetActive(true);  // ������ ������Ʈ Ȱ��ȭ
        }
        else
        {
            Debug.Log("Slot.UpdateSlotUI ���� �߻�");
        }
    }

    public void RemoveSlot()
    {
        item = null;  // ���� �� ������ ����
        //itemIcon.gameObject.SetActive(false);  // ������ ������Ʈ ��Ȱ��ȭ
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (item != null)  // �������� ������
        {
            bool isUse = item.Use();  // ������ ���

            if (isUse)  // ���������
            {
                /* ������� �Ʒ��ּ������� ���ο� ��ũ��Ʈ�� ����ÿ�
                if (item.itemType != ItemType.Equipment)
                {
                    if (item.itemName == "apple" || item.itemName == "cherry")
                    {
                        p.P_CurrentHp += item.effectPoint;
                    }
                    Inventory.instance.RemoveItem(slotNum);  // �κ��丮���� ����
                }
                else
                {
                    EquipmentInventory equipmentInventory = GameObject.FindObjectOfType<EquipmentInventory>();  // �ν��Ͻ�
                    // Debug.Log(equipmentInventory); // == Player
                    Slot targetSlot = equipmentInventory.FindEmptySlot(item.itemName);  // ��� ������ ã�Ƽ� �ҷ���

                    if (targetSlot != null) // ������ ��ȯ ������
                    {
                        equipmentInventory.AddItems(item, targetSlot);  // ��� ���Կ� ������ �߰�
                        equipmentInventory.RemoveItem(slotNum);  // ���� ���Կ��� ������ ����
                    }
                }���ο� ��ũ��Ʈ�� ����κ��� ���������*/
            }
        }
        else  // �������� ������
        {
            Debug.Log("��ĭ!");  // �α� ���
        }
    }
}