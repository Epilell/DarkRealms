using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionAddSlot : MonoBehaviour
{
    InventoryUI inventory;
    public string AddSlotItem;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryUI>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 슬롯 확장 아이템을 먹으면 InventoryUI의 AddSlot() 호출
        if (collision.gameObject.tag == AddSlotItem)
        {
            collision.gameObject.SetActive(false);
            inventory.AddSlot();
        }
    }
}