using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chestItemDrop
{
    public Rito.InventorySystem.ItemData item; //드롭할 아이템
    public float dropChance; // 드롭 확률 
}

public class ChestTrigger : MonoBehaviour
{
    private SpriteRenderer thisImg; // 기존 이미지
    public Sprite change_img; // 바뀔 이미지

    private GameObject DropItem;
    public ItemDrop[] itemDropList;

    public void Start()
    {
        thisImg = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (thisImg.sprite != change_img)
            {
                FindObjectOfType<SoundManager>().PlaySound("BoxOpen");
                Debug.Log("Item drop");
                // 드롭 아이템 처리
                foreach (ItemDrop itemDrop in itemDropList)
                {
                    // 드롭 확률
                    if (Random.value < itemDrop.dropChance)
                    {
                        DropItem = itemDrop.item.DropItemPrefab;
                        GameObject itemclon = Instantiate(DropItem, transform.position, Quaternion.identity);//임시아이템드롭
                        ItemContactToInven itemComponent = itemclon.GetComponent<ItemContactToInven>();
                        if (itemComponent != null)
                        {
                            itemComponent.itemData = itemDrop.item;

                        }

                    }
                }
            }
            else
            {
                //이미 상자가 열려있으면 X
                Debug.Log("No Item");

            }
            thisImg.sprite = change_img;


        }
    }
}
