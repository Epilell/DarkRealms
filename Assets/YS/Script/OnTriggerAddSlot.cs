using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 스크립트는 삭제할 수도 있음
public class OnTriggerAddSlot : MonoBehaviour
{
    InventoryUI inventory;  // 인벤토리 UI
    public string AddSlotItem;  // 슬롯 확장 아이템

    private void Start()
    {
        inventory = FindObjectOfType<InventoryUI>();  // 인벤토리 UI를 찾아서 inventory 변수에 할당
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 슬롯 확장 아이템을 먹으면 InventoryUI의 AddSlot() 호출
        if (collision.gameObject.tag == AddSlotItem)
        {
            collision.gameObject.SetActive(false);  // 아이템 비활성화
            inventory.AddSlot();  // 슬롯 추가
        }
    }
}