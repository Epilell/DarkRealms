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
    /// �κ��丮���� ���⸦ �����ϴ� ���
    /// </summary>
    /// <param name="weaponName"></param>
    private void ChangeWeapon(string weaponName)
    {
        if (this.transform.childCount >= 1)
        {
            RemoveWeapon();
        }

        curWeaponName = weaponName;

        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + weaponName);
        GameObject Instance = Instantiate(prefab, this.transform);
        Instance.name = weaponName;
        Instance.GetComponent<WeaponBase>().SetStatus(EquipmentInventory.instance.EqItems[2].Data as WeaponItemData);
    }

    /// <summary>
    /// ��ų�� ���ؼ� ���⸦ �����ϴ� ��� ( ������ �����Ͱ� �Ѱ��� ��� )
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
    /// �������� ������ �ִ� ���� ����
    /// </summary>
    private void RemoveWeapon()
    {
        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = this.transform.GetChild(i);

            Destroy(child.gameObject);
        }
    }
    #endregion

    //Check Method
    #region

    /// <summary>
    /// ���� ������ ������ ������ Ȯ���Ͽ� ����
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private int CheckCurWeapon(Rito.InventorySystem.ItemData data)
    {
        if(data == null)
        {
            return 0;
        }
        else
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
        // ��� �κ��丮�� ������ ( ���� ���� )
        if (EquipmentInventory.instance != null)
        {
            Debug.Log("a");
            // 1. �����尡 �����ִٸ�
            if (SkillManager.Instance.siegemodedata.IsActive)
            {
                Debug.Log("1");
                // 1-1. ���� ���Ⱑ ���ų� ���� ���Ⱑ dual pistol�� �ƴϸ�
                if (curWeaponName == null || curWeaponName != "dual pistol")
                {
                    ChangeSpecialWeapon("dual pistol");
                    Debug.Log("1-1");
                }
            }
            else // 2. �����尡 �������� ������
            {
                Debug.Log("2");
                // 2-1. ���� �տ� ���Ⱑ ���ٸ� (����)
                if (curWeaponName == null)
                {
                    Debug.Log("2-1");
                    // 2-1-1. �κ��丮���� ���Ⱑ ���ٸ�
                    if (EquipmentInventory.instance.EqItems[2] == null)
                    {
                        Debug.Log("There's no weapon in Equipment");
                    }
                    // 2-1-2. �κ��丮�� ���Ⱑ �ִٸ�
                    else
                    {
                        Debug.Log("2-1-2");
                        //���� �κ��丮 ���⸦ Ȯ���Ͽ� ����
                        switch (CheckCurWeapon(EquipmentInventory.instance.EqItems[2].Data))
                        {
                            case 0:
                                curWeaponName = null;
                                RemoveWeapon();
                                break;
                            case 1:
                                ChangeWeapon("rifle");
                                break;
                            case 2:
                                ChangeWeapon("shotgun");
                                break;
                            case 3:
                                ChangeWeapon("pistol");
                                break;
                        }
                    }
                }
                // 2-2. ���� �տ� ���Ⱑ �ִٸ�
                else
                {
                    Debug.Log("2-2");
                    // 2-2-1. �κ��丮���� ���Ⱑ ���ٸ�
                    if (EquipmentInventory.instance.EqItems[2] == null)
                    {
                        Debug.Log("2-2-1");
                        curWeaponName = null;
                        RemoveWeapon();
                    }
                    // 2-2-2. �κ��丮�� ���Ⱑ �ִµ� �ٸ� ������
                    else if (curWeaponName != EquipmentInventory.instance.EqItems[2].Data.Name)
                    {
                        Debug.Log("2-2-2");
                        //���� �κ��丮 ���⸦ Ȯ���Ͽ� ����
                        switch (CheckCurWeapon(EquipmentInventory.instance.EqItems[2].Data))
                        {
                            case 0:
                                curWeaponName = null;
                                RemoveWeapon();
                                break;
                            case 1:
                                ChangeWeapon("rifle");
                                break;
                            case 2:
                                ChangeWeapon("shotgun");
                                break;
                            case 3:
                                ChangeWeapon("pistol");
                                break;
                        }
                    }
                }
            }
        }
        //��� �κ��丮�� �����ϴ� ��� ( �׽�Ʈ�� )
        else
        {
            Debug.Log("b");
            if (SkillManager.Instance.siegemodedata.IsActive)
            {
                Debug.Log("1");
                // 1-1. ���� ���Ⱑ ���ų� ���� ���Ⱑ dual pistol�� �ƴϸ�
                if (curWeaponName == null || curWeaponName != "dual pistol")
                {
                    ChangeSpecialWeapon("dual pistol");
                    Debug.Log("1-1");
                }
            }
            else // 2. �����尡 �������� ������
            {
                Debug.Log("2");
                // 2-1. ���� �տ� ���Ⱑ ���ٸ� (����)
                if (curWeaponName == null)
                {
                    Debug.Log("2-1");
                }
                // 2-2. ���� �տ� ���Ⱑ �ִٸ�
                else
                {
                    Debug.Log("2-2");
                    curWeaponName = null;
                    RemoveWeapon();
                }
            }
        }
    }
    #endregion
}
