using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public static Inventory instance;

    public delegate void OnSlotCountChange(int val);  // 대리자 정의
    public OnSlotCountChange onSlotCountChange;  // 인스턴스화

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
        // 최초 활성 슬롯 개수
        SlotCount = 5;
    }

    // 아이템 추가
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
            // AddItem()이 true이면(아이템을 획득하면)
            if (AddItem(fieldItem.GetItem()))
            {
                // 필드에서 삭제
                fieldItem.DestroyItem();
            }
        }
    }
}