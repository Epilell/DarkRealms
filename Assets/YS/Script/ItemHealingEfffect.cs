using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable/Health")]
public class ItemHealingEfffect : ItemEffect { public override bool ExecuteRole() { return true; } }