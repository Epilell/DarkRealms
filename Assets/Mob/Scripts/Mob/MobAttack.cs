using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    private Animator animator;
    private bool attackChanger = false;
    private int pattrenChoicer;
    private bool IsAttack = false;
    private Transform PlayerDirection;
    private Vector3 playerPos;

    public Transform pos;
    public Vector2 boxSize = new Vector2(1, 1);
    [Header("Special Mob")]
    public bool isSlime = false;
    private Transform pos2;
    public Vector2 boxSize2 = new Vector2(1, 5);

    public bool isOrc = false;
    public float WhirlWindDuration = 5.0f;
    private bool OrcAtkChanger = false;

    [Header("MobAttacing")]
    private MobAttacking mobAttacking;

    private void Start()
    {
        if (this.transform.GetChild(0) != null)
        {
            pos = this.transform.GetChild(0);
            if (isSlime || isOrc)
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
        if (this.transform.GetChild(1) != null & (isSlime || isOrc))
        {
            pos2 = this.transform.GetChild(1);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(pos2.position, boxSize2);
        }
    }
    public void Attacking(MobStat mobStat, Transform P_direction)//공격
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
            }
            else
            {
                IsAttack = true;
                animator.SetTrigger("Attack1");
                //
                StartCoroutine(MeleeAttack(mobStat));
                //
                attackChanger = true;
            }
        }
        else if (mobStat.mobProperty == "range")//원거리몹 공격
        {
            pattrenChoicer = Random.Range(1, 6);
            if (pattrenChoicer > 4)
            {
                animator.SetTrigger("Attack2");
                IsAttack = false;
                RangeAttack2(mobStat);
                pattrenChoicer = Random.Range(1, 6);
            }
            else if (pattrenChoicer > 3)
            {
                animator.SetTrigger("Attack1");
                IsAttack = false;
                RangeAttack1(mobStat);
                pattrenChoicer = Random.Range(1, 6);
            }
            else if (pattrenChoicer > 2)
            {
                animator.SetTrigger("Attack2");
                IsAttack = false;
                RangeAttack3(mobStat);
                pattrenChoicer = Random.Range(1, 6);
            }
            else
            {
                animator.SetTrigger("Attack1");
                IsAttack = false;
                RangeAttack1(mobStat);
                pattrenChoicer = Random.Range(1, 6);
            }
        }
        else if (mobStat.mobProperty == "GoblinShaman")
        {
            StartCoroutine(GoblinShamanAtk(mobStat));
        }
        else if (mobStat.mobProperty == "Orc")
        {
            StartCoroutine(OrcAtk(mobStat.mobDamage));
        }
        else if (mobStat.mobProperty == "DoppleGanger")
        {
            animator.SetTrigger("Attack1");
            RangeAttack1(mobStat);
        }
        else
        {
            Debug.Log("mobStat.mobProperty 오류!");
        }
    }
    private void RangeAttack1(MobStat mobStat)
    {
        GameObject MobBullet = Instantiate(mobStat.bullet, mobStat.firePoint.transform.position, Quaternion.identity);
        MobBullet.GetComponent<MobRangeBullet>().SetStats(mobStat.bulletSpeed, mobStat.mobDamage, playerPos, 0);
        Rigidbody2D rb = MobBullet.GetComponent<Rigidbody2D>();
        rb.angularVelocity = 180;
        //rb.AddForce(playerPos * 0.3f, ForceMode2D.Impulse);
    }
    private void RangeAttack2(MobStat mobStat)
    {
        for (int i = 1; i < 5; i++)
        {
            GameObject MobBullet = Instantiate(mobStat.bullet, mobStat.firePoint.transform.position, Quaternion.identity);
            MobBullet.GetComponent<MobRangeBullet>().SetStats(mobStat.bulletSpeed, mobStat.mobDamage, playerPos, i);
            Rigidbody2D rb = MobBullet.GetComponent<Rigidbody2D>();
            rb.angularVelocity = 180;/*
            rb.AddForce(playerPos * 0.3f, ForceMode2D.Impulse) ;*/
            //rb.velocity = playerPos * -1f;
        }
    }
    private void RangeAttack3(MobStat mobStat)
    {
        for (int i = 5; i < 9; i++)
        {
            GameObject MobBullet = Instantiate(mobStat.bullet, mobStat.firePoint.transform.position, Quaternion.identity);
            MobBullet.GetComponent<MobRangeBullet>().SetStats(mobStat.bulletSpeed, mobStat.mobDamage, playerPos, i);
            Rigidbody2D rb = MobBullet.GetComponent<Rigidbody2D>();
            rb.angularVelocity = 180;
            rb.AddForce(playerPos.normalized * 1f, ForceMode2D.Impulse);
            rb.velocity = playerPos * -1f;
        }
    }
    protected IEnumerator MeleeAttack(MobStat mobStat)
    {

        yield return new WaitForSeconds(0.5f);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        if (isSlime)
        {
            foreach (Collider2D collider in collider2Ds)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "Player")
                {
                    collider.GetComponent<Player>().P_TakeDamage(mobStat.mobDamage);
                }
            }
            Collider2D[] collider2Ds2 = Physics2D.OverlapBoxAll(pos2.position, boxSize2, 0);
            foreach (Collider2D collider in collider2Ds2)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "Player")
                {
                    collider.GetComponent<Player>().P_TakeDamage(mobStat.mobDamage);
                }
            }
        }
        /*
        else if (isOrc)
        {
            if (OrcAtkChanger)
            {
                StartCoroutine(OrcAtk(mobStat.mobDamage));
                yield return new WaitForSeconds(5f);
                OrcAtkChanger = false;
            }
            else
            {
                animator.SetTrigger("whirlwindReady");
                StartCoroutine(OrcWhirlWind(mobStat.mobDamage / 10));
                yield return new WaitForSeconds(5f);
            }
        }*/
        else
        {
            foreach (Collider2D collider in collider2Ds)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "Player")
                {
                    collider.GetComponent<Player>().P_TakeDamage(mobStat.mobDamage);
                }
            }
        }
    }
    private IEnumerator DoppelAtk()
    {
        yield return new WaitForSeconds(0.5f);

    }
    private IEnumerator OrcAtk(float damage)
    {
        if (OrcAtkChanger)
        {
            animator.SetTrigger("Attack1");
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                Debug.Log(collider.tag);
                if (collider.tag == "Player")
                {
                    collider.GetComponent<Player>().P_TakeDamage(damage);
                }
            }
            OrcAtkChanger = false;
        }
        else
        {
            animator.SetBool("whirlwind", true);
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
                yield return new WaitForSeconds(0.2f);
            }

            animator.SetBool("whirlwind", false);

            yield return new WaitForSeconds(5f);
            OrcAtkChanger = true;
        }

    }
    private IEnumerator OrcWhirlWind(float damage)
    {
        animator.SetBool("whirlwind", true);
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
            yield return new WaitForSeconds(0.2f);
        }

        animator.SetBool("whirlwind", false);

        yield return new WaitForSeconds(5f);
        OrcAtkChanger = true;
    }
    private IEnumerator GoblinShamanAtk(MobStat mobStat)
    {
        yield return new WaitForSeconds(2f);
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerDirection.position);
        float minimumDistance = 2f;
        if (distanceToPlayer > minimumDistance)//플레이어가 최소 공격범위 밖에 있으면 패턴1사용
        {
            IsAttack = false;
            animator.SetBool("attack1", true);
            for (int i = 0; i < 5; i++)
            {
                GameObject MobBullet = Instantiate(mobStat.bullet, mobStat.firePoint.transform.position, Quaternion.identity);
                MobBullet.GetComponent<MobRangeBullet>().SetStats(mobStat.bulletSpeed, mobStat.mobDamage, playerPos, 9);
                yield return new WaitForSeconds(1f);
            }

            animator.SetBool("attack1", false);
        }
        else //플레이어가 최소 공격범위 안에있으면 패턴2 사용
        {
            animator.SetTrigger("Attack2");
            IsAttack = false;
            StartCoroutine(MeleeAttack(mobStat));
        }
    }
}
