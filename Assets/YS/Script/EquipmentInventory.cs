using System;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    protected OldInventory oldInventory;

    public static EquipmentInventory instance;  // �κ��丮 �ν��Ͻ�
    private EquipmentSlot[] slots;
    public EquipmentData Eqdata;
    public Transform slotHolder;

    public Equip equip;

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
        oldInventory = OldInventory.instance;
        
        slots = slotHolder.GetComponentsInChildren<EquipmentSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                slots[i].item = Eqdata.EqItems[i];
                slots[i].item = null;
            }
        }
    }

    public Slot FindEmptySlot(String itemName)  // �� ���� ã��
    {
        if (itemName == "helmet")
        {
            return slots[0];
        }
        else if (itemName == "armor")
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
        else return null;
    }

    public void AddItems(Item item, Slot targetSlot, int slotNum)  // �� ���Կ� ����
    {
        // image�� null�̸� �������� ���� �ɷ�, �̹����� �ٸ��� �ٸ� ���������� ����
        if (targetSlot != null && /*targetSlot.item.itemImage != item.itemImage || */targetSlot.item == null /*targetSlot.item.itemName == ""*/) // �ش� ���Կ� ���� �������� ������ �߰����� ����
        {
            /*if (targetSlot.item != null && targetSlot.item.itemImage != null)
            {
                equip.RemoveEquipmentEffect(targetSlot.item.itemName, targetSlot.item.effectPoint);
                oldInventory.AddItem(targetSlot.item); // �ٸ� �������� �����ϸ� ������ ��ü
            }
            */

            // ��� ���Կ� ������ �߰�
            targetSlot.item = item;
            targetSlot.UpdateSlotUI();

            RemoveItem(slotNum); // �ӽ÷� �߰�

            // ���� ���� ȿ�� �ο�
            /*if (item.itemName == "helmet")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }
            else if (item.itemName == "armor")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }
            else if (item.itemName == "knee")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }
            else if (item.itemName == "shoes")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }
            else if (item.itemName == "rifle")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }
            else if (item.itemName == "shotgun")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }*/
        }
        else { }
    }

    public void RemoveItem(int _index)
    {
        OldInventory.instance.items.RemoveAt(_index);  // ������ ����Ʈ���� �ش� �ε����� ������ ����
        OldInventory.instance.onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
    }
}