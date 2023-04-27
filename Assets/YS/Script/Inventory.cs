using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemType itemType;  // ������ Ÿ��
    public List<Item> items = new List<Item>();  // �κ��丮�� �ִ� ������ ����Ʈ

    public static Inventory instance;  // �κ��丮 �ν��Ͻ�

    public delegate void OnSlotCountChange(int val);  // ���� ���� ���� �� ȣ���� �븮��
    public OnSlotCountChange onSlotCountChange;  // ���� ���� ���� �� �̺�Ʈ

    public delegate void OnChangeItem();  // ������ ���� �븮��
    public delegate void OnChangeItem2();  // ������ ���� �븮��
    public OnChangeItem onChangeItem;  // ������ ���� �ν��Ͻ�
    public OnChangeItem onChangeItem2;  // ������ ���� �ν��Ͻ�

    private int slotCount;

    private void Awake()
    {
        if (instance != null)  // �κ��丮 �ν��Ͻ��� �����ϸ�
        {
            Destroy(gameObject);  // �ߺ� ���� ������ ���� ���� ���� ������Ʈ�� �ı��ϰ�
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

    void Start()
    {
        SlotCount = 15;  // // ���� Ȱ�� ���� ����
    }

    // ������ �߰� �Լ�
    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCount)  // �κ��丮�� ������ ������ ���� ���� ������ ������
        {
            items.Add(_item);  // ������ ����Ʈ�� ������ �߰�
            if (onChangeItem != null)  // ������ ���� �̺�Ʈ�� ��ϵǾ� ������
                onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
            return true;  // ������ �߰�
        }
        return false;  // ������ �߰����� ����
    }

    public bool AddItem2(Item _item)//
    {
        if (items.Count < SlotCount)  // �κ��丮�� ������ ������ ���� ���� ������ ������
        {
            items.Add(_item);  // ������ ����Ʈ�� ������ �߰�
            if (onChangeItem2 != null)  // ������ ���� �̺�Ʈ�� ��ϵǾ� ������
                onChangeItem2.Invoke();  // ������ ���� �̺�Ʈ ȣ��
            return true;  // ������ �߰�
        }
        return false;  // ������ �߰����� ����
    }

    // ������ ���� �Լ�
    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);  // ������ ����Ʈ���� �ش� �ε����� ������ ����
        onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))  // �浹�� ������Ʈ�� �±װ� "FieldItem"�̸�
        {
            FieldItem fieldItem = collision.GetComponent<FieldItem>();  // �ʵ� ������ ������Ʈ ��������
            
            if(fieldItem.item.itemType != ItemType.Equipment) {  // ���� �ʵ� ������ ������ ��� �������� �ƴϸ� ������ �κ��丮�� �߰�
                if (AddItem(fieldItem.GetItem()))  // AddItem()�� true�̸�(�κ��丮�� �������� �߰��ϸ�)
                {
                    fieldItem.DestroyItem();  // �ʵ忡�� ����
                }
            }
            else {  // ��� �������̸�//
                if (AddItem2(fieldItem.GetItem()))  // AddItem2()�� true
                {
                    fieldItem.DestroyItem();  // �ʵ忡�� ����
                }
            }
        }
    }
}