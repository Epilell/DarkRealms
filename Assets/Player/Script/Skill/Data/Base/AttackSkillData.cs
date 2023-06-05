using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkillData : SkillData
{
    public float Damage => damage;

    [SerializeField] private float damage;

    public abstract void AdditionalInit();

    public abstract void AdditionalTimeCheck();
}
