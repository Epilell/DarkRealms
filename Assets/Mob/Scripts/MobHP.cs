using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP; //최대 체력
    private float currentHP; //현재 체력
    private bool isDie = false; //적의 사망 유무
    private MobAI mob;
    private SpriteRenderer spriteRenderer;
    public Animator animator;


    //적의 체력 정보를 외부 클래스에서 확인할 수 있도록 프로퍼티 생성
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    private void Awake()
    {
        currentHP = maxHP; // 현재 체력을 최대 채력으로 
        mob = GetComponent<MobAI>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        //죽었으면 실행x
        if (isDie == true) return;

        //현재 체력을 damage만큼 감소;
        currentHP -= damage;

        //HitAlphaAnimation -> 적의 투명도 변화
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        //체력 0이하시 사망
        if (currentHP <= 0)
        {
            isDie = true;
            //적 사망
            StartCoroutine("Die");
        }
    }
    private IEnumerator Die()
    {
        // 몬스터가 죽을 때의 처리
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        
    }
    private IEnumerator HitAlphaAnimation()
    {
        //현재 적의 색상을 color변수에 저장
        Color color = spriteRenderer.color;

        //적 투명도 4할
        //color.a = 0.4f;
        //spriteRenderer.color = color;
        //0.05ch eorl
        //yield return new WaitForSeconds(0.05f);

        //적의 투명도 10할
        //color.a = 1.0f;
        //spriteRenderer.color = color;

        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(0.05f);
    }
}
