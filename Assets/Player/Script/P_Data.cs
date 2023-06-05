using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObject/Player Data")]
public class P_Data : ScriptableObject
{
    //플레이어 기본 능력치
    #region
    public float maxHP = 100f;

    public float speed = 3f;

    #endregion

    //무기
    #region

    //무기 데미지
    private float damage;
    public float Damage { get { return damage; } set { damage = value; } }

    //분당 발사개수
    private float rpm;
    public float Rpm { get { return rpm; } set { rpm = value; } }

    //탄환 개수
    private int pelletNum;
    public int PelletNum { get { return pelletNum; } set { pelletNum = value; } }

    #endregion

    //방어구
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

    //플레이어 특수 능력치 레벨 및 경험치
    //힘
    #region

    private int strlevel = 0;
    public int Strlevel { get { return strlevel; } set { strlevel++; Strexp = 0; } }

    private float strexp;
    public float Strexp { get { return strexp; } set { strexp = value; } }

    /// <summary> amount양 만큼 Strength 경험치 추가 </summary>
    /// <param name="amount"></param>
    public void AddStrExp(float amount)
    {
        Strexp += amount;
    }

    #endregion

    //민첨
    #region

    private int agilevel = 0;
    public int Agilevel { get { return agilevel; } set { agilevel = value; Agiexp = 0; } }

    private float agiexp;
    public float Agiexp { get { return agiexp; } set { agiexp = value; } }

    /// <summary> amount양 만큼 Agillity 경험치 추가 </summary>
    /// <param name="amount"></param>
    public void AddAgiExp(float amount)
    {
        Agiexp += amount;
    }

    #endregion

    //지능
    #region

    private int intlevel = 0;
    public int Intlevel { get { return intlevel; } set { intlevel = value; Intexp = 0; } }

    private float intexp;
    public float Intexp { get { return intexp; } set { intexp = value; } }

    /// <summary> amount양 만큼 Intelligent 경험치 추가 </summary>
    /// <param name="amount"></param>
    public void AddIntExp(float amount)
    {
        Intexp += amount;
    }

    #endregion

}

