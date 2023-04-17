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
        //���� ������ ������
        mobProperty = mobStat.MobProperty();
        detectionRange = mobStat.DetectingRange();
        mobAttackRange = mobStat.MobAttackRange();
        moveSpeed = mobStat.MoveSpeed();
        attack1or2 = false;
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

        // player�� ���� �Ÿ� �ȿ� �ְ� MobAttackRange ���� �ȿ� ������ MobAttack��ũ��Ʈ�� ȣ���մϴ�.
        if (isPlayerInRange && distanceToPlayer < mobAttackRange)
        {
            //attack1 �ѹ� attack2 �ѹ� �����ư��鼭 ����
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
        else
        {
            IsAttack = false;
        }
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
}
