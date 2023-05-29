using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2021-03-28 PM 10:42:48
// 작성자 : Rito

namespace Rito.InventorySystem
{
    /// <summary> 재료 아이템 정보 </summary>
    [CreateAssetMenu(fileName = "Item_MaterialItem_", menuName = "Inventory System/Item Data/Material", order = 4)]
    public class MaterialItemData : CountableItemData
    {
        /// <summary> 효과량(회복량 등) </summary>
        public float Value => _value;
        [SerializeField] private float _value;
        public override Item CreateItem()
        {
            return new MaterialItem(this);
        }
    }
}