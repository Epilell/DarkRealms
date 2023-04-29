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
    public Image itemIcon;  // 아이콘

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;  // 아이템 이미지 설정
        itemIcon.enabled = true;  // 아이콘 활성화
        itemIcon.gameObject.SetActive(true);  // 아이템 오브젝트 활성화
    }

    public void RemoveSlot()
    {
        item = null;  // 슬롯 안 아이템 제거
        itemIcon.gameObject.SetActive(false);  // 아이템 오브젝트 비활성화
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bool isUse = item.Use();  // 아이템 사용
        // 사용했으면
        if (isUse)
        {
            if (item.itemType != ItemType.Equipment)
            {
                Inventory.instance.RemoveItem(slotNum);  // 인벤토리에서 제거
            }
            else
            {
                Debug.Log("장비템");
                Inventory.instance.RemoveItem(slotNum);
                Slot targetSlot = Inventory.instance.FindEmptySlot();
                if (targetSlot != null)
                {
                    Debug.Log("제거");
                    // 대상 슬롯에 아이템 추가
                    Inventory.instance.AddItems(item);
                }
            }
        }
    }
}