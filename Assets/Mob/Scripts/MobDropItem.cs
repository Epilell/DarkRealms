using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrop
{
    //public GameObject itemPrefab; // 드롭 아이템 프리팹
    public Item item;//드롭 아이템
    public float dropChance; // 드롭 확률 (0 ~ 1 사이의 값)
}
public class MobDropItem : MonoBehaviour
{
    public GameObject mobItemUI;//열리는UI
    public ItemDrop[] itemDropList; // 몬스터가 드롭하는 아이템 정보

    public void ItemDrop()
    {
        // 드롭 아이템 처리
        foreach (ItemDrop itemDrop in itemDropList)
        {
            // 드롭 확률을 계산하여 아이템을 드롭할지 결정
            if (Random.value < itemDrop.dropChance)
            {
                mobItemUI.SetActive(true);

            }
        }
    }
}

