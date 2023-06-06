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

    [Multiline]
    [SerializeField] private string skillName;
    [SerializeField] private string toolTip;
    [SerializeField] private float coolTime;

    private float curTime = 0f;
    public float CurTime { get { return curTime; } set { curTime = value; } }
    private bool canUse = false;
    public bool CanUse { get { return canUse; } set { canUse = value; } }

    public void Init()
    {
        CurTime = 0f;
        CanUse = false;
    }

    /// <summary>
    /// ��ų �ð� Ȯ��
    /// </summary>
    public void TimeCheck()
    {
        if ( CurTime < CoolTime)
        {
            CurTime += Time.deltaTime;
        }
        else
        {
            CanUse = true;
        }
    }

    /// <summary>
    /// ���� ��ų �ð� ����
    /// </summary>
    /// <returns></returns>
    public float GetRemainingTime()
    {
        return coolTime - curTime;
    }
}
