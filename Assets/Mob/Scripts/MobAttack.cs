using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    public Animator animator;

    public void Attack()
    {
        // "MobAttack" �ִϸ��̼����� ��ȯ�մϴ�.
        animator.SetTrigger("MobAttack");
    }
}
