using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SiegeMode
{
    public float coolTime;
    //0.1�̸� ������ ���� 10%
    [Range(0f,1f)]
    public float ArmorReduction;
}

[CreateAssetMenu(fileName = "SiegeMode", menuName = "Skill/SiegeMode")]
public class SiegeModeData : ScriptableObject
{
    public List<SiegeMode> upgradeList = new List<SiegeMode>();
    public int upgradeNum = 0;

    public void Upgrade()
    {
        if (upgradeNum < upgradeList.Count - 1)
        {
            upgradeNum++;
        }
        else
        {
            Debug.Log("���׷��̵� �Ұ���");
        }
    }
}
