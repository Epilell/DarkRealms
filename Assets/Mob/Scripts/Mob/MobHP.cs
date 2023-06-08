using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP; //�ִ� ü��
    private float currentHP; //���� ü��
    private bool isDie = false; //���� ��� ����
    private bool isHit = false;
    //private MobDropItem dropItem;
    private MobAI mob;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public GameObject DeadMob;


    //���� ü�� ������ �ܺ� Ŭ�������� Ȯ���� �� �ֵ��� ������Ƽ ����
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    public bool IsDie => isDie;
    public bool IsHit => isHit;
    private void Awake()
    {
        currentHP = maxHP; // ���� ü���� �ִ� ä������ 
        mob = GetComponent<MobAI>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //dropItem = GetComponent<MobDropItem>();
    }

    public void TakeDamage(float damage)
    {
        //�׾����� ����x
        if (isDie == true) return;

        //���� ü���� damage��ŭ ����;
        currentHP -= damage;

        //HitAlphaAnimation -> ���� ���� ��ȭ
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        //ü�� 0���Ͻ� ���
        if (currentHP <= 0)
        {
            isDie = true;
            //�� ���
            StartCoroutine("Die");
        }
    }
    private IEnumerator Die()
    {
        // ���Ͱ� ���� ���� ó��
        animator.SetBool("IsDead",true);
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
        Instantiate(DeadMob, transform.position, Quaternion.identity); // ��ü ����
        //dropItem.ItemDrop();//������ ���

    }
    private IEnumerator HitAlphaAnimation()
    {
        
        //���� ���� ������ color������ ����
        Color color = spriteRenderer.color;

        //�� ���� 4��
        color.a = 0.4f;
        spriteRenderer.color = color;
        //0.05ch eorl
        yield return new WaitForSeconds(0.05f);

        //���� ���� 10��
        color.a = 1.0f;
        spriteRenderer.color = color;
        
        isHit = true;
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(0.6f);
        isHit = false;
    }
}
