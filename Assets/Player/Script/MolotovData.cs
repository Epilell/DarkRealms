using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MolotovUpgrade
{
    public float coolTime, impactDamage, tickDamage, maxTime, radius, throwDistance;
}

[CreateAssetMenu(fileName = "Molotov", menuName = "Skill/Molotov")]
public class MolotovData : ScriptableObject
{
    public List<MolotovUpgrade> upgradeList = new List<MolotovUpgrade>();
    public int upgradeNum = 0;

    public void Upgrade()
    {
        if(upgradeNum < upgradeList.Count - 1)
        {
            upgradeNum++;
        }
        else
        {
            Debug.Log("업그레이드 불가능");
        }
    }

    public GameObject Molotov;

    //화염 프리팹
    public GameObject firePrefab;
}
