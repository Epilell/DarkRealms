using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    Rigidbody2D rigid;
    private Animator animator;
    SpriteRenderer spriteRenderer;
    private GameObject player;
    private MobAttack mobAttack;
    private MobStat mobStat;
    private MobHP mobHP;
    private SoundManager sm;
    private GameObject mobSpawnEffect;

    private float detectionRange = 10;
    private float mobAttackRange = 2;
    public float moveSpeed = 1;

    public int idleSpeed = 1;
    private int xSpeed = 0;
    private int ySpeed = 0;

    public bool canMove = false;
    public bool IsAttack = false;
    private float mobAttackSpeed;
    private float currentCoolDown = 0f;

    private Vector3 vec;
    private float vecX;
    float distanceToPlayer;


    private int spawndelay = 100;

    void Awake()
    {
        Init();
        InitStat();
        InitRigid();
    }
    private void Start()
    {
        // player를 찾아서 설정합니다.
        player = GameObject.FindGameObjectWithTag("Player");
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        //몹 소환 이펙트
        StartCoroutine(MobSpawnEffect());
    }

    private void FixedUpdate()
    {
        if (spawndelay > 0)
        {
            spawndelay--;
            return;
        }
        vec = this.transform.GetChild(0).localPosition;
        vecX = vec.x;
        if (currentCoolDown > 0.0f)//공격속도 설정
        {
            currentCoolDown -= Time.deltaTime;
        }
        if (!mobHP.IsHit && !mobHP.IsStun && mobHP.IsDie == false&&IsAttack==false)
        {
            // 현재 객체와 플레이어 사이의 거리 계산
            distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
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
        mobSpawnEffect = mobStat.spawnEffect;
    }
    private void InitRigid()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigid.gravityScale = 0;

    }
    private IEnumerator MobSpawnEffect()
    {
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        GameObject _SpawnEffect = Instantiate(mobSpawnEffect, this.transform.position, Quaternion.identity);
        _SpawnEffect.GetComponent<DecreaseAlpha>().SetUp(1f);
        yield return new WaitForSeconds(0.5f);
        Destroy(_SpawnEffect);

        color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;
    }

    private IEnumerator PerformAttack()
    {
        IsAttack = true;
        mobAttack.Attacking(mobStat, player);//공격하기
        //sm.MobSound("Shotgun");//소스필요함
        yield return new WaitForSeconds(2f);
        IsAttack = false;
    }
    private void Attack()
    {
        canMove = false;
        animator.SetInteger("WalkSpeed", 0);
        if (!IsAttack && currentCoolDown <= 0f)
        {
            currentCoolDown = mobAttackSpeed;
            StartCoroutine(PerformAttack());
        }
    }
    private void ChasePlayer()
    {
        if (mobHP.IsDie == false && (IsAttack == false || canMove == true))
        {
            animator.SetInteger("WalkSpeed", 1);
            IsAttack = false;
            // 플레이어를 따라가기 위해 이동
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            if (player.transform.position.x < rigid.position.x)
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
}
