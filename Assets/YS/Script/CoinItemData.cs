using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��¥ : 2021-03-28 PM 10:42:48
// �ۼ��� : Rito

namespace Rito.InventorySystem
{
    /// <summary> �Һ� ������ ���� </summary>
    [CreateAssetMenu(fileName = "Item_Coin", menuName = "Inventory System/Item Data/Coin", order = 3)]
    public class CoinItemData : CountableItemData
    {
        /// <summary> ȿ����(ȸ���� ��) </summary>
        [SerializeField] private float _value;

        public override Item CreateItem()
        {
            return new CoinItem(this);
        }
    }
}