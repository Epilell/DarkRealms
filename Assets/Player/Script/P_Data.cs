using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObject/Player Data")]
public class P_Data : ScriptableObject
{
    //플레이어블 캐릭터별 기본정보
    #region
    public float maxHP = 100f;

    public float speed = 2f;

    private float damage;
    public float Damage { get { return damage; } set { damage = value; } }

    #endregion

    //장비 방어력
    #region
    [Range(0, 100)]
    private float helmet;
    public float Helmet { get { return helmet; } set {  helmet = value; } }
    [Range(0, 100)]
    private float body;
    public float Body { get { return body; } set { body = value; } }
    [Range(0, 100)]
    private float leg;
    public float Leg { get { return leg ; } set {  leg = value; } }
    [Range(0, 100)]
    private float shoes;
    public float Shoes { get { return shoes; } set { shoes = value; } }

    //방어력 총합 리턴
    public float GetArmor()
    {
        return (helmet + body + leg + shoes) / 4;
    }
    #endregion

    //플레이어 능력치 레벨
    #region

    //각 능력 경험치
    public float Str_Exp = 0;
    public float Agi_Exp = 0;
    public float Int_Exp = 0;

    //힘
    private int Strength_Level = 0;

    public int GetStrLevel()
    {
        return Strength_Level;
    }

    public void StrLevelUP()
    {
        Strength_Level++;
        Str_Exp = 0;
    }

    //민첨
    private int Agility_Level = 0;

    public int GetAgiLevel()
    {
        return Agility_Level;
    }

    public void AgiLevelUP()
    {
        Agility_Level++;
        Agi_Exp = 0;
    }

    //지능
    private int Intelligent_Level = 0;

    public int GetIntLevel()
    {
        return Intelligent_Level;
    }

    public void IntLevelUP()
    {
        Intelligent_Level++;
        Int_Exp = 0;
    }

    #endregion
}

