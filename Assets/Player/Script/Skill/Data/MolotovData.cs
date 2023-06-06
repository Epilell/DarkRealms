using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Molotov", menuName = "Skill/Attack/Molotov")]
public class MolotovData : AttackSkillData
{
    //È­¿°º´ ÇÁ¸®ÆÕ
    public GameObject Molotov;

    //È­¿° ÇÁ¸®ÆÕ
    public GameObject firePrefab;

    public float TickDamage => tickDamage;
    public float MaxTime => maxTime;
    public float Radius => radius;
    public float ThrowDistance => throwDistance;

    [SerializeField] private float tickDamage;
    [SerializeField] private float maxTime;
    [SerializeField] private float radius;
    [SerializeField] private float throwDistance;

    public override void AdditionalInit()
    {

    }

    public override void AdditionalTimeCheck()
    {

    }
}
