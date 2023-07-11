using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    private Animator animator;
    private bool attackChanger = false;
    private bool IsAttack = false;
    private Transform PlayerDirection;
    private Vector3 playerPos;

    public Transform pos;
    public Vector2 boxSize = new Vector2(1,1);
    [Header("Special Mob")]
    public bool isSlime = false;
    private Transform pos2;
    public Vector2 boxSize2 = new Vector2(1, 5);

    public bool isOrc = false;
    public float WhirlWindDuration = 5.0f;
    private void Start()
    {
        if (this.transform.GetChild(0) != null)
        {
            pos = this.transform.GetChild(0);
            if (isSlime||isOrc)
            {
                pos2 = this.transform.GetChild(1);
            }
        }
    }
    protected void OnDrawGizmos()
    {
        pos = this.transform.GetChild(0);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
        if (this.transform.GetChild(1) != null & (isSlime||isOrc))
        {
            pos2 = this.transform.GetChild(1);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(pos2.position, boxSize2);
        }
    }
    public bool Attacking(MobStat mobStat, Transform P_direction)//공격
    {
        PlayerDirection = P_direction;
        playerPos = P_direction.position;
        animator = GetComponent<Animator>();
        if (mobStat.mobProperty == "melee")//근접몹 공격
        {
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
        else if (mobStat.mobProperty == "range")//원거리몹 공격
        {
            if (attackChanger)
            {
                animator.SetTrigger("Attack2");
                //발사하는코드
                attackChanger = false;
                IsAttack = false;
                RangeAttack2(mobStat);
                return IsAttack;
            }
            else
            {
                animator.SetTrigger("Attack1");
                IsAttack = false;
                RangeAttack1(mobStat);
                attackChanger = true;
                return IsAttack;
            }
        }
        else
        {
            return IsAttack;
        }
    }
    private void RangeAttack1(MobStat mobStat)
    {
        GameObject MobBullet = Instantiate(mobStat.bullet, mobStat.firePoint.transform.position, Quaternion.identity);
        MobBullet.GetComponent<MobRangeBullet>().SetStats(mobStat.bulletSpeed, mobStat.mobDamage, playerPos, 0);
    }
    private void RangeAttack2(MobStat mobStat)
    {
        for (int i = 1; i < 5; i++)
        {
            GameObject MobBullet = Instantiate(mobStat.bullet, mobStat.firePoint.transform.position, Quaternion.identity);
            MobBullet.GetComponent<MobRangeBullet>().SetStats(mobStat.bulletSpeed, mobStat.mobDamage, playerPos, i);
        }
    }
    protected IEnumerator MeleeAttack(MobStat mobStat)
    {
        yield return new WaitForSeconds(0.5f);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        if (isSlime)
        {
            Collider2D[]collider2Ds2 = Physics2D.OverlapBoxAll(pos2.position, boxSize2, 0);
            foreach (Collider2D collider in collider2Ds2)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "Player")
                {
                    collider.GetComponent<Player>().P_TakeDamage(mobStat.mobDamage);
                }
            }
        }
        else if (isOrc)
        {
            StartCoroutine(OrcWhirlWind(mobStat.mobDamage / 10));
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
    private IEnumerator OrcWhirlWind(float damage)
    {
        while (WhirlWindDuration > 0f)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos2.position, boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "Player")
                {
                    collider.GetComponent<Player>().P_TakeDamage(damage);
                }
            }
            WhirlWindDuration -= Time.deltaTime;
            yield return new WaitForSeconds (0.2f);
        }
    }
}
