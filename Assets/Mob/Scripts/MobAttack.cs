using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    public Animator animator;
    MobAI mobAI;
    public void Attack(string mobProperty)
    {
        // "MobAttack" 애니메이션으로 전환합니다.
        if (mobProperty == "melee")
        {
            animator.SetTrigger("Attack1");
        }
        else if(mobProperty == "range")
        {

        }
    }
}
