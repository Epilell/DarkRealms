using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 능력치 이름 및 레벨
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
    //플레이어 기본 능력치
    public float maxHP = 100f;

    public float speed = 3f;

    //방어구
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

    //플레이어 기본 능력치 레벨
    public List<Stat> Stats = new();
}

