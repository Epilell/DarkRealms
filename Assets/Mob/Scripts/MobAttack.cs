using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    public Animator animator;
    MobAI mobAI;
    public void Attack(bool isAttack)
    {
        // "MobAttack" �ִϸ��̼����� ��ȯ�մϴ�.
        animator.SetTrigger("MobAttack");
    }
}
