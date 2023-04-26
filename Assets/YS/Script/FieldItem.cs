using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public Item item; // �������� ����
    public SpriteRenderer image; // �������� �̹���

    // _item�� �޾Ƽ� item ������ �ʱ�ȭ�ϰ�, �ش� �������� �̹����� image�� ǥ��
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.effects = _item.effects;

        image.sprite = item.itemImage;  // �̹��� ����
    }

    // ������ ȹ��
    public Item GetItem()
    {
        return item;
    }

    // ������ �ʵ� ������ ����
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}