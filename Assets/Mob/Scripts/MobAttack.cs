using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    public Animator animator;

    public void Attack()
    {
        // "MobAttack" 애니메이션으로 전환합니다.
        animator.SetTrigger("MobAttack");
    }
}
