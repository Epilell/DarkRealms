using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��¥ : 2021-03-28 PM 10:42:48
// �ۼ��� : Rito

namespace Rito.InventorySystem
{
    /// <summary> ��� ������ ���� </summary>
    [CreateAssetMenu(fileName = "Item_MaterialItem_", menuName = "Inventory System/Item Data/Material", order = 4)]
    public class MaterialItemData : CountableItemData
    {
        /// <summary> ȿ����(ȸ���� ��) </summary>
        public float Value => _value;
        [SerializeField] private float _value;
        public override Item CreateItem()
        {
            return new MaterialItem(this);
        }
    }
}