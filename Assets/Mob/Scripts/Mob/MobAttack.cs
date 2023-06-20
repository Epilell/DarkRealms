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
    public bool isSlime=false;

    public float AtkDelay = 0.5f;

    public Transform pos;
    public Vector2 boxSize = new Vector2(1,1);
    private void OnDrawGizmos()
    {
        pos = this.transform.GetChild(0);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
    public bool Attacking(MobStat mobStat, Transform P_direction)//����
    {
        PlayerDirection = P_direction;
        MobStat = mobStat;
        if (MobStat.mobProperty == "melee")//������ ����
        {
            if (isSlime)
            {
                //StartCoroutine("changeRadius");
            }
            //attack1 �ѹ� attack2 �ѹ� �����ư��鼭 ����
            if (attackChanger)
            {
                IsAttack = true;
                animator.SetTrigger("Attack2");
                //
                StartCoroutine(MeleeAttack(mobStat));
                //
                attackChanger = false;
                return IsAttack;
            }
            else
            {
                IsAttack = true;
                animator.SetTrigger("Attack1");
                //
                StartCoroutine(MeleeAttack(mobStat));
                //
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
    private IEnumerator MeleeAttack(MobStat mobStat)
    {
        yield return new WaitForSeconds(0.5f);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            Debug.Log(collider.tag);
            if (collider.tag == "Player")
            {
                collider.GetComponent<Player>().P_TakeDamage(mobStat.mobDamage);
            }
        }
    }
    private IEnumerator changeRadius()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = 3f;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<CircleCollider2D>().radius = 0.5f;
    }
}
