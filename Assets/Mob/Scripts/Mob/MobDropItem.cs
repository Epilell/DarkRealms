using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

[System.Serializable]
public class ItemDrop
{
    //public GameObject itemPrefab; // ��� ������ ������
    public Rito.InventorySystem.ItemData item; //��� ������
    public float dropChance; // ��� Ȯ�� (0 ~ 1 ������ ��)
}
public class MobDropItem : MonoBehaviour
{
    public bool IsBoss = false;
    private GameObject DropItem;
    public ItemDrop[] itemDropList; // ���Ͱ� ����ϴ� ������ ����

    public void ItemDrop()
    {
        // ��� ������ ó��
        foreach (ItemDrop itemDrop in itemDropList)
        {
            if (IsBoss)
            {
                // ��� Ȯ���� ����Ͽ� �������� ������� ����
                if (Random.value < itemDrop.dropChance)
                {
                    DropItem = itemDrop.item.DropItemPrefab;
                    //mobItemUI.SetActive(true);
                    GameObject itemclon = Instantiate(DropItem, transform.position+new Vector3(Random.Range(-3,4), Random.Range(-3,4), 0), Quaternion.identity);//�ӽþ����۵��
                    ItemContactToInven itemComponent = itemclon.GetComponent<ItemContactToInven>();
                    if (itemComponent != null)
                    {
                        itemComponent.itemData = itemDrop.item;

                    }
                }
            }
            else
            {
                // ��� Ȯ���� ����Ͽ� �������� ������� ����
                if (Random.value < itemDrop.dropChance)
                {
                    DropItem = itemDrop.item.DropItemPrefab;
                    //mobItemUI.SetActive(true);
                    //GameObject itemclon = Instantiate(DropItem, transform.position, Quaternion.identity);//�ӽþ����۵��
                    GameObject itemclon = Instantiate(DropItem, transform.position + new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0), Quaternion.identity);//�ӽþ����۵��
                    ItemContactToInven itemComponent = itemclon.GetComponent<ItemContactToInven>();
                    if (itemComponent != null)
                    {
                        itemComponent.itemData = itemDrop.item;

                    }
                }
            }
        }
    }
}

