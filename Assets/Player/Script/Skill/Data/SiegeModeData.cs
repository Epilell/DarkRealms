using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SiegeMode", menuName = "Skill/Defence/SiegeMode")]
public class SiegeModeData : DefenceSkillData
{
    public float SpeedReduction => speedReduction;
    [SerializeField] private float speedReduction;


    private bool isActive = false;
    public bool IsActive { get { return isActive; } set { isActive = value; } }

    public override void AdditionalInit()
    {
        Init();
        isActive = false;
    }

    public override void AdditionalTimeCheck()
    {
        if (!isActive)
        {
            TimeCheck();
        }
    }
}
