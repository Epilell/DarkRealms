using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2021-03-28 PM 10:38:33
// 작성자 : Rito

namespace Rito.InventorySystem
{
    /// <summary> List<>사용, 사용 재료와 개수를 저장 </summary>
    [Serializable]
    public class Requirements
    {
        public CountableItemData Data => _data;
        public int Num => _num;

        [SerializeField] private CountableItemData _data;
        [SerializeField] private int _num;
    }

    /// <summary> 장비 - 무기 아이템 </summary>
    [CreateAssetMenu(fileName = "Item_Weapon_", menuName = "Inventory System/Item Data/Weaopn", order = 1)]
    public class WeaponItemData : EquipmentItemData
    {
        /// <summary> 공격력 </summary>
        public int Damage => _damage;

        /// <summary> 업그레이드 후 아이템 정보 </summary>
        public WeaponItemData NextWeaponData => _nextWeaponData;

        /// <summary> 업그레이드에 필요한 재료 리스트 </summary>
        public List<Requirements> Requirements => _requirements;

        [SerializeField] private int _damage = 1;
        [SerializeField] private WeaponItemData _nextWeaponData;
        [SerializeField] private List<Requirements> _requirements = new List<Requirements>();
        public override Item CreateItem()
        {
            return new WeaponItem(this);
        }
    }
}