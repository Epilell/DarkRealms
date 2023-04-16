using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    Rigidbody2D rigid;
    public Transform player;
    public float detectionRange = 10f;
    public float mobAttackRange = 1f;
    public float moveSpeed = 1f;
    public Animator animator;
    SpriteRenderer spriteRenderer;
    private bool isPlayerInRange = false;

    public int xSpeed = 0;
    public int ySpeed = 0;
    public int MaxSpeed = 1;
    public bool IsAttack = false;
    private MobAttack mobAttack;

    public string mobProperty = "melee";

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            IsAttack = true;
            animator.SetTrigger("Attack");

            //mobAttack.Attack();
        }
        else if (isPlayerInRange)
        {
            animator.SetInteger("WalkSpeed", 1);
            IsAttack = false;
            // �÷��̾ ���󰡱� ���� �̵�
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            if (player.position.x < rigid.position.x)
            {
                spriteRenderer.flipX = true;
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
            if (IsAttack == true && collision.gameObject.CompareTag("player"))
            {
                //�÷��̾��� HP�� ������ ���ݷ¸�ŭ ����
                //player.HP-= monsterStat.MobDamage;
            }
        }
        else
        {

        }
    }
    private IEnumerator MobIdleMove()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        while (distanceToPlayer > detectionRange)
        {
            rigid.velocity = new Vector2(xSpeed, ySpeed);
            Think();
            yield return new WaitForSeconds(3f);
        }
    }
    void Think()
    {
        xSpeed = Random.Range(-MaxSpeed, MaxSpeed + 1);
        ySpeed = Random.Range(-MaxSpeed, MaxSpeed + 1);
        int speed = 1;
        if (ySpeed == 0 && xSpeed == 0)
        {
            speed = 0;
        }
        animator.SetInteger("WalkSpeed", speed);
        //������
        if (speed != 0 && xSpeed > 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
