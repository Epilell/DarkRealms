using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ũ��Ʈ�� ������ ���� ����
public class OnTriggerAddSlot : MonoBehaviour
{
    InventoryUI inventory;  // �κ��丮 UI
    public string AddSlotItem;  // ���� Ȯ�� ������

    private void Start()
    {
        inventory = FindObjectOfType<InventoryUI>();  // �κ��丮 UI�� ã�Ƽ� inventory ������ �Ҵ�
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� Ȯ�� �������� ������ InventoryUI�� AddSlot() ȣ��
        if (collision.gameObject.tag == AddSlotItem)
        {
            collision.gameObject.SetActive(false);  // ������ ��Ȱ��ȭ
            inventory.AddSlot();  // ���� �߰�
        }
    }
}