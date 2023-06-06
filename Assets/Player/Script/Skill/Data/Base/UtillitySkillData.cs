using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UtillitySkillData : SkillData
{
    //다른 스킬 추가시 공통부분 추가

    public abstract void AdditionalInit();

    public abstract void AdditionalTimeCheck();
}
