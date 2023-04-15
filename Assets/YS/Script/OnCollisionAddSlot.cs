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
        // ���� Ȯ�� �������� ������ InventoryUI�� AddSlot() ȣ��
        if (collision.gameObject.tag == AddSlotItem)
        {
            collision.gameObject.SetActive(false);
            inventory.AddSlot();
        }
    }
}