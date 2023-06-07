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
    /// �κ��丮���� ���⸦ �����ϴ� ���
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
        // ��� �κ��丮�� ������ ( ���� ���� )
        if (EquipmentInventory.instance != null)
        {
            //Debug.Log("��� �κ��丮 ����");
            // 1. �����尡 �����ִٸ�
            if (SkillManager.Instance.siegemodedata.IsActive)
            {
                //Debug.Log("������ ON");
                // 1-1. ���� ���Ⱑ ���ų� ���� ���Ⱑ dual pistol�� �ƴϸ�
                if (curWeaponName == null || curWeaponName != "dual pistol")
                {
                    //Debug.Log("���� �ٲ�");
                    ChangeSpecialWeapon("dual pistol");
                }
            }
            else // 2. �����尡 �������� ������
            {
                //Debug.Log("������ OFF");
                // 2-1. ���� �տ� ���Ⱑ ���ٸ� (����)
                if (curWeaponName == null)
                {
                    //Debug.Log("�տ� ���� ����");
                    // 2-1-1. �κ��丮���� ���Ⱑ ���ٸ�
                    if (EquipmentInventory.instance.EqItems[2] == null)
                    {
                        //Debug.Log("���â�� �������");
                    }
                    // 2-1-2. �κ��丮�� ���Ⱑ �ִٸ�
                    else
                    {
                        //Debug.Log("���� �ٲ�");
                        //���� �κ��丮 ���⸦ Ȯ���Ͽ� ����
                        ChangeWeapon(EquipmentInventory.instance.EqItems[2].Data as WeaponItemData);
                    }
                }
                // 2-2. ���� �տ� ���Ⱑ �ִٸ�
                else
                {
                    //Debug.Log("�տ� ��������");
                    // 2-2-1. �κ��丮���� ���Ⱑ ���ٸ�
                    if (EquipmentInventory.instance.EqItems[2] == null)
                    {
                        //Debug.Log("���� ����");
                        RemoveWeapon();
                    }
                    // 2-2-2. �κ��丮�� ���Ⱑ �ִµ� �ٸ� ������
                    else if (curWeaponName != EquipmentInventory.instance.EqItems[2].Data.Name)
                    {
                        //Debug.Log("���� �ٲ�");
                        //���� �κ��丮 ���⸦ Ȯ���Ͽ� ����
                        ChangeWeapon(EquipmentInventory.instance.EqItems[2].Data as WeaponItemData);
                    }
                }
            }
        }
    }
    #endregion
}
