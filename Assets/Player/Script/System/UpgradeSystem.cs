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
        /// <summary> ����� �κ��丮�� �� </summary>
        [SerializeField]
        private List<Item> _latestItemList;
        [SerializeField]
        private GameObject _targetPanelPrefab;

        private bool _firstMake = true;
        #endregion

        // Check Method
        #region
        /// <summary>
        /// ����Ʈ�� ��ȭ�� �ִ��� ���� ( ������ T �ٸ��� F )
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
        /// <summary> ���׷��̵� ������ ������ ����Ʈ�� �������� ���׷��̵� �г� ���� </summary>
        /// <param name="_items"></param>
        private void FindUpgradableItemAndMakeList(Item[] _items)
        {

            //������ ����Ʈ�� ����������
            if (_items != null && _items.Length != 0)
            {
                //������ �г� ����
                for (int i = _contentArea.childCount - 1; i >= 0; i--)
                {
                    Transform child = _contentArea.GetChild(i);
                    Destroy(child.gameObject);
                }
                int num = 0;
                // ������ ����Ʈ�� ���̸�ŭ ����
                for (int i = 0; i < _items.Length; i++)
                {
                    int index = i;
                    //�������� null�� �ƴϰ� ArmorItem �Ǵ� WeaponItem�� ��츸
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
                            //�ش� ���׷��̵� �г��� �̹��� �Ҵ�
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
                            

                            //�ش� �г��� ���׷��̵� ��ư�� ��� �Ҵ�
                            UI._upgradeButton.onClick.AddListener(() =>
                            {
                                AttemptArmorUpgrade(data, index);
                            });
                        }
                        if (_items[i].GetType() == typeof(WeaponItem))
                        {
                            //�ش� ���׷��̵� �г��� �̹��� �Ҵ�
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

                            //�ش� �г��� ���׷��̵� ��ư�� ��� �Ҵ�
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

        /// <summary> �� ��ȭ �õ� </summary>
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

        /// <summary> ���� ��ȭ �õ� </summary>
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

