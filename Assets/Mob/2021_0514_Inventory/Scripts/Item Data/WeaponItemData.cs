using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : 2021-03-28 PM 10:38:33
// 작성자 : Rito

namespace Rito.InventorySystem
{
    [Serializable]
    public class Requirements
    {
        public ItemData Data => _data;
        public int Num => _num;

        [SerializeField] private ItemData _data;
        [SerializeField] private int _num;
    }

    /// <summary> 장비 - 무기 아이템 </summary>
    [CreateAssetMenu(fileName = "Item_Weapon_", menuName = "Inventory System/Item Data/Weaopn", order = 1)]
    public class WeaponItemData : EquipmentItemData
    {
        /// <summary> 공격력 </summary>
        public float Damage => _damage;

        /// <summary>  분당 발사량  </summary>
        public float Rpm => _rpm;

        /// <summary> 발사시 총알 개수 </summary>
        public int PelletNum => _pelletNum;

        /// <summary> 업그레이드 후 아이템 정보 </summary>
        public WeaponItemData NextWeaponData => _nextWeaponData;

        /// <summary> 업그레이드에 필요한 재료 리스트 </summary>
        public List<Requirements> Requirements => _requirements;

        [SerializeField] private int _damage = 1;
        [SerializeField] private float _rpm;
        [SerializeField] private int _pelletNum;
        [SerializeField] private WeaponItemData _nextWeaponData;
        [SerializeField] private List<Requirements> _requirements = new List<Requirements>();
        public override Item CreateItem()
        {
            return new WeaponItem(this);
        }
    }
}