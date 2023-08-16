using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��¥ : 2021-03-28 PM 11:07:23
// �ۼ��� : Rito

namespace Rito.InventorySystem
{
    /// <summary> ���� ������ - ���� ������ </summary>
    public class CoinItem : CountableItem, IUsableItem
    {
        public CoinItem(CoinItemData data, int amount = 1) : base(data, amount) { }

        public bool Use()
        {
            // �ӽ� : ���� �ϳ� ����
            Amount--;

            return true;
        }

        protected override CountableItem Clone(int amount)
        {
            return new CoinItem(CountableData as CoinItemData, amount);
        }
    }
}