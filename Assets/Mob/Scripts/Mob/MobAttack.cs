using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    private Animator animator;
    private bool attackChanger = false;
    private int pattrenChoicer;
    private bool IsAttack = true;
    private Transform PlayerDirection;
    private GameObject player;

    public Transform pos;
    public Vector2 boxSize = new Vector2(1, 1);
    [Header("Special Mob")]
    public bool isSlime = false;
    private Transform pos2;
    public Vector2 boxSize2 = new Vector2(1, 5);

    public bool isOrc = false;
    public float WhirlWindDuration = 5.0f;
    private bool OrcAtkChanger = false;


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
    public void Attacking(MobStat mobStat, GameObject _player)//공격
    {
        player = GameObject.FindWithTag("Player");
        //Debug.Log(player.transform.position);
        PlayerDirection = player.transform;
        animator = GetComponent<Animator>();
        // 몹 공격 사운드 재생
        if (gameObject.name != "Slime(Clone)" && gameObject.name != "Orc(Clone)") 
            FindObjectOfType<SoundManager>().MobSound(gameObject.name.Replace("(Clone)", ""));
        if (mobStat.mobProperty == "melee")//근접몹 공격
        {
            //attack1 한번 attack2 한번 번갈아가면서 공격
            if (attackChanger)
            {
                IsAttack = false;
                animator.SetTrigger("Attack2");
                //
                StartCoroutine(MeleeAttack(mobStat));
                //
                attackChanger = false;
            }
            else
            {
                IsAttack = false;
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
                animator.SetTrigger("Attack1");
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
                animator.SetTrigger("Attack1");
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
    /// <summary>
    /// 즉시발사
    /// </summary>
    /// <param name="mobStat"></param>
    private void RangeAttack1(MobStat mobStat)
    {
        GameObject MobBullet = Instantiate(mobStat.bullet, mobStat.firePoint.transform.position, Quaternion.identity);
        MobBullet.GetComponent<MobRangeBullet>().SetStats(mobStat.bulletSpeed, mobStat.mobDamage, player, 0);
        Rigidbody2D rb = MobBullet.GetComponent<Rigidbody2D>();
        rb.angularVelocity = 180;
    }
    /// <summary>
    /// 상하좌우
    /// </summary>
    /// <param name="mobStat"></param>
    private void RangeAttack2(MobStat mobStat)
    {
        for (int i = 1; i < 5; i++)
        {
            GameObject MobBullet = Instantiate(mobStat.bullet, mobStat.firePoint.transform.position, Quaternion.identity);
            MobBullet.GetComponent<MobRangeBullet>().SetStats(mobStat.bulletSpeed+5f, mobStat.mobDamage, player, i);
            Rigidbody2D rb = MobBullet.GetComponent<Rigidbody2D>();
            rb.angularVelocity = 180;
        }
    }
    /// <summary>
    /// 4개 회전
    /// </summary>
    /// <param name="mobStat"></param>
    private void RangeAttack3(MobStat mobStat)
    {
        for (int i = 5; i < 9; i++)
        {
            GameObject MobBullet = Instantiate(mobStat.bullet, mobStat.firePoint.transform.position, Quaternion.identity);
            MobBullet.GetComponent<MobRangeBullet>().SetStats(mobStat.bulletSpeed, mobStat.mobDamage, player, i);
            Rigidbody2D rb = MobBullet.GetComponent<Rigidbody2D>();
            rb.angularVelocity = 180;
            Vector3 pd = player.transform.position;
            Vector3 dr = pd - transform.position;
            rb.AddRelativeForce(dr, ForceMode2D.Impulse);
            //rb.velocity = player.transform.position * -1f;
        }
    }
    protected IEnumerator MeleeAttack(MobStat mobStat)
    {
        yield return new WaitForSeconds(0.5f);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        if (isSlime)
        {
            if (attackChanger)//atk2
            {
                collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
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
                yield return new WaitForSeconds(0.5f);
                FindObjectOfType<SoundManager>().MobSound("Slime");
            }
            else//atk1
            {
                yield return new WaitForSeconds(0.5f);
                SlimeAtk1 slimeAtk1 = transform.Find("SlimeAtk1").gameObject.GetComponent<SlimeAtk1>();
                slimeAtk1.atkOn = true;
                SlimeAtk1 slimeAtk2 = transform.Find("SlimeAtk2").gameObject.GetComponent<SlimeAtk1>();
                slimeAtk2.atkOn = true;
                yield return new WaitForSeconds(0.5f);
                slimeAtk1.atkOn = false;
                slimeAtk2.atkOn = false;
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
                    yield return new WaitForSeconds(0.5f);
                    FindObjectOfType<SoundManager>().MobSound("Orc");
                }
            }
            OrcAtkChanger = false;
        }
        else
        {
            MobAI ai = GetComponent<MobAI>();
            ai.canMove = true;
            animator.SetBool("whirlwind", true);
            while (WhirlWindDuration > 0f)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos2.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    FindObjectOfType<SoundManager>().MobSound("OrcSpin");
                    Debug.Log(collider.tag);
                    if (collider.tag == "Player")
                    {
                        collider.GetComponent<Player>().P_TakeDamage(damage);
                    }
                }
                WhirlWindDuration -= 0.2f;
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
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerDirection.position);
        float minimumDistance = 2f;

        if(distanceToPlayer < minimumDistance)//플레이어가 최소 공격범위 안에있으면 패턴2 사용
        {
            animator.SetTrigger("Attack2");
            IsAttack = false;
            StartCoroutine(MeleeAttack(mobStat));
        }
        else //플레이어가 최소 공격범위 밖에 있으면 패턴1사용
        {
            MobAI ai = GetComponent<MobAI>();
            ai.canMove = false;
            IsAttack = false;
            animator.SetBool("attack1", true);
            for (int i = 0; i < 3; i++)
            {
                GameObject MobBullet = Instantiate(mobStat.bullet, mobStat.firePoint.transform.position+new Vector3(i-1f,0,0), Quaternion.identity);
                MobBullet.GetComponent<MobRangeBullet>().SetStats(mobStat.bulletSpeed, mobStat.mobDamage, player, 9);
                yield return new WaitForSeconds(1f);
            }

            ai.canMove = true;
            animator.SetBool("attack1", false);
            yield return new WaitForSeconds(10f);
            ai.canMove = true;
        }
    }
}
