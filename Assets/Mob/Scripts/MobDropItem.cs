using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrop
{
    //public GameObject itemPrefab; // ��� ������ ������
    public Item item;//��� ������
    public float dropChance; // ��� Ȯ�� (0 ~ 1 ������ ��)
}
public class MobDropItem : MonoBehaviour
{
    public GameObject mobItemUI;//������UI
    public ItemDrop[] itemDropList; // ���Ͱ� ����ϴ� ������ ����

    public void ItemDrop()
    {
        // ��� ������ ó��
        foreach (ItemDrop itemDrop in itemDropList)
        {
            // ��� Ȯ���� ����Ͽ� �������� ������� ����
            if (Random.value < itemDrop.dropChance)
            {
                mobItemUI.SetActive(true);

            }
        }
    }
}

