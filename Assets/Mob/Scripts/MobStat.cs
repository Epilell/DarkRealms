using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStat : MonoBehaviour
{
    public float mobDamage = 5f;
    public float mobSpeed = 1f;
    public string mobProperty = "melee";
    public float detectionRange = 10f;
    public float mobAttackRange = 1f;
    public float moveSpeed = 1f;
    // Start is called before the first frame update
    public float MobDamage()
    {
        return mobDamage;
    }
    public float MobSpeed()
    {
        return mobSpeed;
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
