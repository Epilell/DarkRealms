using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStat : MonoBehaviour
{
    //보스 스텟
    [SerializeField]
    private float bossHP = 100f;
    [SerializeField]
    private float bossDamageA = 10f;
    [SerializeField]
    private float bossDamageB = 25f;
    [SerializeField]
    private float bossDamageC = 40f;
    //private bool patternChanger = false;//분노패턴 이전/이후
    public float BossHP =>bossHP;
    public float BossDamageA => bossDamageA;
    public float BossDamageB => bossDamageB;
    public float BossDamageC => bossDamageC;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
