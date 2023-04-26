using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public Item item; // 아이템의 정보
    public SpriteRenderer image; // 아이템의 이미지

    // _item을 받아서 item 변수를 초기화하고, 해당 아이템의 이미지를 image에 표시
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