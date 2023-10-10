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
    public int dropCount = 1;//��� ������ ��
}
public class MobDropItem : MonoBehaviour
{
    public bool IsBoss = false;
    public bool IsObj = false;
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
                    GameObject itemclon = Instantiate(DropItem, transform.position+new Vector3(Random.Range(-1,2), Random.Range(-1,2), 0), Quaternion.identity);//�ӽþ����۵��
                    ItemContactToInven itemComponent = itemclon.GetComponent<ItemContactToInven>();
                    if (itemComponent != null)
                    {
                        itemComponent.itemData = itemDrop.item;

                    }
                }
            }
            else if (IsObj)
            {
                if (Random.value < itemDrop.dropChance)
                {
                    DropItem = itemDrop.item.DropItemPrefab;
                    GameObject itemclon = Instantiate(DropItem, transform.position, Quaternion.identity);//�ӽþ����۵��
                    ItemContactToInven itemComponent = itemclon.GetComponent<ItemContactToInven>();
                    if (itemComponent != null)
                    {
                        itemComponent.itemData = itemDrop.item;
                        itemComponent.itemCount = itemDrop.dropCount;
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
                    GameObject itemclon = Instantiate(DropItem, transform.position + new Vector3(Random.Range(-0.5f, 0.6f), Random.Range(-0.5f, 0.6f), 0), Quaternion.identity);//�ӽþ����۵��
                    ItemContactToInven itemComponent = itemclon.GetComponent<ItemContactToInven>();
                    if (itemComponent != null)
                    {
                        itemComponent.itemData = itemDrop.item;
                        itemComponent.itemCount = itemDrop.dropCount;
                    }
                }
            }
        }
    }
}

