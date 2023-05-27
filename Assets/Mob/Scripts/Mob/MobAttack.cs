using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    public Animator animator;
    MobAI mobAI;
    MobStat MobStat;
    private bool attackChanger = false;
    private bool IsAttack = false;
    private Transform PlayerDirection;
    public bool Attacking(MobStat mobStat, Transform P_direction)//����
    {
        PlayerDirection = P_direction;
        MobStat = mobStat;
        if (MobStat.mobProperty == "melee")//������ ����
        {
            //attack1 �ѹ� attack2 �ѹ� �����ư��鼭 ����
            if (attackChanger)
            {
                IsAttack = true;
                animator.SetTrigger("Attack2");
                attackChanger = false;
                return IsAttack;
            }
            else
            {
                IsAttack = true;
                animator.SetTrigger("Attack1");
                attackChanger = true;
                return IsAttack;
            }
        }
        else if (MobStat.mobProperty == "range")//���Ÿ��� ����
        {
            if (attackChanger)
            {
                animator.SetTrigger("Attack2");
                //�߻��ϴ��ڵ�
                attackChanger = false;
                IsAttack = false;
                RangeAttack2();
                return IsAttack;
            }
            else
            {
                animator.SetTrigger("Attack1");
                IsAttack = false;
                RangeAttack1();
                attackChanger = true;
                return IsAttack;
            }
        }
        else
        {
            return IsAttack;
        }
    }
    private void RangeAttack1()
    {
        GameObject MobBullet = Instantiate(MobStat.bullet, MobStat.firePoint.transform.position, Quaternion.identity);
        MobBullet.GetComponent<MobRangeBullet>().SetStats(MobStat.bulletSpeed, MobStat.mobDamage, PlayerDirection, 0);
    }
    private void RangeAttack2()
    {
        for (int i = 1; i < 5; i++)
        {
            GameObject MobBullet = Instantiate(MobStat.bullet, MobStat.firePoint.transform.position, Quaternion.identity);
            MobBullet.GetComponent<MobRangeBullet>().SetStats(MobStat.bulletSpeed, MobStat.mobDamage, null, i);
        }
    }
}
