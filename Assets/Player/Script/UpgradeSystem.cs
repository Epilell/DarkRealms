using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;
using TMPro;
using System;
using System.Linq;
using Unity.VisualScripting;

namespace Rito.InventorySystem
{
    public class UpgradeSystem : MonoBehaviour
    {
        //private Fields
        #region
        [SerializeField]
        private GameObject _targetInventory;
        [SerializeField]
        private Inventory _inventory;

        [SerializeField]
        private RectTransform _contentArea;
        /// <summary> 변경된 인벤토리랑 비교 </summary>
        [SerializeField]
        private List<Item> _latestItemList;
        [SerializeField]
        private GameObject _targetPanelPrefab;

        private bool _firstMake = true;
        #endregion

        // Check Method
        #region
        bool AreListsEqual(List<Item> list1, List<Item> list2)
        {
            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] == null || list2[i] == null)
                {
                    if (list1[i] != list2[i])
                        return false;
                }
                else if (!list1[i].Equals(list2[i]))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        // Private Method
        #region
        /// <summary>
        /// 업그레이드 가능한 아이템 리스트를 가져오고 업그레이드 패널 생성
        /// </summary>
        /// <param name="_items"></param>
        private void FindUpgradableItemAndMakeList(Item[] _items)
        {
            //아이템 리스트가 비어있을경우 ( Inventory에 아이템리스트가 List<>형식으로 안되어있어서 작동안됨 )
            //형식을 바꾸는 것도 고려해봄
            if (_items != null && _items.Length != 0)
            {
                //기존의 패널 삭제
                for (int i = _contentArea.childCount - 1; i >= 0; i--)
                {
                    Transform child = _contentArea.GetChild(i);
                    Destroy(child.gameObject);
                }

                // 아이템 리스트의 길이만큼 생성
                for (int i = 0; i < _items.Length; i++)
                {
                    int index = i;
                    //아이템이 null이 아니고 ArmorItem 또는 WeaponItem일 경우만
                    if (_items[i] != null && _items[i].HasNextItemData())
                    {
                        RectTransform RT = CloneUpgradePanel();
                        RT.localPosition = new Vector2(0, -280 * (_contentArea.childCount - 1));
                        RT.gameObject.SetActive(true);
                        //RT.gameObject.name = $"Upgrade List [{i}]";
                        if (_items[i].GetType() == typeof(ArmorItem))
                        {
                            //해당 업그레이드 패널의 이미지 할당
                            ArmorItemData data = _items[i].Data as ArmorItemData;
                            var UI = RT.GetComponent<UpgradePanelUI>();
                            UI._beforeImage.sprite = data.IconSprite;
                            UI._afterImage.sprite = data.NextArmorData.IconSprite;

                            //필요데이터와 수량을 넣음
                            /*if (data.Requirements.Count != 0)
                            {
                                for (int k = 0; k < data.Requirements.Count; k++)
                                {
                                    Requirements require = data.Requirements[k];
                                    _inventory.UseMaterial(require.Data, require.Num);
                                }
                            }*/

                            //해당 패널의 업그레이드 버튼에 기능 할당
                            UI._upgradeButton.onClick.AddListener(() =>
                            { 
                                _inventory.Remove(index);
                                _inventory.Add(data.NextArmorData);
                            });
                        }
                        if (_items[i].GetType() == typeof(WeaponItem))
                        {
                            //해당 업그레이드 패널의 이미지 할당
                            WeaponItemData data = (WeaponItemData)_items[i].Data;
                            UpgradePanelUI UI = RT.GetComponent<UpgradePanelUI>();
                            UI._beforeImage.sprite = data.IconSprite;
                            UI._afterImage.sprite = data.NextWeaponData.IconSprite;

                            //필요한 재료를 소진하는 기능을 버튼에 추가
                            /*if (data.Requirements.Count != 0)
                            {
                                for (int k = 0; k < data.Requirements.Count; k++)
                                {
                                    Requirements require = data.Requirements[k];
                                    _inventory.UseMaterial(require.Data, require.Num);
                                }
                            }*/

                            //해당 패널의 업그레이드 버튼에 기능 할당
                            UI._upgradeButton.onClick.AddListener(() =>
                            {
                                _inventory.Remove(index);
                                _inventory.Add(data.NextWeaponData);
                            });
                        }
                    }

                    RectTransform CloneUpgradePanel()
                    {
                        GameObject UpgradeList = Instantiate(_targetPanelPrefab,_contentArea);
                        RectTransform rt = UpgradeList.GetComponent<RectTransform>();

                        return rt;
                    }
                }
                _contentArea.sizeDelta = new Vector2(_contentArea.sizeDelta.x, _contentArea.childCount * 280);
            }
        }
        #endregion

        // Unity Event
        #region
        private void Start()
        {
            _targetInventory = GameObject.FindWithTag("Warehouse");
            _inventory = _targetInventory.GetComponent<Inventory>();
        }

        private void Update()
        {
            if (_firstMake)
            {
                _latestItemList = new List<Item>(_inventory._items);
                FindUpgradableItemAndMakeList(_inventory._items);
                _firstMake = false;
            }

            if (_inventory._items != null && _latestItemList != null && !AreListsEqual(_latestItemList, new List<Item>(_inventory._items)))
            {
                _contentArea.sizeDelta = new Vector2(_contentArea.sizeDelta.x, 0);
                _latestItemList = new List<Item>(_inventory._items);
                FindUpgradableItemAndMakeList( _inventory._items);
            }
        }
        #endregion
    }
}

