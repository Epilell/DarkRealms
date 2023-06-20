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

    public int idleSpeed = 1;
    private int xSpeed = 0;
    private int ySpeed = 0;
    private bool IsAttack = false;
    private string mobProperty;
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
        // player�� ã�Ƽ� �����մϴ�.
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void FixedUpdate()
    {
        vec = this.transform.GetChild(0).localPosition;
        vecX = vec.x;
        if (currentCoolDown > 0.0f)//���ݼӵ� ����
        {
            currentCoolDown -= Time.deltaTime;
        }

        if (!mobHP.IsHit)
        {
            // ���� ��ü�� �÷��̾� ������ �Ÿ� ���
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer > detectionRange)//�÷��̾ �������� �ۿ� ������ idle
            {
                //idle
            }
            else if (distanceToPlayer <= mobAttackRange)//���ݹ��� �ȿ������� ����
            {
                //����
                Attack();
            }
            else//�������� �ȿ������鼭 ���ݹ��� �ۿ������� ����
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
        //���� ������ ������
        mobProperty = mobStat.MobProperty();
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

    private IEnumerator MobIdleMove()//�� ���� X�� ������
    {
        while (distanceToPlayer > detectionRange)
        {
            Think();
            //������
            if (xSpeed > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (xSpeed < 0)
            {
                spriteRenderer.flipX = true;
            }
            rigid.velocity = new Vector2(xSpeed, ySpeed);
            yield return new WaitForSeconds(10f);
        }
        yield return new WaitForSeconds(0.3f);
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

    private IEnumerator PerformAttack()
    {
        IsAttack = true;
        mobAttack.Attacking(mobStat, player);//�����ϱ�
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
        // �÷��̾ ���󰡱� ���� �̵�
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
