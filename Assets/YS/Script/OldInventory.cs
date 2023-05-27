using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldInventory : MonoBehaviour
{
    [SerializeField]
    private InventoryData InvenData;

    public static OldInventory instance;  // 인벤토리 인스턴스

    public List<Item> items = new();  // 인벤토리에 있는 아이템 리스트

    public delegate void OnSlotCountChange(int val);  // 슬롯 개수 변경 시 호출할 대리자
    public OnSlotCountChange onSlotCountChange;  // 슬롯 개수 변경 시 이벤트

    public delegate void OnChangeItem();  // 아이템 변경 대리자
    public OnChangeItem onChangeItem;  // 아이템 변경 인스턴스

    protected int slotCount;

    protected void Awake()
    {
        if (instance != null)  // 인벤토리 인스턴스가 존재하면
        {
            Destroy(gameObject);  // 중복 생성 방지를 위해 현재 게임 오브젝트를 파괴
            return;  // 종료
        }
        instance = this;  // 인스턴스가 존재하지 않으면 현재 인스턴스를 할당
    }

    public int SlotCount  // 슬롯 개수 프로퍼티
    {
        get => slotCount;  // 슬롯 수 반환
        set  // 슬롯 수 설정
        {
            slotCount = value;  // 입력된 값으로 슬롯 개수 설정
            onSlotCountChange.Invoke(slotCount);  // 슬롯 개수 변경 호출
        }
    }

    protected void Start()
    {
        SlotCount = 15;  // // 최초 활성 슬롯 개수
        if (InvenData.items.Count > 0)
        {
            items = InvenData.items;
            if (onChangeItem != null)  // 아이템 변경 이벤트가 등록되어 있으면
                onChangeItem.Invoke();  // 아이템 변경 이벤트 호출
        }
    }

    // 아이템 추가 함수
    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCount)  // 인벤토리에 보유한 아이템 수가 슬롯 수보다 작으면
        {
            items.Add(_item);  // 아이템 리스트에 아이템 추가
            if (onChangeItem != null)  // 아이템 변경 이벤트가 등록되어 있으면
                onChangeItem.Invoke();  // 아이템 변경 이벤트 호출
            InvenData.items = items;
            return true;  // 아이템 추가
        }
        else
        {
            Debug.Log("인벤토리 포화!");
            InvenData.items = items;
            return false;  // 아이템 추가하지 않음
        }
    }

    // 아이템 제거 함수
    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);  // 아이템 리스트에서 해당 인덱스의 아이템 제거
        onChangeItem.Invoke();  // 아이템 변경 이벤트 호출
        InvenData.items = items;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))  // 충돌한 오브젝트의 태그가 "FieldItem"일 때
        {
            PItem pItem = collision.GetComponent<PItem>(); // 아이템 컴포넌트 가져오기
            if (AddItem(pItem.GetItem()))  // AddItem()이 true이면(인벤토리에 아이템을 추가하면)
            {
                pItem.DestroyItem();  // 필드에서 삭제
            }
        }
    }

    // 아래부터 5월 4주차
    public void RemoveAllItem() // 인벤토리 내 모든 아이템 제거
    {
        items.Clear(); // 아이템 리스트 비우기
        if (onChangeItem != null) { onChangeItem.Invoke(); } // 아이템 변경 이벤트 호출
        InvenData.items = items; // 인벤토리 데이터 업데이트
    }

    public int GetItemCount(string itemName) // 해당 아이템 개수 반환
    {
        int count = 0;

        foreach (Item item in items)
        {
            if (item.itemName == itemName)
            {
                count++; // 밑에 걸로
                // count += item.amount;
            }
        }
        
        return count;
    }

    public int GetItemSlotCount() // 용량(아이템 슬롯 수) 반환
    {
        int count = 0;

        foreach (Item item in items)
        {
            if (item != null)
            {
                count++;
            }
        }

        return count;
    }
}