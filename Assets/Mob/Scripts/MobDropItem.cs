using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrop
{
    public GameObject itemPrefab; // ��� ������ ������
    public float dropChance; // ��� Ȯ�� (0 ~ 1 ������ ��)
}
public class MobDropItem : MonoBehaviour
{
    public GameObject mobItemUI;//������UI
    public ItemDrop[] itemDrops; // ���Ͱ� ����ϴ� ������ ����

    public void ItemDrop()
    {
        // ��� ������ ó��
        foreach (ItemDrop itemDrop in itemDrops)
        {
            // ��� Ȯ���� ����Ͽ� �������� ������� ����
            if (Random.value < itemDrop.dropChance)
            {
                mobItemUI.SetActive(true);
                //���� �κ��丮 �ý��۰� ����
                // ������ �������� �����Ͽ� ���� �߰�
                //GameObject item = Instantiate(itemDrop.itemPrefab, transform.position, Quaternion.identity);
                // �������� �κ��丮�� �̵�
                //Inventory.instance.AddItem(item.GetComponent<Item>());
            }
        }
    }
}

