using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    Rigidbody2D rigid;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    public Transform player;

    public float detectionRange;
    public float mobAttackRange;
    public float moveSpeed;

    private bool isPlayerInRange = false;
    public int idleSpeed = 1;
    private int xSpeed = 0;
    private int ySpeed = 0;
    bool flipFlag = false;
    public bool IsAttack = false;
    private MobStat mobStat;
    string mobProperty;
    private bool attack1or2;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mobStat = GetComponent<MobStat>();
        //몹의 스텟을 가져옴
        mobProperty = mobStat.MobProperty();
        detectionRange = mobStat.DetectingRange();
        mobAttackRange = mobStat.MobAttackRange();
        moveSpeed = mobStat.MoveSpeed();
        attack1or2 = false;
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

        // player가 일정 거리 안에 있고 MobAttackRange 범위 안에 있으면 MobAttack스크립트를 호출합니다.
        if (isPlayerInRange && distanceToPlayer < mobAttackRange)
        {
            //attack1 한번 attack2 한번 번갈아가면서 공격
            if (attack1or2 == false)
            {
                attack1or2 = true;
                IsAttack = true;
                animator.SetTrigger("Attack2");
            }
            else
            {
                attack1or2 = false;
                IsAttack = true;
                animator.SetTrigger("Attack1");
            }
        }
        else if (isPlayerInRange)
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
        else
        {
            IsAttack = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
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
    private IEnumerator MobIdleMove()
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
}
