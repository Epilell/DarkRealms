using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

public class ItemContactToInven : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �ڽ� ������Ʈ���� Ȯ���մϴ�.
        foreach (Transform child in collision.transform)
        {
            // �ڽ� ������Ʈ���� Inventory ������Ʈ�� �����ɴϴ�.
            Inventory inventoryComponent = child.GetComponent<Inventory>();

            // Inventory ������Ʈ�� �ִٸ� �ش� ���� ������Ʈ�� ����մϴ�.
            if (inventoryComponent != null)
            {
                // Inventory ������Ʈ�� ����ϴ� ���� ������Ʈ�� ���� �۾��� �����մϴ�.
                // ���� ���, �ش� ���� ������Ʈ�� �������� �������ų� ������ �� �ֽ��ϴ�.
                //inventoryComponent.Add(this.itemData);
            }
        }
    }
}
