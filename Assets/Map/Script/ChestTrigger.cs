using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chestItemDrop
{
    public Rito.InventorySystem.ItemData item; //����� ������
    public float dropChance; // ��� Ȯ�� 
}

public class ChestTrigger : MonoBehaviour
{
    private SpriteRenderer thisImg; // ���� �̹���
    public Sprite change_img; // �ٲ� �̹���

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
                // ��� ������ ó��
                foreach (ItemDrop itemDrop in itemDropList)
                {
                    // ��� Ȯ��
                    if (Random.value < itemDrop.dropChance)
                    {
                        DropItem = itemDrop.item.DropItemPrefab;
                        GameObject itemclon = Instantiate(DropItem, transform.position, Quaternion.identity);//�ӽþ����۵��
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
                //�̹� ���ڰ� ���������� X
                Debug.Log("No Item");

            }
            thisImg.sprite = change_img;


        }
    }
}
