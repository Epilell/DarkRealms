using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
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
    #region

    //힘
    private int strLevel = 0;
    public int StrLevel { get { return strLevel; } set { strLevel++; } }

    //민첨
    private int agiLevel = 0;
    public int AgiLevel { get { return agiLevel; } set { agiLevel++; } }

    //지능
    private int intLevel = 0;
    public int IntLevel { get { return intLevel; } set { intLevel++; } }

    //갑옷 숙력도
    private int armorMasteryLevel = 0;
    public int ArmorMasteryLevel {  get {  return armorMasteryLevel; } set { armorMasteryLevel++; } }

    #endregion
}

