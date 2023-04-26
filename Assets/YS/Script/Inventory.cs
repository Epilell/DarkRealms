using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();  // 인벤토리에 보유한 아이템 리스트

    public static Inventory instance;  // 인벤토리 싱글톤 인스턴스

    public delegate void OnSlotCountChange(int val);  // 슬롯 개수 변경 이벤트 대리자 정의
    public OnSlotCountChange onSlotCountChange;  // 슬롯 개수 변경 이벤트 인스턴스화

    public delegate void OnChangeItem();  // 아이템 변경 이벤트 대리자 정의
    public OnChangeItem onChangeItem;  // 아이템 변경 이벤트 인스턴스화

    private int slotCount;  // 인벤토리 슬롯의 개수

    private void Awake()
    {
        if (instance != null)  // 인스턴스가 이미 존재하면
        {
            Destroy(gameObject);  // 중복 생성 방지를 위해 현재 게임 오브젝트를 파괴하고
            return;  // 함수 종료
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

    void Start()
    {
        SlotCount = 5;  // // 최초 활성 슬롯 개수
    }

    // 아이템 추가 함수
    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCount)  // 인벤토리에 보유한 아이템 수가 슬롯 수보다 작을 때
        {
            items.Add(_item);  // 아이템 리스트에 아이템 추가
            if (onChangeItem != null)  // 아이템 변경 이벤트가 등록되어 있으면
                onChangeItem.Invoke();  // 아이템 변경 이벤트 호출
            return true;  // 아이템 추가
        }
        return false;  // 아이템 추가하지 않음
    }

    // 아이템 제거 함수
    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);  // 아이템 리스트에서 해당 인덱스의 아이템 제거
        onChangeItem.Invoke();  // 아이템 변경 이벤트 호출
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FieldItem"))  // 충돌한 오브젝트의 태그가 "FieldItem"이면
        {
            FieldItem fieldItem = collision.GetComponent<FieldItem>();  // 필드 아이템 컴포넌트 가져오기
            // AddItem()이 true이면(아이템을 획득하면)
            if (AddItem(fieldItem.GetItem()))  // AddItem()이 true이면(인벤토리에 아이템을 추가하면)
            {
                // 필드에서 삭제
                fieldItem.DestroyItem();
            }
        }
    }
}