using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkillData : SkillData
{
    //Field
    #region

    public float Damage => damage;
    
    public string CCType => ccType;
    public float CCDuration => ccDuration;

    [Header("Data Field"), Space(2)]
    [SerializeField] private float damage;
    [Tooltip("CCType : freezing(빙결 슬로우), stun(스턴), burn(화상), reducedDefense(방깎)")]
    [SerializeField] private string ccType;
    [SerializeField] private float ccDuration;

    #endregion

    //Abstract Method
    #region
    public abstract void AdditionalInit();

    public abstract void AdditionalTimeCheck();
    #endregion
}
