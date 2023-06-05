using JetBrains.Annotations;
using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //Public Field
    #region

    [SerializeField]
    public GameObject curWeapon;

    #endregion

    //Private Field
    #region

    private readonly EquipmentInventory EI = EquipmentInventory.instance;

    #endregion

    //Public Method
    #region

    #endregion

    //Private Method
    #region
    private void ChangeWeapon(string WeaponName)
    {
        if (this.transform.childCount >= 1)
        {
            RemoveWeapon();
        }

        GameObject prefab = Resources.Load<GameObject>("Prefabs/"+WeaponName);
        curWeapon = prefab;
        GameObject Instance = Instantiate(prefab, this.transform);
        Instance.tag = "Weapon";
        Instance.GetComponent<WeaponBase>().SetStatus(EI.EqItems[2].Data as WeaponItemData);
    }

    private void RemoveWeapon()
    {
        for (int i = this.transform.childCount - 1; i >=0; i--)
        {
            Transform child = this.transform.GetChild(i);

            if (child.CompareTag("Weapon"))
            {
                Destroy(child.gameObject);
            }
            else
            {
                RemoveWeapon();
            }
        }
    }
    #endregion

    //Check Method
    #region

    /// <summary>
    /// 현재 장착한 무기의 종류를 확인하여 리턴
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private int CheckCurWeapon(Rito.InventorySystem.ItemData data)
    {
        string s1 = data.name;
        if (s1.Contains("rifle"))
        {
            return 1;
        }
        else if (s1.Contains("shotgun"))
        {
            return 2;
        }
        else if (s1.Contains("pistol"))
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }

    #endregion

    //Unity Event
    #region
    private void Update()
    {
        if (SkillManager.Instance.siegeIsActive)
        {
            //ChangeWeapon("dual pistol");
        }
        else
        {
            /*switch (CheckCurWeapon(EI.EqItems[2].Data))
        {
            case 0: //Null
                break;
            case 1: //rifle
                ChangeWeapon("rifle");
                break;
            case 2: //shotgun
                ChangeWeapon("shotgun");
                break;
            case 3: //pistol
                ChangeWeapon("pistol");
                break;
        }
        */
        }
    }
    #endregion
}
