using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public static EquipmentInventory instance;  // 인벤토리 인스턴스
    //public EquipmentSlot slot, slot2, slot3, slot4, slot5, slot6, slot7, slot8;
    private EquipmentSlot[] slots;
    public P_Data p;//이건 장비아이템 코드를 따로 만들면 필요없음
    public EquipmentData Eqdata;
    public Transform slotHolder;
    private void Awake()
    {
        if (instance != null)  // 인벤토리 인스턴스가 존재하면
        {
            Destroy(gameObject);  // 중복 생성 방지를 위해 현재 게임 오브젝트를 파괴
            return;  // 종료
        }
        instance = this;  // 인스턴스가 존재하지 않으면 현재 인스턴스를 할당
    }
    private void Start() // 장비 슬롯 초기화
    {
        slots = slotHolder.GetComponentsInChildren<EquipmentSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                slots[i].item = Eqdata.EqItems[i];
            }
        }
    }

    public Slot FindEmptySlot(String itemName)  // 빈 슬롯 찾기
    {
        if (itemName == "helmet")
        {
            return slots[0];
        }
        else if (itemName == "armor" /*|| itemName == "armor2"*/)
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
        else if (itemName == "mobDrop")
        {
            return slots[6];
        }
        else return null;
    }

    public void AddItems(Item item, Slot targetSlot)  // 빈 슬롯에 넣음
    {
        if (targetSlot != null && targetSlot.item == null) // 해당 슬롯에 이미 아이템이 있으면 추가하지 않음
        {
            targetSlot.item = item;
            targetSlot.UpdateSlotUI();

            if (item.itemName == "helmet")
            {
                Debug.Log("투구 장착");
            }
            else if (item.itemName == "armor")
            {
                Debug.Log("갑옷 장착");
                // p.P_MaxHp += item.effectPoint;
                //플레이어에게 장착효과 넣는것도 새로운 스크립트로 만들어서 여기서는 호출만 하시오
            }
            else if (item.itemName == "knee")
            {
                Debug.Log("무릎 보호구 장착");
            }
            else if (item.itemName == "shoes")
            {
                Debug.Log("신발 장착");
            }
            else if (item.itemName == "shotgun")
            {
                Debug.Log("소총 장착");
            }
            else if (item.itemName == "shotgun")
            {
                Debug.Log("산탄총 장착");
            }
            else if (item.itemName == "mobDrop")
            {
                Debug.Log("반지 장착");
            }
        }
        else { }
    }

    public void RemoveItem(int _index)
    {
        Inventory.instance.items.RemoveAt(_index);  // 아이템 리스트에서 해당 인덱스의 아이템 제거
        Inventory.instance.onChangeItem.Invoke();  // 아이템 변경 이벤트 호출
    }
}