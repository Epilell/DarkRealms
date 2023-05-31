using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Boss Data", menuName = "Mob Data/Boss Data")]
public class BossStat : ScriptableObject
{
    //보스 스텟
    [SerializeField]
    private float bossMaxHP = 100f;
    [SerializeField]
    private float bossDamageSmall = 10f;
    [SerializeField]
    private float bossDamageMiddle = 25f;
    [SerializeField]
    private float bossDamageStrong = 40f;
    //private bool patternChanger = false;//분노패턴 이전/이후
    public float BossHP =>bossMaxHP;
    public float BossDamageSmall => bossDamageSmall;
    public float BossDamageMiddle => bossDamageMiddle;
    public float BossDamageStrong => bossDamageStrong;

}
