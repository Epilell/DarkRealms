using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType  // 열거형으로 아이템 종류 구분
{
    Equipment,  // 장비 아이템
    Consumables,  // 소모 아이템
    Etc  // 기타(조합 재료 등)
}

[System.Serializable]
public class Item
{
    public ItemType itemType;  // 위에서 만든 아이템 종류
    public string itemName;
    public Sprite itemImage;
    public float effectPoint; // 버프 수치
    public List<ItemEffect> effects;  // 아이템 효과

    public bool Use()  // 아이템 사용시 호출
    {
        bool isUsed = false;

        foreach (ItemEffect effect in effects)  // effects 목록의 각 효과를 순회하면서 실행
        {
            isUsed = effect.ExecuteRole();  // 효과 중 하나라도 성공하면 true를 반환
        }

        return isUsed; // true 반환 -> Slot의 OnPointerUp의 isUse가 참이됨
    }
}