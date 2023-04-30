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

    public float detectionRange=10;
    public float mobAttackRange=2;
    public float moveSpeed=1;

    private bool isPlayerInRange = false;
    public int idleSpeed = 1;
    private int xSpeed = 0;
    private int ySpeed = 0;
    bool flipFlag = false;
    public bool IsAttack = false;
    string mobProperty;
    bool attackChanger = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mobStat = GetComponent<MobStat>();
        mobHP = GetComponent<MobHP>();
        //���� ������ ������
        mobProperty = mobStat.MobProperty();
        detectionRange = mobStat.DetectingRange();
        mobAttackRange = mobStat.MobAttackRange();
        moveSpeed = mobStat.MoveSpeed();
    }
    private void Start()
    {
        // player�� ã�Ƽ� �����մϴ�.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(MobIdleMove());
    }

    private void FixedUpdate()
    {
        // ���� ��ü�� �÷��̾� ������ �Ÿ� ���
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            isPlayerInRange = true; // ���� ���� ���� �ִٸ� isPlayerInRange ������ true�� ����
        }
        else
        {
            isPlayerInRange = false; // ���� ���� �ۿ� �ִٸ� isPlayerInRange ������ false�� ����
        }
        AI(distanceToPlayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�������̸�
        if (mobProperty == "melee")
        {
            if (IsAttack == true && collision.gameObject.CompareTag("Player"))
            {
                //�÷��̾��� HP�� ������ ���ݷ¸�ŭ ����
                collision.gameObject.GetComponent<Player>().P_TakeDamage(mobStat.mobDamage);
            }
        }
    }
    /*
    // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǵ� �Լ�
    public void Attack()
    {
        // �÷��̾�� �������� ����
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, mobAttackRange, LayerMask.GetMask("Player"));
        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<Player>().P_TakeDamage(mobStat.mobDamage);
        }
    }*/
    private IEnumerator MobIdleMove()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
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
            return;//����� or Hit�� ����X
        }
        // player�� ���� �Ÿ� �ȿ� �ְ� MobAttackRange ���� �ȿ� ������ MobAttack��ũ��Ʈ�� ȣ���մϴ�.
        if (isPlayerInRange)
        {
            if (distanceToPlayer < mobAttackRange)
            {
                if (mobProperty == "melee")//������ ����
                {
                    //attack1 �ѹ� attack2 �ѹ� �����ư��鼭 ����
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
                else if (mobProperty == "range")
                {

                }
            }
            else
            {
                //mobAttack.Attack(mobProperty);
                animator.SetInteger("WalkSpeed", 1);
                IsAttack = false;
                // �÷��̾ ���󰡱� ���� �̵�
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
}
