using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ɷ�ġ �̸� �� ����
/// </summary>
[Serializable]
public class Stat
{
    [SerializeField] private string name;
    public string Name { get { return name; } set { name = value; } }

    [SerializeField] private int level = 0;
    public int Level { get { return level; } set { level++; } }
}

[CreateAssetMenu(fileName = "Player Data", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    //�÷��̾� �⺻ �ɷ�ġ
    public float maxHP = 100f;

    public float speed = 3f;

    //��
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

    //�÷��̾� �⺻ �ɷ�ġ ����
    public List<Stat> Stats = new();
}

