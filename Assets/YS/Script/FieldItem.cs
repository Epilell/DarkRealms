using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;

    // 전달받은 아이템으로 현재 클래스의 아이템 초기화
    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;
        item.effects = _item.effects;

        image.sprite = item.itemImage;  // 이미지 변경
    }

    // 아이템 획득
    public Item GetItem()
    {
        return item;
    }

    // 먹으면 필드 아이템 삭제
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}