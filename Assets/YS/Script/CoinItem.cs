using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2021-03-28 PM 11:07:23
// 작성자 : Rito

namespace Rito.InventorySystem
{
    /// <summary> 수량 아이템 - 코인 아이템 </summary>
    public class CoinItem : CountableItem, IUsableItem
    {
        public CoinItem(CoinItemData data, int amount = 1) : base(data, amount) { }

        public bool Use()
        {
            // 임시 : 개수 하나 감소
            Amount--;

            return true;
        }

        protected override CountableItem Clone(int amount)
        {
            return new CoinItem(CountableData as CoinItemData, amount);
        }
    }
}