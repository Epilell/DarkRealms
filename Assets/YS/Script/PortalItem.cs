using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.InventorySystem
{
    /// <summary> 수량 아이템 - 코인 아이템 </summary>
    public class PortalItem : CountableItem, IUsableItem
    {
        public PortalItem(PortalItemData data, int amount = 1) : base(data, amount) { }

        public bool Use()
        {
            Amount--;

            return true;
        }

        protected override CountableItem Clone(int amount)
        {
            return new PortalItem(CountableData as PortalItemData, amount);
        }
    }
}