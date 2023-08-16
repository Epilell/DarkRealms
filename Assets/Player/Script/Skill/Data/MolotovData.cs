using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Molotov", menuName = "Skill/Attack/Molotov")]
public class MolotovData : AttackSkillData
{
    //ȭ���� ������
    public GameObject Molotov;

    //ȭ�� ������
    public GameObject firePrefab;

    public float Radius => radius;
    public float Distance => distance;

    [SerializeField] private float radius;
    [SerializeField] private float distance;

    public override void AdditionalInit()
    {

    }

    public override void AdditionalTimeCheck()
    {

    }
}
