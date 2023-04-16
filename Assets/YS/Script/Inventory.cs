using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public static Inventory instance;

    public delegate void OnSlotCountChange(int val);  // �븮�� ����
    public OnSlotCountChange onSlotCountChange;  // �ν��Ͻ�ȭ

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    private int slotCount;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public int SlotCount
    {
        get => slotCount;
        set
        {
            slotCount = value;
            onSlotCountChange.Invoke(slotCount);
        }
    }

    void Start()
    {
        // ���� Ȱ�� ���� ����
        SlotCount = 5;
    }

    // ������ �߰�
    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCount)
        {
            items.Add(_item);
            if (onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;
    }

    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))
        {
            FieldItem fieldItem = collision.GetComponent<FieldItem>();
            // AddItem()�� true�̸�(�������� ȹ���ϸ�)
            if (AddItem(fieldItem.GetItem()))
            {
                // �ʵ忡�� ����
                fieldItem.DestroyItem();
            }
        }
    }
}