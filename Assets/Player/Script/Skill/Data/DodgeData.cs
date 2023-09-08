using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dodge", menuName = "Skill/Utillity/Dodge")]
public class DodgeData : UtillitySkillData
{
    [Header("Upgrade GameObject", order = 1), Space(5)]
    [SerializeField] private GameObject BearTrap;

    [Header("Effect GameObject", order = 2), Space(5)]
    [SerializeField] private GameObject DashEffect;

    public GameObject GetBearTrap()
    {
        return BearTrap;
    }

    public GameObject GetDodgeEffect()
    {
        return DashEffect;
    }

    public override void AdditionalInit()
    {

    }

    public override void AdditionalTimeCheck()
    {

    }
}
