using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

[CreateAssetMenu(fileName = "Inven Data", menuName = "Inven Data/Inventory Data")]
public class InvenData : ScriptableObject
{
    public List<Rito.InventorySystem.ItemData> invenitems;
    public List<int> count;
    //public Rito.InventorySystem.Item[] equipitems;
}
