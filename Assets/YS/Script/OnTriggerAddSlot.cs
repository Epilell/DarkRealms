using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerAddSlot : MonoBehaviour
{
    InventoryUI inventory;
    public string AddSlotItem;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryUI>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� Ȯ�� �������� ������ InventoryUI�� AddSlot() ȣ��
        if (collision.gameObject.tag == AddSlotItem)
        {
            collision.gameObject.SetActive(false);
            inventory.AddSlot();
        }
    }
}