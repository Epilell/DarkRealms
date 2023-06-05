using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObject/Player Data")]
public class P_Data : ScriptableObject
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
    //��
    #region

    private int strlevel = 0;
    public int Strlevel { get { return strlevel; } set { strlevel++; Strexp = 0; } }

    private float strexp;
    public float Strexp { get { return strexp; } set { strexp = value; } }

    /// <summary> amount�� ��ŭ Strength ����ġ �߰� </summary>
    /// <param name="amount"></param>
    public void AddStrExp(float amount)
    {
        Strexp += amount;
    }

    #endregion

    //��÷
    #region

    private int agilevel = 0;
    public int Agilevel { get { return agilevel; } set { agilevel = value; Agiexp = 0; } }

    private float agiexp;
    public float Agiexp { get { return agiexp; } set { agiexp = value; } }

    /// <summary> amount�� ��ŭ Agillity ����ġ �߰� </summary>
    /// <param name="amount"></param>
    public void AddAgiExp(float amount)
    {
        Agiexp += amount;
    }

    #endregion

    //����
    #region

    private int intlevel = 0;
    public int Intlevel { get { return intlevel; } set { intlevel = value; Intexp = 0; } }

    private float intexp;
    public float Intexp { get { return intexp; } set { intexp = value; } }

    /// <summary> amount�� ��ŭ Intelligent ����ġ �߰� </summary>
    /// <param name="amount"></param>
    public void AddIntExp(float amount)
    {
        Intexp += amount;
    }

    #endregion

}

