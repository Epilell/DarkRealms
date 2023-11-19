using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStat : MonoBehaviour
{
    public float mobDamage = 5f;
    public string mobProperty = "melee";
    public float detectionRange = 10f;
    public float mobAttackRange = 1f;
    public float moveSpeed = 1f;
    public float mobAttackSpeed = 1f;
    public GameObject bullet;
    public GameObject firePoint;
    public float bulletSpeed;
    public float HPbar_correction=4.3f;
    public GameObject spawnEffect;
    // Start is called before the first frame update
    public float MobDamage()
    {
        return mobDamage;
    }
    public string MobProperty()
    {
        return mobProperty;
    }
    public float DetectingRange()
    {
        return detectionRange;
    }
    public float MobAttackRange()
    {
        return mobAttackRange;
    }
    public float MoveSpeed()
    {
        return moveSpeed;
    }
    public float MobAttackSpeed()
    {
        return mobAttackSpeed;
    }
    public GameObject Bullet()
    {
        return bullet;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
