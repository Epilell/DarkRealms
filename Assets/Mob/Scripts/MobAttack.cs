using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    public Animator animator;
    MobAI mobAI;
    public void Attack(string mobProperty)
    {
        // "MobAttack" �ִϸ��̼����� ��ȯ�մϴ�.
        if (mobProperty == "melee")
        {
            animator.SetTrigger("Attack1");
        }
        else if(mobProperty == "range")
        {

        }
    }
}
