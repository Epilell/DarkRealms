using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Weapon Stats", menuName = "ScriptableObject/Weapon Stats")]
public class W_Data : ScriptableObject
{
    public GameObject W_Prefab;

    //���� �⺻����
    public float W_Damage;
    [Range(0.1f,5f)]
    public float W_AttackSpeed;
    public float W_Distance;
    public float W_Speed;
    
    //�Ѿ� ������
    public GameObject Bullet;
}
