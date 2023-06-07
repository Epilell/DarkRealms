using JetBrains.Annotations;
using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //Public Field
    #region

    [SerializeField]
    public string curWeaponName = null;

    #endregion

    //Private Field
    #region

    #endregion

    //Public Method
    #region

    #endregion

    //Private Method
    #region

    /// <summary>
    /// 인벤토리에서 무기를 변경하는 경우
    /// </summary>
    /// <param name="weaponName"></param>
    private void ChangeWeapon(Rito.InventorySystem.WeaponItemData data)
    {
        if (this.transform.childCount >= 1)
        {
            RemoveWeapon();
        }

        curWeaponName = data.Name;

        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + RemoveNumber(data.Name));
        GameObject Instance = Instantiate(prefab, this.transform);
        Instance.name = data.Name;
        Instance.GetComponent<WeaponBase>().SetStatus(EquipmentInventory.instance.EqItems[2].Data as WeaponItemData);
    }

    /// <summary>
    /// 스킬에 의해서 무기를 변경하는 경우 ( 아이템 데이터가 한개인 경우 )
    /// </summary>
    /// <param name="weaponName"></param>
    private void ChangeSpecialWeapon(string weaponName)
    {
        if (this.transform.childCount >= 1)
        {
            RemoveWeapon();
        }

        curWeaponName = weaponName;

        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + weaponName);
        GameObject Instance = Instantiate(prefab, this.transform);
        Instance.name = weaponName;
    }

    /// <summary>
    /// 변경전에 기존에 있던 무기 제거
    /// </summary>
    private void RemoveWeapon()
    {
        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = this.transform.GetChild(i);

            Destroy(child.gameObject);
        }

        curWeaponName = null;
    }
    #endregion

    //Check Method
    #region

    private string RemoveNumber(string name)
    {
        return Regex.Replace(name, @"[^a-zA-Z]", "");
    }

    #endregion

    //Unity Event
    #region

    private void Start()
    {
        curWeaponName = null;
    }

    private void Update()
    {
        // 장비 인벤토리가 있으면 ( 실제 적용 )
        if (EquipmentInventory.instance != null)
        {
            //Debug.Log("장비 인벤토리 있음");
            // 1. 시즈모드가 켜져있다면
            if (SkillManager.Instance.siegemodedata.IsActive)
            {
                //Debug.Log("시즈모드 ON");
                // 1-1. 현재 무기가 없거나 현재 무기가 dual pistol이 아니면
                if (curWeaponName == null || curWeaponName != "dual pistol")
                {
                    //Debug.Log("무기 바꿈");
                    ChangeSpecialWeapon("dual pistol");
                }
            }
            else // 2. 시즈모드가 켜져있지 않으면
            {
                //Debug.Log("시즈모드 OFF");
                // 2-1. 현재 손에 무기가 없다면 (지속)
                if (curWeaponName == null)
                {
                    //Debug.Log("손에 무기 없음");
                    // 2-1-1. 인벤토리에도 무기가 없다면
                    if (EquipmentInventory.instance.EqItems[2] == null)
                    {
                        //Debug.Log("장비창에 무기없음");
                    }
                    // 2-1-2. 인벤토리에 무기가 있다면
                    else
                    {
                        //Debug.Log("무기 바꿈");
                        //현재 인벤토리 무기를 확인하여 변경
                        ChangeWeapon(EquipmentInventory.instance.EqItems[2].Data as WeaponItemData);
                    }
                }
                // 2-2. 현재 손에 무기가 있다면
                else
                {
                    //Debug.Log("손에 무기있음");
                    // 2-2-1. 인벤토리에는 무기가 없다면
                    if (EquipmentInventory.instance.EqItems[2] == null)
                    {
                        //Debug.Log("무기 없앰");
                        RemoveWeapon();
                    }
                    // 2-2-2. 인벤토리에 무기가 있는데 다른 무기라면
                    else if (curWeaponName != EquipmentInventory.instance.EqItems[2].Data.Name)
                    {
                        //Debug.Log("무기 바꿈");
                        //현재 인벤토리 무기를 확인하여 변경
                        ChangeWeapon(EquipmentInventory.instance.EqItems[2].Data as WeaponItemData);
                    }
                }
            }
        }
    }
    #endregion
}
