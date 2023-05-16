using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PItem : MonoBehaviour  // FieldItem의 기능 옮겨옴. 프리팹에 아이템 정보 리스트 저장 후 필드에 드롭할 수 있음
{
    public Item item; // 아이템의 정보
    public SpriteRenderer image; // 아이템의 이미지
    public List<Item> items = new();

    // _item으로 아이템 정보를 받아서 item 변수를 초기화하고, 해당 아이템의 이미지를 image에 표시
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.effects = _item.effects;
        item.effectPoint = _item.effectPoint;

        image.sprite = item.itemImage;  // 이미지 변경
    }

    public Item GetItem()
    {
        return item;
    }

    // 먹으면 필드 아이템 삭제
    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    internal void SetItem(object p)
    {
        throw new NotImplementedException();
    }
}