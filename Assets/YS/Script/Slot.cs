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
    public Image itemIcon;  // ������

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;  // ������ �̹��� ����
        itemIcon.enabled = true;  // ������ Ȱ��ȭ
        itemIcon.gameObject.SetActive(true);  // ������ ������Ʈ Ȱ��ȭ
    }

    public void RemoveSlot()
    {
        item = null;  // ���� �� ������ ����
        itemIcon.gameObject.SetActive(false);  // ������ ������Ʈ ��Ȱ��ȭ
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bool isUse = item.Use();  // ������ ���
        // ���������
        if (isUse)
        {
            if (item.itemType != ItemType.Equipment)
            {
                Inventory.instance.RemoveItem(slotNum); // �κ��丮���� ����
            }
            else
            {
                Slot targetSlot = Inventory.instance.FindEmptySlot(item.itemName);
                // Debug.Log(targetSlot); // ��� ���� ���
                // Debug.Log(item.itemName); // ������ �̸�
                if (targetSlot != null)
                {
                    Inventory.instance.AddItems(item, targetSlot); // ��� ���Կ� ������ �߰�
                    Inventory.instance.RemoveItem(slotNum); // ���� ���Կ��� ������ ����
                }
                else
                {
                    Inventory.instance.AddItems(item, targetSlot);
                }
            }
        }
    }
}