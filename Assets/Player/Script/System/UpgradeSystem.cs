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
        [SerializeField] private InvenData EquipmentInventory;
        [SerializeField] private Inventory inventory;

        [SerializeField] private RectTransform contentArea;

        // ����� �κ��丮�� ��
        private List<ItemData> latestEquipmentItemList;
        private List<Item> latestItemList;

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

        /// <summary>
        /// ����Ʈ�� ��ȭ�� �ִ��� ���� ( ������ T �ٸ��� F )
        /// </summary>
        /// <param name="list1">ItemData</param>
        /// <param name="list2">ItemData</param>
        /// <returns></returns>
        bool AreListsEqual(List<ItemData> list1, List<ItemData> list2)
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
        /// <param name="_items"></param>
        private void FindUpgradableItemAndMakeList(Item[] _items)
        {
            //������ ����Ʈ�� ����������
            if (_items != null && _items.Length != 0)
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
                for (int i = 0; i < EquipmentInventory.invenitems.Count; i++)
                {
                    //������ ��� �۵��� ���� ����
                    int index = i;

                    // 1. �������� null�� �ƴϰ� ���� ���׷��̵尡 �ִ� ���
                    if (_items[i] != null && _items[i].HasNextItemData())
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
                        if (_items[i].GetType() == typeof(ArmorItem))
                        {
                            //�ش� ���׷��̵� �г��� �̹��� �Ҵ�
                            ArmorItemData data = _items[i].Data as ArmorItemData;
                            var UI = RT.GetComponent<UpgradePanelUI>();
                            UI._beforeImage.sprite = data.IconSprite;
                            UI._afterImage.sprite = data.NextArmorData.IconSprite;
                            if (data.Requirements.Count != 0)
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
                                AttemptDirectArmorUpgrade(data, index);
                            });
                        }

                        // 1-2. ������ ���
                        else if (_items[i].GetType() == typeof(WeaponItem))
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
                                AttemptDirectWeaponUpgrade(data, index);
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
                for (int i = 0; i < _items.Length; i++)
                {
                    //������ ��� �۵��� ���� ����
                    int index = i;

                    // 1. �������� null�� �ƴϰ� ���� ���׷��̵尡 �ִ� ���
                    if (_items[i] != null && _items[i].HasNextItemData())
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
                        if (_items[i].GetType() == typeof(ArmorItem))
                        {
                            //�ش� ���׷��̵� �г��� �̹��� �Ҵ�
                            ArmorItemData data = _items[i].Data as ArmorItemData;
                            var UI = RT.GetComponent<UpgradePanelUI>();
                            UI._beforeImage.sprite = data.IconSprite;
                            UI._afterImage.sprite = data.NextArmorData.IconSprite;
                            if (data.Requirements.Count != 0)
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

                        // 1-2. ������ ���
                        else if (_items[i].GetType() == typeof(WeaponItem))
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
        private void AttemptArmorUpgrade(ArmorItemData _data, int _index)
        {
            if (_data.Requirements.Count != 0)
            {
                if (inventory.UseMaterial(_data.Requirements[0].Data, _data.Requirements[0].Num))
                {
                    inventory.Remove(_index);
                    inventory.Add(_data.NextArmorData);
                }
            }
            else
            {
                inventory.Remove(_index);
                inventory.Add(_data.NextArmorData);
            }
        }

        /// <summary>
        /// ���â �� ������ ��ȭ
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="_index"></param>
        private void AttemptDirectArmorUpgrade(ArmorItemData _data, int _index)
        {
            if (_data.Requirements.Count != 0)
            {
                if (inventory.UseMaterial(_data.Requirements[0].Data, _data.Requirements[0].Num))
                {
                    EquipmentInventory.invenitems[_index] = _data.NextArmorData;
                }
            }
            else
            {
                EquipmentInventory.invenitems[_index] = _data.NextArmorData;
            }
        }

        /// <summary> �κ��丮 ���� ������ ��ȭ </summary>
        /// <param name="_data"></param>
        /// <param name="_index"></param>
        private void AttemptWeaponUpgrade(WeaponItemData _data, int _index)
        {
            if (_data.Requirements.Count != 0)
            {
                if (inventory.UseMaterial(_data.Requirements[0].Data, _data.Requirements[0].Num))
                {
                    inventory.Remove(_index);
                    inventory.Add(_data.NextWeaponData);
                }
            }
            else
            {
                inventory.Remove(_index);
                inventory.Add(_data.NextWeaponData);
            }
        }

        /// <summary>
        /// ���â ���� ������ ���׷��̵�
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="_index"></param>
        private void AttemptDirectWeaponUpgrade(WeaponItemData _data, int _index)
        {
            if (_data.Requirements.Count != 0)
            {
                if (inventory.UseMaterial(_data.Requirements[0].Data, _data.Requirements[0].Num))
                {
                    EquipmentInventory.invenitems[_index] = _data.NextWeaponData;
                }
            }
            else
            {
                EquipmentInventory.invenitems[_index] = _data.NextWeaponData;
            }
        }
        #endregion

        #endregion

        // Unity Event
        #region .
        private void Start()
        {
            inventory = GameObject.FindWithTag("GameController").GetComponentInChildren<Inventory>();

            latestItemList = new List<Item>(inventory._Items);
            latestEquipmentItemList = EquipmentInventory.invenitems;
            FindUpgradableItemAndMakeList(inventory._Items);
        }

        private void Update()
        {
            if (inventory._Items != null && latestItemList != null && (!AreListsEqual(latestItemList, new List<Item>(inventory._Items)) || !AreListsEqual(latestEquipmentItemList, EquipmentInventory.invenitems) ))
            {
                contentArea.sizeDelta = new Vector2(contentArea.sizeDelta.x, 0);
                latestItemList = new List<Item>(inventory._Items);
                FindUpgradableItemAndMakeList(inventory._Items);
            }
        }
        #endregion
    }
}

