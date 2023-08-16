using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [��� ����]  (�����ϸ� �̷�������)
 * 
 *  -SkillData
 *  
 *      -AttackSkillData
 *          -MolotovData
 *          -BearTrapData
 *          -EvadeShotData
 *          
 *      -DefenceSkillData
 *          -SiegeModeData
 *          
 *      -UtillitySkillData
 *          -DodgeData
 */

public abstract class SkillData : ScriptableObject
{
    public string Name => skillName;
    public string Tooltip => toolTip;
    public float CoolTime => coolTime;
    public int MaxStack => maxStack;

    [Header("Common Field"), Space(2)]
    [Multiline]
    [SerializeField] private string skillName;
    [SerializeField] private string toolTip;
    [SerializeField] private float coolTime;
    [SerializeField] private int maxStack;
}
