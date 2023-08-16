using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    //�÷��̾� �⺻ �ɷ�ġ
    #region
    public float maxHP = 100f;

    public float speed = 3f;

    #endregion

    //����
    #region

    //���� ������
    private float damage;
    public float Damage { get { return damage; } set { damage = value; } }

    //�д� �߻簳��
    private float rpm;
    public float Rpm { get { return rpm; } set { rpm = value; } }

    //źȯ ����
    private int pelletNum;
    public int PelletNum { get { return pelletNum; } set { pelletNum = value; } }

    #endregion

    //��
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

    //���� ���� ����
    public float GetArmor()
    {
        return (helmet + body + leg + shoes) / 4;
    }
    #endregion

    //�÷��̾� Ư�� �ɷ�ġ ���� �� ����ġ
    #region

    //��
    private int strLevel = 0;
    public int StrLevel { get { return strLevel; } set { strLevel++; } }

    //��÷
    private int agiLevel = 0;
    public int AgiLevel { get { return agiLevel; } set { agiLevel++; } }

    //����
    private int intLevel = 0;
    public int IntLevel { get { return intLevel; } set { intLevel++; } }

    //���� ���µ�
    private int armorMasteryLevel = 0;
    public int ArmorMasteryLevel {  get {  return armorMasteryLevel; } set { armorMasteryLevel++; } }

    #endregion
}

