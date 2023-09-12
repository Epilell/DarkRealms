using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class
#region

/*
 *  SkillList
 *      SkillUpgrade
 *      SkillUpgrade
 *      SkillUpgrade
 *      ...
 *  SkillList
 *      SkillUpgrade
 *      SkillUpgrade
 *      SkillUpgrade
 *      ...
 */

/// <summary>
/// 스킬 업그레이드 리스트
/// </summary>
[Serializable]
public class Skill
{
    //스킬명
    [SerializeField] private string skillName;
    public string SkillName { get { return skillName; } set {  skillName = value; } }

    //스킬 아이콘
    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } set { icon = value; } }

    //스킬 업그레이드 리스트
    [SerializeField] private List<SkillUpgrade> upgradeList;
    public List<SkillUpgrade> UpgradeList {get { return upgradeList; } set {  upgradeList = value; } }
}

/// <summary>
/// 스킬 업그레이드 리스트 요소
/// </summary>
[Serializable]
public class SkillUpgrade
{
    //스킬 업그레이드 이름
    [SerializeField] private string name;
    public string Name {get { return name; } set {  name = value; } }

    //업그레이드 여부
    [SerializeField] private bool isUpgrade;
    public bool IsUpgrade { get { return isUpgrade; } set { isUpgrade = value; } }
}

#endregion

[CreateAssetMenu(fileName = "Skill Upgrade Data", menuName = "Skill/Upgrade Data")]
public class SkillUpgradeList : ScriptableObject
{
    [Space(5)]
    public List<Skill> Upgradablelist = new();
    [Space(5)]
    public List<Skill> ApplyUpgradeList = new();
}
