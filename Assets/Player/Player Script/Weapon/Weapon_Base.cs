using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon_Base : ScriptableObject
{
    //���� �̸�
    [SerializeField] private string W_Name;

    //���� ����
    [SerializeField] private float W_Damage = 10f;
    [SerializeField][Range(0f,100f)]
    private float W_Durabillty = 0f;
    [SerializeField] private float W_AttackTime = 1f;

    //���� ��� ����
    [SerializeField] private Animation Attack_Motion;

    //���� ��� ����
    [SerializeField] private Weapon_Mechanism Mechanism;


    private void Update()
    {
        if(W_AttackTime >= 0f)
        {
            W_AttackTime -= Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Mechanism.Attack();
            }
        }
    }

    private void FixedUpdate()
    {

    }
}
