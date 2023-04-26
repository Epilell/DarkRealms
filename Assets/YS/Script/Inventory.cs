using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();  // �κ��丮�� ������ ������ ����Ʈ

    public static Inventory instance;  // �κ��丮 �̱��� �ν��Ͻ�

    public delegate void OnSlotCountChange(int val);  // ���� ���� ���� �̺�Ʈ �븮�� ����
    public OnSlotCountChange onSlotCountChange;  // ���� ���� ���� �̺�Ʈ �ν��Ͻ�ȭ

    public delegate void OnChangeItem();  // ������ ���� �̺�Ʈ �븮�� ����
    public OnChangeItem onChangeItem;  // ������ ���� �̺�Ʈ �ν��Ͻ�ȭ

    private int slotCount;  // �κ��丮 ������ ����

    private void Awake()
    {
        if (instance != null)  // �ν��Ͻ��� �̹� �����ϸ�
        {
            Destroy(gameObject);  // �ߺ� ���� ������ ���� ���� ���� ������Ʈ�� �ı��ϰ�
            return;  // �Լ� ����
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
        SlotCount = 5;  // // ���� Ȱ�� ���� ����
    }

    // ������ �߰� �Լ�
    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCount)  // �κ��丮�� ������ ������ ���� ���� ������ ���� ��
        {
            items.Add(_item);  // ������ ����Ʈ�� ������ �߰�
            if (onChangeItem != null)  // ������ ���� �̺�Ʈ�� ��ϵǾ� ������
                onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
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
            // AddItem()�� true�̸�(�������� ȹ���ϸ�)
            if (AddItem(fieldItem.GetItem()))  // AddItem()�� true�̸�(�κ��丮�� �������� �߰��ϸ�)
            {
                // �ʵ忡�� ����
                fieldItem.DestroyItem();
            }
        }
    }
}