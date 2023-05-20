using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dodge
{
    public float coolTime;
}

[CreateAssetMenu(fileName = "Dodge", menuName = "Skill/Dodge")]
public class DodgeData : ScriptableObject
{
    public List<Dodge> upgradeList = new List<Dodge>();
    public int upgradeNum = 0;

    public void Upgrade()
    {
        if (upgradeNum < upgradeList.Count - 1)
        {
            upgradeNum++;
        }
        else
        {
            Debug.Log("업그레이드 불가능");
        }
    }
}
