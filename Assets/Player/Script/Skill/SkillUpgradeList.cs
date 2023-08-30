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
/// ��ų ���׷��̵� ����Ʈ
/// </summary>
[Serializable]
public class SkillList
{
    [SerializeField] private string skillName;
    public string SkillName { get { return skillName; } set {  skillName = value; } }
    [SerializeField] private List<SkillUpgrade> upgradeList;
    public List<SkillUpgrade> UpgradeList {get { return upgradeList; } set {  upgradeList = value; } }
}

/// <summary>
/// ��ų ���׷��̵� ����Ʈ ���
/// </summary>
[Serializable]
public class SkillUpgrade
{
    [SerializeField] private string name;
    public string Name {get { return name; } set {  name = value; } }
    [SerializeField] private bool isUpgrade;
    public bool IsUpgrade { get { return isUpgrade; } set { isUpgrade = value; } }
}

#endregion

[CreateAssetMenu(fileName = "Skill Upgrade Data", menuName = "Skill/Upgrade Data")]
public class SkillUpgradeList : ScriptableObject
{
    [Space(5)]
    public List<SkillList> Upgradablelist = new();
    [Space(5)]
    public List<SkillList> ApplyUpgradeList = new();
}
