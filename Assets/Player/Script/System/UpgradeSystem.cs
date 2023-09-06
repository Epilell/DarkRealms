using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;
using TMPro;
using System;
using System.Linq;
using Unity.VisualScripting;
using System.Reflection;

namespace Rito.InventorySystem
{
    public class UpgradeSystem : MonoBehaviour
    {
        //Private Fields
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
        /// <summary>
        /// 리스트에 변화가 있는지 리턴 ( 같으면 T 다르면 F )
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
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
        /// <summary> 업그레이드 가능한 아이템 리스트를 가져오고 업그레이드 패널 생성 </summary>
        /// <param name="_items"></param>
        private void FindUpgradableItemAndMakeList(Item[] _items)
        {

            //아이템 리스트가 비어있을경우
            if (_items != null && _items.Length != 0)
            {
                //기존의 패널 삭제
                for (int i = _contentArea.childCount - 1; i >= 0; i--)
                {
                    Transform child = _contentArea.GetChild(i);
                    Destroy(child.gameObject);
                }
                int num = 0;
                // 아이템 리스트의 길이만큼 생성
                for (int i = 0; i < _items.Length; i++)
                {
                    int index = i;
                    //아이템이 null이 아니고 ArmorItem 또는 WeaponItem일 경우만
                    if (_items[i] != null && _items[i].HasNextItemData())
                    {
                        num++;
                        RectTransform RT = CloneUpgradePanel();
                        if (i == 0)
                        {
                            RT.localPosition = new Vector3(0,0,0);
                        }
                        else
                        {
                            RT.localPosition = new Vector2(0, -130 * (num - 1));
                        }
                        RT.gameObject.SetActive(true);
                        //RT.gameObject.name = $"Upgrade List [{i}]";
                        if (_items[i].GetType() == typeof(ArmorItem))
                        {
                            //해당 업그레이드 패널의 이미지 할당
                            ArmorItemData data = _items[i].Data as ArmorItemData;
                            var UI = RT.GetComponent<UpgradePanelUI>();
                            UI._beforeImage.sprite = data.IconSprite;
                            UI._afterImage.sprite = data.NextArmorData.IconSprite;
                            if(data.Requirements.Count != 0)
                            {
                                UI._requirements.text += data.Requirements[0].Num; //+ " X " + data.Requirements[0].Data.Name;
                            }
                            else
                            {
                                UI._requirements.text += " 0 ";
                            }
                            

                            //해당 패널의 업그레이드 버튼에 기능 할당
                            UI._upgradeButton.onClick.AddListener(() =>
                            {
                                AttemptArmorUpgrade(data, index);
                            });
                        }
                        if (_items[i].GetType() == typeof(WeaponItem))
                        {
                            //해당 업그레이드 패널의 이미지 할당
                            WeaponItemData data = (WeaponItemData)_items[i].Data;
                            UpgradePanelUI UI = RT.GetComponent<UpgradePanelUI>();
                            UI._beforeImage.sprite = data.IconSprite;
                            UI._afterImage.sprite = data.NextWeaponData.IconSprite;
                            if (data.Requirements.Count != 0)
                            {
                                UI._requirements.text += data.Requirements[0].Num;//+ " X " + data.Requirements[0].Data.Name;
                            }
                            else
                            {
                                UI._requirements.text += " 0 ";
                            }

                            //해당 패널의 업그레이드 버튼에 기능 할당
                            UI._upgradeButton.onClick.AddListener(() =>
                            {
                                AttemptWeaponUpgrade(data, index);
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
                _contentArea.sizeDelta = new Vector2(_contentArea.sizeDelta.x, num * 280);
            }
        }

        /// <summary> 방어구 강화 시도 </summary>
        /// <param name="_data"></param>
        /// <param name="_index"></param>
        private void AttemptArmorUpgrade(ArmorItemData _data, int _index)
        {
            if (_data.Requirements.Count != 0)
            {
                if (_inventory.UseMaterial(_data.Requirements[0].Data, _data.Requirements[0].Num))
                {
                    _inventory.Remove(_index);
                    _inventory.Add(_data.NextArmorData);
                }
            }
            else
            {
                _inventory.Remove(_index);
                _inventory.Add(_data.NextArmorData);
            }
        }

        /// <summary> 무기 강화 시도 </summary>
        /// <param name="_data"></param>
        /// <param name="_index"></param>
        private void AttemptWeaponUpgrade(WeaponItemData _data, int _index)
        {
            if(_data.Requirements.Count != 0)
            {
                if (_inventory.UseMaterial(_data.Requirements[0].Data, _data.Requirements[0].Num))
                {
                    _inventory.Remove(_index);
                    _inventory.Add(_data.NextWeaponData);
                }
            }
            else
            {
                _inventory.Remove(_index);
                _inventory.Add(_data.NextWeaponData);
            }
            
        }

        #endregion

        // Unity Event
        #region
        private void Start()
        {
            //_targetInventory = GameObject.FindWithTag("Warehouse");
            //_inventory = _targetInventory.GetComponent<Inventory>();

            _inventory = GameObject.FindWithTag("GameController").GetComponentInChildren<Inventory>();
        }

        private void Update()
        {
            if (_firstMake)
            {
                _latestItemList = new List<Item>(_inventory._Items);
                FindUpgradableItemAndMakeList(_inventory._Items);
                _firstMake = false;
            }

            if (_inventory._Items != null && _latestItemList != null && !AreListsEqual(_latestItemList, new List<Item>(_inventory._Items)))
            {
                _contentArea.sizeDelta = new Vector2(_contentArea.sizeDelta.x, 0);
                _latestItemList = new List<Item>(_inventory._Items);
                FindUpgradableItemAndMakeList( _inventory._Items);
            }
        }
        #endregion
    }
}

