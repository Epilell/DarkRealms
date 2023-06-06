using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * [상속 구조]  (가능하면 이런식으로)
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
    /// 스킬 시간 확인
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
    /// 남은 스킬 시간 리턴
    /// </summary>
    /// <returns></returns>
    public float GetRemainingTime()
    {
        return coolTime - curTime;
    }
}
