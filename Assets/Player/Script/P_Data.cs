using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObject/Player Data")]
public class P_Data : ScriptableObject
{
    //플레이어블 캐릭터별 기본정보

    public float maxHP = 100f;

    private float currentHP;
    public float CurrentHP { get { return CurrentHP; } set { CurrentHP = value; } }

    public float speed = 2f;


    //방어력
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

    public void HelmetUpgrade()
    {
        List<float> list = new List<float>();
        int upgradeNum = 0;
        list[upgradeNum] = 0;
    }

    public void BodyUpgrade()
    {

    }

    public void LegUpgrade()
    {

    }

    public void ShoesUpgrade()
    {

    }

    //방어력 총합 리턴
    public float GetArmor()
    {
        return helmet + body + leg + shoes;
    }

    //플레이어 능력치 레벨
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
    
    //각 능력 경험치
    public float Str_Exp = 0;
    public float Agi_Exp = 0;
    public float Int_Exp = 0;

}

