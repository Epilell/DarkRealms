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
    private GameObject DropItem;
    public ItemDrop[] itemDropList; // ���Ͱ� ����ϴ� ������ ����

    public void ItemDrop()
    {
        // ��� ������ ó��
        foreach (ItemDrop itemDrop in itemDropList)
        {
            // ��� Ȯ���� ����Ͽ� �������� ������� ����
            if (Random.value < itemDrop.dropChance)
            {
                DropItem = itemDrop.item.DropItemPrefab;
                //mobItemUI.SetActive(true);
                Instantiate(DropItem, transform.position, Quaternion.identity);//�ӽþ����۵��
            }
        }
    }
}

