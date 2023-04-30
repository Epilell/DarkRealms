using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObject/Player Data")]
public class P_Data : ScriptableObject
{
    //�÷��̾�� ĳ���ͺ� �⺻����
    public string P_Name;

    public float P_MaxHp = 100f;
    public float P_CurrentHp;

    [Range(0.5f, 5f)]
    public float P_Speed = 2f;

    [Range(0, 100)]
    public float P_HelmetArmor = 0;
    [Range(0, 100)]
    public float P_BodyArmor = 0;
    [Range(0, 100)]
    public float P_LegArmor = 0;

    //�÷��̾� �ɷ�ġ ����
    public int Strength_Level = 0;
    public int Agility_Level = 0;
    public int Intelligent_Level = 0;

    public float Str_Exp = 0;
    public float Agi_Exp = 0;
    public float Int_Exp = 0;
}
