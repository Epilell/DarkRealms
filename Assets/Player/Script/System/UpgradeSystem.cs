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
        //Field
        #region .
        // ��� �����۰� �κ��丮 ������
        [SerializeField] private EquipmentInventory EquipmentInventory;
        [SerializeField] private Inventory inventory;

        //���׷��̵� ǥ�� ����
        [SerializeField] private RectTransform contentArea;

        // ����� �κ��丮�� ��
        [SerializeField] private List<Item> latestEquipmentItemList;
        [SerializeField] private List<Item> latestItemList;

        // ���׷��̵� �г� ������
        [SerializeField] private GameObject targetPanelPrefab;
        #endregion

        // Check Method
        #region .
        /// <summary>
        /// ����Ʈ�� ��ȭ�� �ִ��� ���� ( ������ T �ٸ��� F )
        /// </summary>
        /// <param name="list1">Item</param>
        /// <param name="list2">Item</param>
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

        //Method
        #region .

        //���׷��̵� �г� ����
        #region .
        /// <summary> ���׷��̵� ������ ������ ����Ʈ�� �������� ���׷��̵� �г� ���� </summary>
        /// <param name="_inventoryItems"></param>
        private void FindUpgradableItemAndMakeList(Item[] _inventoryItems, Item[] _equipmentItems)
        {
            //������ ����Ʈ�� ����������
            if ( (_inventoryItems != null && _inventoryItems.Length != 0) || (_equipmentItems != null && _equipmentItems.Length != 0) )
            {
                //������ �г� ����
                for (int i = contentArea.childCount - 1; i >= 0; i--)
                {
                    Transform child = contentArea.GetChild(i);
                    Destroy(child.gameObject);
                }

                //�г��� �� ���� ����
                int num = 0;

                // ���â ������ ����Ʈ�� ���̸�ŭ ����
                for (int i = 0; i < _equipmentItems.Length; i++)
                {
                    //������ ��� �۵��� ���� ����
                    int index = i;

                    // 1. �������� null�� �ƴϰ� ���� ���׷��̵尡 �ִ� ���
                    if (_equipmentItems[i] != null && _equipmentItems[i].HasNextItemData() )
                    {
                        num++;
                        RectTransform RT = CloneUpgradePanel();

                        //���׷��̵� �г��� ��ġ ����
                        if (num == 0)
                        {
                            RT.localPosition = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            RT.localPosition = new Vector2(0, -130 * (num - 1));
                        }
                        RT.gameObject.SetActive(true);

                        // 1-1. ���� ���
                        if (_equipmentItems[i].GetType() == typeof(ArmorItem))
                        {
                            //�ش� ���׷��̵� �г��� �̹��� �Ҵ�
                            ArmorItemData data = _equipmentItems[i].Data as ArmorItemData;
                            var UI = RT.GetComponent<UpgradePanelUI>();
                            UI._beforeImage.sprite = data.IconSprite;
                            UI._afterImage.sprite = data.NextArmorData.IconSprite;
                            if (data.Requirements.Count != 0)
                            {
                                UI._materialText.text += data.Requirements[0].Num;
                                UI._goldText.text += data.Requirements[0].Cost;
                            }
                            else
                            {
                                UI._materialText.text += "0";
                                UI._goldText.text += "0";
                            }

                            //�ش� �г��� ���׷��̵� ��ư�� ��� �Ҵ�
                            UI._upgradeButton.onClick.AddListener(() =>
                            {
                                AttemptArmorUpgrade(EquipmentInventory, data, index);
                            });
                        }

                        // 1-2. ������ ���
                        else if (_equipmentItems[i].GetType() == typeof(WeaponItem))
                        {
                            //�ش� ���׷��̵� �г��� �̹��� �Ҵ�
                            WeaponItemData data = _equipmentItems[i].Data as WeaponItemData;
                            UpgradePanelUI UI = RT.GetComponent<UpgradePanelUI>();
                            UI._beforeImage.sprite = data.IconSprite;
                            UI._afterImage.sprite = data.NextWeaponData.IconSprite;
                            if (data.Requirements.Count != 0)
                            {
                                UI._materialText.text += data.Requirements[0].Num;
                                UI._goldText.text += data.Requirements[0].Cost;
                            }
                            else
                            {
                                UI._materialText.text += "0";
                                UI._goldText.text += "0";
                            }

                            //�ش� �г��� ���׷��̵� ��ư�� ��� �Ҵ�
                            UI._upgradeButton.onClick.AddListener(() =>
                            {
                                AttemptWeaponUpgrade(EquipmentInventory, data, index);
                            });
                        }
                    }

                    RectTransform CloneUpgradePanel()
                    {
                        GameObject UpgradeList = Instantiate(targetPanelPrefab, contentArea);

                        RectTransform rt = UpgradeList.GetComponent<RectTransform>();

                        return rt;
                    }
                }

                // �κ��丮 ������ ����Ʈ�� ���̸�ŭ ����
                for (int j = 0; j < _inventoryItems.Length; j++)
                {
                    //������ ��� �۵��� ���� ����
                    int index = j;

                    // 1. �������� null�� �ƴϰ� ���� ���׷��̵尡 �ִ� ���
                    if (_inventoryItems[j] != null && _inventoryItems[j].HasNextItemData())
                    {
                        num++;
                        RectTransform RT = CloneUpgradePanel();

                        //���׷��̵� �г��� ��ġ ����
                        if (num == 0)
                        {
                            RT.localPosition = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            RT.localPosition = new Vector2(0, -130 * (num - 1));
                        }
                        RT.gameObject.SetActive(true);

                        // 1-1. ���� ���
                        if (_inventoryItems[j].GetType() == typeof(ArmorItem))
                        {
                            //�ش� ���׷��̵� �г��� �̹��� �Ҵ�
                            ArmorItemData data = _inventoryItems[j].Data as ArmorItemData;
                            var UI = RT.GetComponent<UpgradePanelUI>();
                            UI._beforeImage.sprite = data.IconSprite;
                            UI._afterImage.sprite = data.NextArmorData.IconSprite;
                            if (data.Requirements.Count != 0)
                            {
                                UI._materialText.text += data.Requirements[0].Num;
                                UI._goldText.text += data.Requirements[0].Cost;
                            }
                            else
                            {
                                UI._materialText.text += "0";
                                UI._goldText.text += "0";
                            }

                            //�ش� �г��� ���׷��̵� ��ư�� ��� �Ҵ�
                            UI._upgradeButton.onClick.AddListener(() =>
                            {
                                AttemptArmorUpgrade(inventory, data, index);
                            });
                        }

                        // 1-2. ������ ���
                        else if (_inventoryItems[j].GetType() == typeof(WeaponItem))
                        {
                            //�ش� ���׷��̵� �г��� �̹��� �Ҵ�
                            WeaponItemData data = (WeaponItemData)_inventoryItems[j].Data;
                            UpgradePanelUI UI = RT.GetComponent<UpgradePanelUI>();
                            UI._beforeImage.sprite = data.IconSprite;
                            UI._afterImage.sprite = data.NextWeaponData.IconSprite;
                            if (data.Requirements.Count != 0)
                            {
                                UI._materialText.text += data.Requirements[0].Num;
                                UI._goldText.text += data.Requirements[0].Cost;
                            }
                            else
                            {
                                UI._materialText.text += "0";
                                UI._goldText.text += "0";
                            }

                            //�ش� �г��� ���׷��̵� ��ư�� ��� �Ҵ�
                            UI._upgradeButton.onClick.AddListener(() =>
                            {
                                AttemptWeaponUpgrade(inventory, data, index);
                            });
                        }
                    }

                    RectTransform CloneUpgradePanel()
                    {
                        GameObject UpgradeList = Instantiate(targetPanelPrefab, contentArea);

                        RectTransform rt = UpgradeList.GetComponent<RectTransform>();

                        return rt;
                    }
                }

                //�г� ������ŭ ���׷��̵� ���� Ȯ��
                contentArea.sizeDelta = new Vector2(contentArea.sizeDelta.x, num * 280);
            }
        }
        #endregion

        //��ȭ ���
        #region .
        /// <summary> �κ��丮 �� ������ ��ȭ </summary>
        /// <param name="_data"></param>
        /// <param name="_index"></param>
        private void AttemptArmorUpgrade(Inventory _target, ArmorItemData _data, int _index)
        {
            if (_data.Requirements.Count != 0)
            {
                if (inventory.UseMaterial(_data.Requirements[0].Data, _data.Requirements[0].Num))
                {
                    if(inventory.UseMaterial(_data.Requirements[0].CostData, _data.Requirements[0].Cost))
                    {
                        _target.Remove(_index);
                        _target.Add(_data.NextArmorData);
                    }
                    else
                    {
                        inventory.Add(_data.Requirements[0].Data, _data.Requirements[0].Num);
                    }
                }
            }
            else
            {
                _target.Remove(_index);
                _target.Add(_data.NextArmorData);
            }
        }

        /// <summary>
        /// ����� �� ������ ��ȭ
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="_index"></param>
        private void AttemptArmorUpgrade(EquipmentInventory _target, ArmorItemData _data, int _index)
        {
            if (_data.Requirements.Count != 0)
            {
                if (inventory.UseMaterial(_data.Requirements[0].Data, _data.Requirements[0].Num))
                {
                    if (inventory.UseMaterial(_data.Requirements[0].CostData, _data.Requirements[0].Cost))
                    {
                        _target.RemoveItem(_index);
                        _target.Add(_data.NextArmorData);
                    }
                    else
                    {
                        inventory.Add(_data.Requirements[0].Data, _data.Requirements[0].Num);
                    }
                }
            }
            else
            {
                _target.RemoveItem(_index);
                _target.Add(_data.NextArmorData);
            }
        }

        /// <summary> �κ��丮 ���� ������ ��ȭ </summary>
        /// <param name="_data"></param>
        /// <param name="_index"></param>
        private void AttemptWeaponUpgrade(Inventory _target, WeaponItemData _data, int _index)
        {
            if (_data.Requirements.Count != 0)
            {
                if (inventory.UseMaterial(_data.Requirements[0].Data, _data.Requirements[0].Num))
                {
                    if(inventory.UseMaterial(_data.Requirements[0].CostData, _data.Requirements[0].Cost))
                    {
                        _target.Remove(_index);
                        _target.Add(_data.NextWeaponData);
                    }
                    else
                    {
                        inventory.Add(_data.Requirements[0].Data, _data.Requirements[0].Num);
                    }
                }
            }
            else
            {
                _target.Remove(_index);
                _target.Add(_data.NextWeaponData);
            }
        }

        /// <summary>
        /// ����� ���� ������ ��ȭ
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="_index"></param>
        private void AttemptWeaponUpgrade(EquipmentInventory _target, WeaponItemData _data, int _index)
        {
            if (_data.Requirements.Count != 0)
            {
                if (inventory.UseMaterial(_data.Requirements[0].CostData, _data.Requirements[0].Cost))
                {
                    _target.RemoveItem(_index);
                    _target.Add(_data.NextWeaponData);
                }
                else
                {
                    inventory.Add(_data.Requirements[0].Data, _data.Requirements[0].Num);
                }
            }
            else
            {
                _target.RemoveItem(_index);
                _target.Add(_data.NextWeaponData);
            }
        }
        #endregion
        #endregion

        // Unity Event
        #region .
        private void Start()
        {
            inventory = GameObject.FindWithTag("GameController").GetComponentInChildren<Inventory>();
            EquipmentInventory = inventory.EqInven;

            latestItemList = new List<Item>(inventory._Items);
            latestEquipmentItemList = new List<Item>(EquipmentInventory.EqItems);
            FindUpgradableItemAndMakeList(inventory._Items, EquipmentInventory.EqItems);
        }

        private void Update()
        {
            if (inventory._Items != null && latestItemList != null && (!AreListsEqual(latestItemList, new List<Item>(inventory._Items)) || !AreListsEqual(latestEquipmentItemList, new List<Item>(EquipmentInventory.EqItems)) ))
            {
                contentArea.sizeDelta = new Vector2(contentArea.sizeDelta.x, 0);
                latestItemList = new List<Item>(inventory._Items);
                latestEquipmentItemList = new List<Item>(EquipmentInventory.EqItems);
                FindUpgradableItemAndMakeList(inventory._Items, EquipmentInventory.EqItems);
            }
        }
        #endregion
    }
}

