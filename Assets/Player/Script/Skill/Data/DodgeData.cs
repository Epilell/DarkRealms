using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dodge", menuName = "Skill/Utillity/Dodge")]
public class DodgeData : UtillitySkillData
{
    [Header("Upgrade GameObject")]
    [SerializeField] private GameObject obj;

    public GameObject GetObj()
    {
        return obj;
    }

    public override void AdditionalInit()
    {

    }

    public override void AdditionalTimeCheck()
    {

    }
}
