using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownEffect : MonoBehaviour
{
    public Player player; // 플레이어 객체

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void SkillCoolDown()
    {
        FindObjectOfType<SkillManager>().molotovTS.canUse = true;
        FindObjectOfType<SkillManager>().molotovTS.curTime = 0;

        if (FindObjectOfType<SkillManager>().siegemodeTS.isActive == false)
        {
            FindObjectOfType<SkillManager>().siegemodeTS.canUse = true;
            FindObjectOfType<SkillManager>().siegemodeTS.curTime = 0;
        }

        FindObjectOfType<SkillManager>().dodgeTS.canUse = true;
        FindObjectOfType<SkillManager>().dodgeTS.curTime = 0;

        FindObjectOfType<SkillManager>().evdshotTS.canUse = true;
        FindObjectOfType<SkillManager>().evdshotTS.curTime = 0;

        FindObjectOfType<CoolDown>().isSkillUse = true;
    }
}
