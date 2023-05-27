using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    Rigidbody2D rigid;
    private Animator animator;
    SpriteRenderer spriteRenderer;
    private Transform player;
    private MobAttack mobAttack;
    private MobStat mobStat;
    private MobHP mobHP;

    private float detectionRange = 10;
    private float mobAttackRange = 2;
    private float moveSpeed = 1;

    private bool isPlayerInRange = false;
    public int idleSpeed = 1;
    private int xSpeed = 0;
    private int ySpeed = 0;
    bool flipFlag = false;
    private bool IsAttack = false;
    private string mobProperty;
    private float mobAttackSpeed;
    private float currentCoolDown = 0f;

    void Awake()
    {
        mobAttack = GetComponent<MobAttack>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mobStat = GetComponent<MobStat>();
        mobHP = GetComponent<MobHP>();
        //몹의 스텟을 가져옴
        mobProperty = mobStat.MobProperty();
        detectionRange = mobStat.DetectingRange();
        mobAttackRange = mobStat.MobAttackRange();
        moveSpeed = mobStat.MoveSpeed();
        mobAttackSpeed = mobStat.MobAttackSpeed();
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    private void Start()
    {
        // player를 찾아서 설정합니다.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(MobIdleMove());
    }

    private void FixedUpdate()
    {
        // 현재 객체와 플레이어 사이의 거리 계산
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            isPlayerInRange = true; // 감지 범위 내에 있다면 isPlayerInRange 변수를 true로 설정
        }
        else
        {
            isPlayerInRange = false; // 감지 범위 밖에 있다면 isPlayerInRange 변수를 false로 설정
        }
        AI(distanceToPlayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)//근접공격
    {
        //근접몹이면
        if (mobProperty == "melee")
        {
            if (IsAttack == true && collision.gameObject.CompareTag("Player"))
            {
                //플레이어의 HP를 몬스터의 공격력만큼 깎음
                collision.gameObject.GetComponent<Player>().P_TakeDamage(mobStat.mobDamage);
            }
        }
    }
    private IEnumerator MobIdleMove()//적 감지 X시 움직임
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        while (distanceToPlayer > detectionRange)
        {
            Think();
            //뒤집기
            if (xSpeed > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (xSpeed < 0)
            {
                spriteRenderer.flipX = true;
            }
            rigid.velocity = new Vector2(xSpeed, ySpeed);
            yield return new WaitForSeconds(3f);
        }
    }
    private void Think()
    {
        xSpeed = Random.Range(-idleSpeed, idleSpeed + 1);
        ySpeed = Random.Range(-idleSpeed, idleSpeed + 1);
        int speed = 1;
        if (ySpeed == 0 && xSpeed == 0)
        {
            speed = 0;
        }
        animator.SetInteger("WalkSpeed", speed);
    }

    private void AI(float distanceToPlayer)
    {
        if (mobHP.IsDie == true || mobHP.IsHit == true)
        {
            return;//사망시 or Hit시 실행X
        }
        // player가 일정 거리 안에 있고 
        if (isPlayerInRange)
        {
            if (distanceToPlayer < mobAttackRange)//공격범위안에 있으면
            {
                if (currentCoolDown > 0.0f)//공격속도 설정
                {
                    currentCoolDown -= Time.deltaTime;
                }
                else
                {
                    currentCoolDown = mobAttackSpeed;
                    mobAttack.Attacking(mobStat, player);//공격하기
                }
            }
            else
            {
                animator.SetInteger("WalkSpeed", 1);
                IsAttack = false;
                // 플레이어를 따라가기 위해 이동
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

                if (flipFlag == false && player.position.x < rigid.position.x)
                {
                    flipFlag = true;
                    spriteRenderer.flipX = true;
                }
                else if (flipFlag == true && player.position.x > rigid.position.x)
                {
                    flipFlag = false;
                    spriteRenderer.flipX = false;
                }
            }
        }
        else
        {
            IsAttack = false;
        }
    }
    /*
    private void Attacking()//공격
    {
        if (mobProperty == "melee")//근접몹 공격
        {
            //attack1 한번 attack2 한번 번갈아가면서 공격
            if (attackChanger)
            {
                IsAttack = true;
                animator.SetTrigger("Attack2");
                attackChanger = false;
            }
            else
            {
                IsAttack = true;
                animator.SetTrigger("Attack1");
                attackChanger = true;
            }
        }
        else if (mobProperty == "range")//원거리몹 공격
        {
            if (attackChanger)
            {
                animator.SetTrigger("Attack2");
                //발사하는코드
                attackChanger = false;
            }
            else
            {
                animator.SetTrigger("Attack1");
                //발사하는코드
                attackChanger = true;
            }
        }
    }
    private void RangeAttack1()
    {
        GameObject bullet = Instantiate(mobStat.bullet, firePoint.position, Quaternion.identity);
    }
    private void RangeAttack2()
    {

    }
    */
}
