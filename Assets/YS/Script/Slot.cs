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
    public Image itemIcon;  // �������� ǥ�õ� ������(���� �ȿ� ����)
    public Player player;

    public void UpdateSlotUI()
    {
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
        itemIcon.gameObject.SetActive(false);  // ������ ������Ʈ ��Ȱ��ȭ
    }

    // EquipmentSlot�� new �ٿ��� �� �Ǿ �ϴ� virtual �ٿ��� �۵� ��Ŵ
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (item != null)  // �������� ������
        {
            bool isUse = item.Use();  // ������ ���

            if (isUse)  // ���������
            {
                if (item.itemType == ItemType.Consumables)
                {
                    Debug.Log("slot use");
                    player.P_Heal(item.effectPoint);
                    OldInventory.instance.RemoveItem(slotNum);  // �κ��丮���� ����
                }
                else if (item.itemType == ItemType.Equipment)
                {
                    EquipmentInventory equipmentInventory = GameObject.FindObjectOfType<EquipmentInventory>();  // �ν��Ͻ�
                    Slot targetSlot = equipmentInventory.FindEmptySlot(item.itemName);  // ��� ������ ã�Ƽ� �ҷ���

                    if (targetSlot != null) // ������ ��ȯ ������
                    {
                        equipmentInventory.AddItems(item, targetSlot, slotNum);  // ��� ���Կ� ������ �߰�
                        // equipmentInventory.RemoveItem(slotNum);  // ���� ���Կ��� ������ ���� �� AddItems�� ����
                    }
                }
                else return;
            }
        }
        else  // �������� ������
        {
            Debug.Log("��ĭ!");  // �α� ���
        }
    }
}