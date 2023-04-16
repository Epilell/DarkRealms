using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    public Animator animator;
    MobAI mobAI;
    public void Attack(bool isAttack)
    {
        // "MobAttack" 애니메이션으로 전환합니다.
        animator.SetTrigger("MobAttack");
    }
}
