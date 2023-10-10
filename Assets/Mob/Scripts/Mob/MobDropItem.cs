using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

[System.Serializable]
public class ItemDrop
{
    //public GameObject itemPrefab; // 드롭 아이템 프리팹
    public Rito.InventorySystem.ItemData item; //드롭 아이템
    public float dropChance; // 드롭 확률 (0 ~ 1 사이의 값)
    public int dropCount = 1;//드롭 아이템 수
}
public class MobDropItem : MonoBehaviour
{
    public bool IsBoss = false;
    public bool IsObj = false;
    private GameObject DropItem;
    public ItemDrop[] itemDropList; // 몬스터가 드롭하는 아이템 정보


    public void ItemDrop()
    {
        // 드롭 아이템 처리
        foreach (ItemDrop itemDrop in itemDropList)
        {
            if (IsBoss)
            {
                // 드롭 확률을 계산하여 아이템을 드롭할지 결정
                if (Random.value < itemDrop.dropChance)
                {
                    DropItem = itemDrop.item.DropItemPrefab;
                    //mobItemUI.SetActive(true);
                    GameObject itemclon = Instantiate(DropItem, transform.position+new Vector3(Random.Range(-1,2), Random.Range(-1,2), 0), Quaternion.identity);//임시아이템드롭
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
                    GameObject itemclon = Instantiate(DropItem, transform.position, Quaternion.identity);//임시아이템드롭
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
                // 드롭 확률을 계산하여 아이템을 드롭할지 결정
                if (Random.value < itemDrop.dropChance)
                {
                    DropItem = itemDrop.item.DropItemPrefab;
                    //mobItemUI.SetActive(true);
                    //GameObject itemclon = Instantiate(DropItem, transform.position, Quaternion.identity);//임시아이템드롭
                    GameObject itemclon = Instantiate(DropItem, transform.position + new Vector3(Random.Range(-0.5f, 0.6f), Random.Range(-0.5f, 0.6f), 0), Quaternion.identity);//임시아이템드롭
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

