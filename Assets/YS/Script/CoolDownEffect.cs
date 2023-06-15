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
        FindObjectOfType<SkillManager>().molotovdata.CanUse = true;
        FindObjectOfType<SkillManager>().molotovdata.CurTime = 0;

        if (FindObjectOfType<SkillManager>().siegemodedata.IsActive == false)
        {
            FindObjectOfType<SkillManager>().siegemodedata.CanUse = true;
            FindObjectOfType<SkillManager>().siegemodedata.CurTime = 0;
        }

        FindObjectOfType<SkillManager>().dodgedata.CanUse = true;
        FindObjectOfType<SkillManager>().dodgedata.CurTime = 0;

        FindObjectOfType<SkillManager>().evdshotdata.CanUse = true;
        FindObjectOfType<SkillManager>().evdshotdata.CurTime = 0;

        FindObjectOfType<CoolDown>().isSkillUse = true;
    }
}
