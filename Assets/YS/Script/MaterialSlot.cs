using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSlot : MonoBehaviour
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
}