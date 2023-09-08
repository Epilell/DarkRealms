using UnityEngine;

namespace Rito.InventorySystem
{
    /// <summary> �Һ� ������ ���� </summary>
    [CreateAssetMenu(fileName = "Item_PortalItem", menuName = "Inventory System/Item Data/PortalItem", order = 3)]
    public class PortalItemData : CountableItemData
    {
        public override Item CreateItem()
        {
            return new PortalItem(this);
        }
    }
}