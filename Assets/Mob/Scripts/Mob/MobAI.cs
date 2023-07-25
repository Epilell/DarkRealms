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
    public float moveSpeed = 1;

    public int idleSpeed = 1;
    private int xSpeed = 0;
    private int ySpeed = 0;

    private bool IsAttack = false;
    private float mobAttackSpeed;
    private float currentCoolDown = 0f;

    private Vector3 vec;
    private float vecX;
    float distanceToPlayer;

    void Awake()
    {
        Init();
        InitStat();
        InitRigid();
    }
    private void Start()
    {
        // player를 찾아서 설정합니다.
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void FixedUpdate()
    {
        vec = this.transform.GetChild(0).localPosition;
        vecX = vec.x;
        if (currentCoolDown > 0.0f)//공격속도 설정
        {
            currentCoolDown -= Time.deltaTime;
        }

        if (!mobHP.IsHit||!mobHP.IsStun)
        {
            // 현재 객체와 플레이어 사이의 거리 계산
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer > detectionRange)//플레이어가 감지범위 밖에 있으면 idle
            {
                //idle
            }
            else if (distanceToPlayer <= mobAttackRange)//공격범위 안에있으면 공격
            {
                //공격
                Attack();
            }
            else//감지범위 안에있으면서 공격범위 밖에있으면 추적
            {
                ChasePlayer();
            }

        }
    }
    private void Init()
    {
        mobAttack = GetComponent<MobAttack>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mobHP = GetComponent<MobHP>();
    }
    private void InitStat()
    {
        mobStat = GetComponent<MobStat>();
        //몹의 스텟을 가져옴
        detectionRange = mobStat.DetectingRange();
        mobAttackRange = mobStat.MobAttackRange();
        moveSpeed = mobStat.MoveSpeed();
        mobAttackSpeed = mobStat.MobAttackSpeed();
    }
    private void InitRigid()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigid.gravityScale = 0;

    }


    private IEnumerator PerformAttack()
    {
        IsAttack = true;
        mobAttack.Attacking(mobStat, player);//공격하기
        yield return new WaitForSeconds(1f);
        IsAttack = false;
    }
    private void Attack()
    {
        if (!IsAttack && currentCoolDown <= 0f)
        {
            currentCoolDown = mobAttackSpeed;
            StartCoroutine(PerformAttack());
        }
    }
    private void ChasePlayer()
    {
        animator.SetInteger("WalkSpeed", 1);
        IsAttack = false;
        // 플레이어를 따라가기 위해 이동
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        if (player.position.x < rigid.position.x)
        {
            spriteRenderer.flipX = true;
            if (vecX > 0)
            {
                this.transform.GetChild(0).localPosition = new Vector3(-vecX, vec.y, vec.z);
            }
        }
        else
        {
            spriteRenderer.flipX = false;
            if (vecX < 0)
            {
                this.transform.GetChild(0).localPosition = new Vector3(-vecX, vec.y, vec.z);
            }
        }
    }
}
