using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Upgrade
{
    public float damage;
    [Range(10f, 2000f)]
    public float rpm;
    public int pelletNum;
}

[CreateAssetMenu (fileName = "Weapon Stats", menuName = "ScriptableObject/Weapon Stats")]
public class WeaponData : ScriptableObject
{
    public List<Upgrade> upgradeList = new List<Upgrade>();
    public int upgradeNum = 0;
    
    public void Upgrade()
    {
        if(upgradeNum < upgradeList.Count - 1)
        {
            upgradeNum++;
        }
        else
        {
            Debug.Log("���׷��̵� �Ұ���");
        }
    }

    //�Ѿ� ������
    public GameObject bulletPrefab;
}
