using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP; //�ִ� ü��
    private float currentHP; //���� ü��
    private bool isDie = false; //���� ��� ����
    //private MobDropItem dropItem;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public GameObject DeadMob;
    private BossStat Stat;


    //���� ü�� ������ �ܺ� Ŭ�������� Ȯ���� �� �ֵ��� ������Ƽ ����
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    public bool IsDie => isDie;
    private void Awake()
    {
        currentHP = maxHP; // ���� ü���� �ִ� ä������ 
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //dropItem = GetComponent<MobDropItem>();
        Stat = GetComponent<BossStat>();
        maxHP = Stat.BossHP;
    }

    public void TakeDamage(float damage)
    {
        //�׾����� ����x
        if (isDie == true) return;

        //���� ü���� damage��ŭ ����;
        currentHP -= damage;

        //HitAlphaAnimation -> ���� ������ ��ȭ
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
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(1.1f);
        Destroy(gameObject);
        Instantiate(DeadMob, transform.position, Quaternion.identity); // ��ü ����
        //dropItem.ItemDrop();//������ ���

    }
    private IEnumerator HitAlphaAnimation()
    {
        //���� ���� ������ color������ ����
        Color color = spriteRenderer.color;

        //�� ������ 8��
        color.a = 0.8f;
        spriteRenderer.color = color;
        //0.05ch eorl
        yield return new WaitForSeconds(0.05f);

        //���� ������ 10��
        color.a = 1.0f;
        spriteRenderer.color = color;
    }
}