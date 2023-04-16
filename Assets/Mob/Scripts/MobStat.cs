using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStat : MonoBehaviour
{
    public float mobDamage = 5f;
    public float mobSpeed = 1f;
    public string mobProperty = "melee";
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
