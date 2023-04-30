using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Weapon Stats", menuName = "ScriptableObject/Weapon Stats")]
public class W_Data : ScriptableObject
{
    public GameObject W_Prefab;

    //무기 기본정보
    public float W_Damage;
    [Range(0.1f,5f)]
    public float W_AttackSpeed;
    public float W_Distance;
    public float W_Speed;
    
    //총알 프리팹
    public GameObject Bullet;
}
