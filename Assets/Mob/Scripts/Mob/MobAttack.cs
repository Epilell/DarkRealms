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
    private Transform _slimePos;
    public Vector2 boxSize = new Vector2(1,1);
    private void Awake()
    {
        if (this.transform.GetChild(0) != null)
        {
            pos = this.transform.GetChild(0);
            if (this.transform.GetChild(1) != null&&isSlime)
            {
                _slimePos = this.transform.GetChild(1);
            }
        }
    }
    protected void OnDrawGizmos()
    {
        pos = this.transform.GetChild(0);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
        if (this.transform.GetChild(1) != null & isSlime)
        {
            _slimePos = this.transform.GetChild(1);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_slimePos.position, new Vector2(1,5));
        }
    }
    public bool Attacking(MobStat mobStat, Transform P_direction)//공격
    {
        PlayerDirection = P_direction;
        MobStat = mobStat;
        if (MobStat.mobProperty == "melee")//근접몹 공격
        {
            if (isSlime)
            {
                //StartCoroutine("changeRadius");
            }
            //attack1 한번 attack2 한번 번갈아가면서 공격
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
        else if (MobStat.mobProperty == "range")//원거리몹 공격
        {
            if (attackChanger)
            {
                animator.SetTrigger("Attack2");
                //발사하는코드
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
    protected IEnumerator MeleeAttack(MobStat mobStat)
    {
        yield return new WaitForSeconds(0.5f);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        if (isSlime)
        {
            Collider2D[]collider2Ds2 = Physics2D.OverlapBoxAll(_slimePos.position, new Vector2(1,5), 0);
            foreach (Collider2D collider in collider2Ds2)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "Player")
                {
                    collider.GetComponent<Player>().P_TakeDamage(mobStat.mobDamage);
                }
            }
        }
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
