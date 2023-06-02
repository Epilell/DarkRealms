using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

public class ItemContactToInven : MonoBehaviour
{
    SpriteRenderer sr;
    public Rito.InventorySystem.ItemData itemData;
    bool isDataON = false;
    private void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (itemData != null&&isDataON==false)
        {
            isDataON = true;
        }
        if (isDataON)
        {
            sr.sprite = itemData.IconSprite;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag== "Player")
        {
            Inventory inventoryComponent = GameObject.Find("GameManager").GetComponentInChildren<Inventory>();
            inventoryComponent.Add(this.itemData);
            Destroy(this.gameObject);
        }
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
                inventoryComponent.Add(this.itemData);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("�κ��丮�� �ִ� �ڽ� ��ã�Ҵ�!");
            }
        }
    }
}
