using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PItem : MonoBehaviour  // FieldItem�� ��� �Űܿ�. �����տ� ������ ���� ����Ʈ ���� �� �ʵ忡 ����� �� ����
{
    public Item item; // �������� ����
    public SpriteRenderer image; // �������� �̹���
    public List<Item> items = new();

    // _item���� ������ ������ �޾Ƽ� item ������ �ʱ�ȭ�ϰ�, �ش� �������� �̹����� image�� ǥ��
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.effects = _item.effects;
        item.effectPoint = _item.effectPoint;

        image.sprite = item.itemImage;  // �̹��� ����
    }

    public Item GetItem()
    {
        return item;
    }

    // ������ �ʵ� ������ ����
    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    internal void SetItem(object p)
    {
        throw new NotImplementedException();
    }
}