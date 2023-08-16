using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefenceSkillData : SkillData
{
    public float DamageReduction => reductionRate;

    [SerializeField] private float reductionRate = 1f;

    public abstract void AdditionalInit();

    public abstract void AdditionalTimeCheck();
}
