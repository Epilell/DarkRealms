using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EvdShot
{
    public float coolTime;
}

[CreateAssetMenu(fileName = "EvadeShot", menuName = "Skill/EvadeShot")]
public class EvdshotData : ScriptableObject
{
    public List<EvdShot> upgradeList = new List<EvdShot>();
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
