using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��¥ : 2021-03-28 PM 11:07:23
// �ۼ��� : Rito

namespace Rito.InventorySystem
{
    /// <summary> ���� ������ - ��� ������ </summary>
    public class MaterialItem : CountableItem
    {
        public MaterialItem(MaterialItemData data, int amount = 1) : base(data, amount) { }

        protected override CountableItem Clone(int amount)
        {
            return new MaterialItem(CountableData as MaterialItemData, amount);
        }
    }
}