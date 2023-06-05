using System;
using UnityEngine;
using Rito.InventorySystem;

public class EquipmentInventory : MonoBehaviour
{
    public Inventory _inventory;
    public static EquipmentInventory instance;  // ��� �κ��丮 �ν��Ͻ�
    public P_Data playerData;

    private Rito.InventorySystem.Item[] _eqitems;
    public Rito.InventorySystem.Item[] EqItems { get => _eqitems; }

    private ItemSlotUI[] slots;
    public Transform slotHolder;
    GameManager gm;
    private void Awake()
    {
        gm = GetComponentInParent<GameManager>();
        gm.LoadEquibment(this);
        _eqitems = new Rito.InventorySystem.Item[5];
        if (instance != null)  // �κ��丮 �ν��Ͻ��� �����ϸ�
        {
            Destroy(gameObject);  // �ߺ� ���� ������ ���� ���� ���� ������Ʈ�� �ı�
            return;  // ����
        }
        instance = this;  // �ν��Ͻ��� �������� ������ ���� �ν��Ͻ��� �Ҵ�
    }
    private void OnDestroy()
    {
        gm = GetComponentInParent<GameManager>();
        gm.SaveEquipment(this);
    }
    private void Start() // ��� ���� �ʱ�ȭ
    {
        slots = slotHolder.GetComponentsInChildren<ItemSlotUI>();
        for (int i = 0; i < slots.Length; i++)
        {
            Updateslot(i);
        }
    }
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="index"></param>
    public void UnEquip(int index)
    {
        _inventory.Add(_eqitems[index].Data);
        _eqitems[index] = null;
        slots[index].RemoveItem();
    }
    /// <summary>
    /// ����ϱ�. ������ �����͸� �ް� ����Ű�� �̹� ����� �������� ������ �� ������ �����ְ� ������ null��ȯ
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public ItemData ChangeEquip(ItemData itemData)
    {
        if (itemData is EquipmentItemData eqData)
        {
            int index = FindSlot(eqData);
            if (index == 5)
            {
                return itemData;
            }
            if (_eqitems[index] == null)
            {
                _eqitems[index] = itemData.CreateItem();
                Updateslot(index);
                //������ ��� ȿ��
                ItemEffect(_eqitems[index].Data as EquipmentItemData);
                return null;
            }
            else
            {
                ItemData forReturnItemData = _eqitems[index].Data;
                _eqitems[index] = itemData.CreateItem();
                Updateslot(index);
                //������ ��� ȿ��
                ItemEffect(_eqitems[index].Data as EquipmentItemData);
                return forReturnItemData;
            }
        }
        else
        {
            return itemData;
        }
    }

    public int FindSlot(EquipmentItemData eqdata)  // �� ���� ã��
    {
        if (eqdata.itemType == "helmet")
        {
            return 0;
        }
        else if (eqdata.itemType == "leg")
        {
            return 1;
        }
        else if (eqdata.itemType == "weapon")
        {
            return 2;
        }
        else if (eqdata.itemType == "armor")
        {
            return 3;
        }
        else if (eqdata.itemType == "shoes")
        {
            return 4;
        }
        else return 5;
    }

    private void Updateslot(int index)
    {
        Rito.InventorySystem.Item? item;
        if (_eqitems[index] != null)
        {
            item = _eqitems[index];
        }
        else
        {
            item = null;
        }
        // 1. �������� ���Կ� �����ϴ� ���
        if (item != null)
        {
            // ������ ���
            SetItemIcon(index, item.Data.IconSprite);
        }
        // 2. �� ������ ��� : ������ ����
        else
        {
            RemoveIcon();
        }

        // ���� : ������ �����ϱ�
        void RemoveIcon()
        {
            RemoveItem(index);
        }
    }
    public void SetItemIcon(int index, Sprite icon)
    {
        slots[index].SetItem(icon);
    }
    public void RemoveItem(int index)
    {
        slots[index].RemoveItem();
    }
    /// <summary>
    /// ��� ������ ���� ȿ��
    /// </summary>
    /// <param name="eqdata"></param>
    private void ItemEffect(EquipmentItemData eqdata)
    {
        int itemNum =FindSlot(eqdata);
        if (eqdata is ArmorItemData armdata)
        {
            switch (itemNum)
            {
                case 0: 
                    playerData.Helmet = armdata.Defence;
                    break;
                case 1:
                    playerData.Leg = armdata.Defence;
                    break;
                case 3:
                    playerData.Body = armdata.Defence;
                    break;
                case 4:
                    playerData.Shoes = armdata.Defence;
                    break;
            }
        }
        else if(eqdata is WeaponItemData wdata)
        {
            //playerData.���ݷ� = wdata.Damage;
        }
        else
        {
            Debug.Log("���������� �ƴ�");
        }
    }
}