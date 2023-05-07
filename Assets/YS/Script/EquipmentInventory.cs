using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    public Slot slot, slot2, slot3, slot4/*, slot5*/;
    public P_Data p;

    private void Start() // 장비 슬롯 초기화
    {
        slot.item = null;
        slot2.item = null;
        slot3.item = null;
        slot4.item = null;
        // slot5.item = null;
    }

    public Slot FindEmptySlot(String itemName)  // 빈 슬롯 찾기
    {
        if (itemName == "helmet")
        {
            return slot;
        }
        else if (itemName == "armor" /*|| itemName == "armor2"*/)
        {
            return slot2;
        }
        else if (itemName == "rifle" || itemName == "shotgun")
        {
            return slot3;
        }
        else if (itemName == "shoes")
        {
            return slot4;
        }
        /*else if (itemName == "mobDrop")
        {
            return slot5;
        }*/
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
            else if (item.itemName == "armor" /*|| item.itemName == "armor2"*/)
            {
                Debug.Log("갑옷 장착");
                p.P_MaxHp += item.effectPoint;
/*                HealthBar healthBar = GameObject.FindObjectOfType<HealthBar>();
                healthBar.maxHp += item.effectPoint;  // effectPoint만큼 최대 체력 증가
                healthBar.currentHp += item.effectPoint / 2;  // effectPoint의 절반만큼 현재 체력 증가*/
            }
            else if (item.itemName == "sword")
            {
                Debug.Log("무기 장착");
            }
            else if (item.itemName == "shoes")
            {
                Debug.Log("신발 장착");
            }
            else if (item.itemName == "mobDrop")
            {
                Debug.Log("반지 장착");
            }
        }
        else
        {
            Debug.Log("이미 장착되어 있습니다. 인벤토리에서 삭제");
        }
    }

    public void RemoveItem(int _index)
    {
        Inventory.instance.items.RemoveAt(_index);  // 아이템 리스트에서 해당 인덱스의 아이템 제거
        Inventory.instance.onChangeItem.Invoke();  // 아이템 변경 이벤트 호출
    }
}