using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 마우스 클릭 이벤트를 처리하기 위해 IPointerUpHandler 인터페이스 구현
public class Slot : MonoBehaviour, IPointerUpHandler
{
    public int slotNum;  // 슬롯 번호
    public Item item;
    public Image itemIcon;  // 아이템이 표시될 아이콘(슬롯 안에 있음)
    public Player player;

    public void UpdateSlotUI()
    {
        if (item != null && itemIcon != null)
        {
            itemIcon.sprite = item.itemImage;  // 아이템 이미지 설정
            itemIcon.enabled = true;  // 아이콘 활성화
            itemIcon.gameObject.SetActive(true);  // 아이템 오브젝트 활성화
        }
        else
        {
            Debug.Log("Slot.UpdateSlotUI 오류 발생");
        }
    }

    public void RemoveSlot()
    {
        item = null;  // 슬롯 안 아이템 제거
        itemIcon.gameObject.SetActive(false);  // 아이템 오브젝트 비활성화
    }

    // EquipmentSlot에 new 붙여도 안 되어서 일단 virtual 붙여서 작동 시킴
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (item != null)  // 아이템이 있으면
        {
            bool isUse = item.Use();  // 아이템 사용

            if (isUse)  // 사용했으면
            {
                if (item.itemType == ItemType.Consumables)
                {
                    Debug.Log("slot use");
                    player.P_Heal(item.effectPoint);
                    OldInventory.instance.RemoveItem(slotNum);  // 인벤토리에서 제거
                }
                else if (item.itemType == ItemType.Equipment)
                {
                    EquipmentInventory equipmentInventory = GameObject.FindObjectOfType<EquipmentInventory>();  // 인스턴스
                    Slot targetSlot = equipmentInventory.FindEmptySlot(item.itemName);  // 대상 슬롯을 찾아서 불러옴

                    if (targetSlot != null) // 슬롯을 반환 받으면
                    {
                        equipmentInventory.AddItems(item, targetSlot, slotNum);  // 대상 슬롯에 아이템 추가
                        // equipmentInventory.RemoveItem(slotNum);  // 현재 슬롯에서 아이템 제거 ← AddItems에 통합
                    }
                }
                else return;
            }
        }
        else  // 아이템이 없으면
        {
            Debug.Log("빈칸!");  // 로그 출력
        }
    }
}