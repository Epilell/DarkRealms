using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;  // �κ��丮 �ν��Ͻ�

    public ItemType itemType;  // ������ Ÿ��

    public List<Item> items = new();  // �κ��丮�� �ִ� ������ ����Ʈ
    [SerializeField]
    private InventoryData InvenData;
    public delegate void OnSlotCountChange(int val);  // ���� ���� ���� �� ȣ���� �븮��
    public OnSlotCountChange onSlotCountChange;  // ���� ���� ���� �� �̺�Ʈ

    public delegate void OnChangeItem();  // ������ ���� �븮��
    public OnChangeItem onChangeItem;  // ������ ���� �ν��Ͻ�

    protected int slotCount;

    private void Awake()
    {
        if (instance != null)  // �κ��丮 �ν��Ͻ��� �����ϸ�
        {
            Destroy(gameObject);  // �ߺ� ���� ������ ���� ���� ���� ������Ʈ�� �ı�
            return;  // ����
        }
        instance = this;  // �ν��Ͻ��� �������� ������ ���� �ν��Ͻ��� �Ҵ�
    }

    public int SlotCount  // ���� ���� ������Ƽ
    {
        get => slotCount;  // ���� �� ��ȯ
        set  // ���� �� ����
        {
            slotCount = value;  // �Էµ� ������ ���� ���� ����
            onSlotCountChange.Invoke(slotCount);  // ���� ���� ���� ȣ��
        }
    }

    private void Start()
    {
        SlotCount = 15;  // // ���� Ȱ�� ���� ����

        items = InvenData.items;
        if (onChangeItem != null)  // ������ ���� �̺�Ʈ�� ��ϵǾ� ������
            onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
    }

    // ������ �߰� �Լ�
    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCount)  // �κ��丮�� ������ ������ ���� ���� ������ ������
        {
            items.Add(_item);  // ������ ����Ʈ�� ������ �߰�
            if (onChangeItem != null)  // ������ ���� �̺�Ʈ�� ��ϵǾ� ������
                onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
            InvenData.items = items;
            return true;  // ������ �߰�
        }
        else
        {
            Debug.Log("�κ��丮 ��ȭ!");
            InvenData.items = items;
            return false;  // ������ �߰����� ����
        }
    }

    // ������ ���� �Լ�
    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);  // ������ ����Ʈ���� �ش� �ε����� ������ ����
        onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
        InvenData.items = items;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))  // �浹�� ������Ʈ�� �±װ� "FieldItem"�� ��
        {
            // ������ ������Ʈ ��������
            PItem pItem = collision.GetComponent<PItem>();
            if (AddItem(pItem.GetItem()))  // AddItem()�� true�̸�(�κ��丮�� �������� �߰��ϸ�)
            {
                pItem.DestroyItem();  // �ʵ忡�� ����
            }
        }
    }
}