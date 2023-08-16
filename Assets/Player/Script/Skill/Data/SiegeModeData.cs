using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SiegeMode", menuName = "Skill/Defence/SiegeMode")]
public class SiegeModeData : DefenceSkillData
{
    public float SpeedReduction => speedReduction;
    [SerializeField] private float speedReduction;

    public override void AdditionalInit()
    {

    }

    public override void AdditionalTimeCheck()
    {

    }
}
