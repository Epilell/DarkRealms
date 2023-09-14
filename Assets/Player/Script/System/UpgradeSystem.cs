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
        // 장비 아이템과 인벤토리 아이템
        [SerializeField] private InvenData EquipmentInventory;
        [SerializeField] private Inventory inventory;

        [SerializeField] private RectTransform contentArea;

        // 변경된 인벤토리랑 비교
        private List<ItemData> latestEquipmentItemList;
        private List<Item> latestItemList;

        // 업그레이드 패널 프리팹
        [SerializeField] private GameObject targetPanelPrefab;
        #endregion

        // Check Method
        #region .
        /// <summary>
        /// 리스트에 변화가 있는지 리턴 ( 같으면 T 다르면 F )
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
        /// 리스트에 변화가 있는지 리턴 ( 같으면 T 다르면 F )
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

        //업그레이드 패널 생성
        #region .
        /// <summary> 업그레이드 가능한 아이템 리스트를 가져오고 업그레이드 패널 생성 </summary>
        /// <param name="_items"></param>
        private void FindUpgradableItemAndMakeList(Item[] _items)
        {
            //아이템 리스트가 비어있을경우
            if (_items != null && _items.Length != 0)
            {
                //기존의 패널 삭제
                for (int i = contentArea.childCount - 1; i >= 0; i--)
                {
                    Transform child = contentArea.GetChild(i);
                    Destroy(child.gameObject);
                }

                //패널의 총 개수 저장
                int num = 0;

                // 장비창 아이템 리스트의 길이만큼 생성
                for (int i = 0; i < EquipmentInventory.invenitems.Count; i++)
                {
                    //리스너 기능 작동을 위한 변수
                    int index = i;

                    // 1. 아이템이 null이 아니고 다음 업그레이드가 있는 경우
                    if (_items[i] != null && _items[i].HasNextItemData())
                    {
                        num++;
                        RectTransform RT = CloneUpgradePanel();

                        //업그레이드 패널의 위치 조정
                        if (num == 0)
                        {
                            RT.localPosition = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            RT.localPosition = new Vector2(0, -130 * (num - 1));
                        }
                        RT.gameObject.SetActive(true);

                        // 1-1. 방어구인 경우
                        if (_items[i].GetType() == typeof(ArmorItem))
                        {
                            //해당 업그레이드 패널의 이미지 할당
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

                            //해당 패널의 업그레이드 버튼에 기능 할당
                            UI._upgradeButton.onClick.AddListener(() =>
                            {
                                AttemptDirectArmorUpgrade(data, index);
                            });
                        }

                        // 1-2. 무기인 경우
                        else if (_items[i].GetType() == typeof(WeaponItem))
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

                // 인벤토리 아이템 리스트의 길이만큼 생성
                for (int i = 0; i < _items.Length; i++)
                {
                    //리스너 기능 작동을 위한 변수
                    int index = i;

                    // 1. 아이템이 null이 아니고 다음 업그레이드가 있는 경우
                    if (_items[i] != null && _items[i].HasNextItemData())
                    {
                        num++;
                        RectTransform RT = CloneUpgradePanel();

                        //업그레이드 패널의 위치 조정
                        if (num == 0)
                        {
                            RT.localPosition = new Vector3(0, 0, 0);
                        }
                        else
                        {
                            RT.localPosition = new Vector2(0, -130 * (num - 1));
                        }
                        RT.gameObject.SetActive(true);

                        // 1-1. 방어구인 경우
                        if (_items[i].GetType() == typeof(ArmorItem))
                        {
                            //해당 업그레이드 패널의 이미지 할당
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

                            //해당 패널의 업그레이드 버튼에 기능 할당
                            UI._upgradeButton.onClick.AddListener(() =>
                            {
                                AttemptArmorUpgrade(data, index);
                            });
                        }

                        // 1-2. 무기인 경우
                        else if (_items[i].GetType() == typeof(WeaponItem))
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
                        GameObject UpgradeList = Instantiate(targetPanelPrefab, contentArea);

                        RectTransform rt = UpgradeList.GetComponent<RectTransform>();

                        return rt;
                    }
                }

                //패널 개수만큼 업그레이드 영역 확장
                contentArea.sizeDelta = new Vector2(contentArea.sizeDelta.x, num * 280);
            }
        }
        #endregion

        //강화 기능
        #region .
        /// <summary> 인벤토리 방어구 아이템 강화 </summary>
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
        /// 장비창 방어구 아이템 강화
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

        /// <summary> 인벤토리 무기 아이템 강화 </summary>
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
        /// 장비창 무기 아이템 업그레이드
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

