using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP; //최대 체력
    private float currentHP; //현재 체력
    private bool isDie = false; //적의 사망 유무
    private bool isHit = false;
    private bool isStun = false;//스턴 유무

    private bool isDefDecrease = false;
    private float DefDcreasePercent = 0.3f;

    //private MobDropItem dropItem;
    private MobAI mob;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public GameObject DeadMob;
    public MobHPViewer hpViewer;

    [Header("CCEffectPrefab")]
    [SerializeField]
    private List<GameObject> freezingEffect;
    [SerializeField]
    private GameObject stunEffect;
    [SerializeField]
    private GameObject burnEffect;
    [SerializeField]
    private GameObject reducedDefenseEffect;

    //적의 체력 정보를 외부 클래스에서 확인할 수 있도록 프로퍼티 생성
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    public bool IsDie => isDie;
    public bool IsHit => isHit;
    public bool IsStun => isStun;
    private void Awake()
    {
        currentHP = maxHP; // 현재 체력을 최대 채력으로 
        mob = GetComponent<MobAI>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //dropItem = GetComponent<MobDropItem>();
    }

    /// <summary>
    /// CC가 있는 기술시 호출
    /// </summary>
    /// <param name="cc">freezing(빙결 슬로우), stun(스턴), burn(화상), reducedDefense(방깎)</param>
    /// <param name="duration">지속시간</param>
    public void TakeCC(string cc, float duration = 2f, float etc = 30f)
    {
        if (cc == "freezing")
        {
            StartCoroutine(Slow(duration));
            hpViewer.CC_HPViewer(1, duration);
        }
        if (cc == "stun")
        {
            StartCoroutine(Stun(duration));
            hpViewer.CC_HPViewer(2, duration);
        }
        if (cc == "burn")
        {
            StartCoroutine(Burn(duration, etc));
            hpViewer.CC_HPViewer(3, duration);
        }
        if (cc == "reducedDefense")
        {
            StartCoroutine(ReducedDefense(duration));
            hpViewer.CC_HPViewer(4, duration);
        }
    }

    public void TakeDamage(float damage)
    {
        //죽었으면 실행x
        if (isDie == true) return;

        if (isDefDecrease)
        {
            float damageD = damage + (damage * DefDcreasePercent);
            currentHP -= damageD;
        }
        else
        {
            //현재 체력을 damage만큼 감소;
            currentHP -= damage;
        }

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
    /// <summary>
    /// 슬로우
    /// </summary>
    /// <param name="slowDuration">슬로우 지속시간(기본3f)</param>
    /// <returns></returns>
    private IEnumerator Slow(float slowDuration = 3f)
    {
        float moveSpeed = mob.moveSpeed;
        mob.moveSpeed *= 0.6f;

        //GameObject CCEffect = Instantiate(freezingEffect, this.transform.position, Quaternion.identity) as GameObject;
        //CCEffect.transform.SetParent(this.gameObject.transform);
        StartCoroutine( SlowEffectSpawn(slowDuration));
        Color color = spriteRenderer.color;
        color.g = 0.8f;
        color.r = 0.8f;
        spriteRenderer.color = color;

        yield return new WaitForSeconds(slowDuration);
        color.g = 1.0f;
        color.r = 1.0f;
        spriteRenderer.color = color;
        mob.moveSpeed = moveSpeed;
    }
    private IEnumerator SlowEffectSpawn(float slowDuration = 3f)
    {
        for (int i = 0; i < slowDuration*2; i++)
        {
            GameObject CCEffect = Instantiate(freezingEffect[i], this.transform.position+new Vector3(0,-0.5f,5), Quaternion.identity) as GameObject;
            Destroy(CCEffect, 3f);
            /*SpriteRenderer sr = CCEffect.GetComponent<SpriteRenderer>();
            Color color = sr.color;
            color.a = 0.8f;
            color.r = 0.8f;
            sr.color = color;*/
            CCEffect.GetComponent<DecreaseAlpha>().SetUp(slowDuration);
            yield return new WaitForSeconds(0.5f);
        }
    }
    /// <summary>
    /// 스턴
    /// </summary>
    /// <param name="stunDuration">지속시간(기본1.5f)</param>
    /// <returns></returns>
    private IEnumerator Stun(float stunDuration = 1.5f)
    {
        isStun = true;
        float moveSpeed = mob.moveSpeed;
        mob.moveSpeed = 0;

        GameObject CCEffect = Instantiate(stunEffect, this.transform.position, Quaternion.identity) as GameObject;
        CCEffect.transform.SetParent(this.gameObject.transform);

        yield return new WaitForSeconds(stunDuration);
        mob.moveSpeed = moveSpeed;
        isStun = false;
        Destroy(CCEffect);
    }
    /// <summary>
    /// 화상
    /// </summary>
    /// <param name="burnDuration">화상 지속시간 (기본3f)</param>
    /// <param name="burnDamage">화상 데미지 (기본30f)</param>
    /// <returns></returns>
    private IEnumerator Burn(float burnDuration = 3f, float burnDamage = 30f)
    {
        float dotTime = burnDuration * 2;
        float dotDamage = burnDamage / dotTime;

        GameObject CCEffect = Instantiate(burnEffect, this.transform.position, Quaternion.identity) as GameObject;
        CCEffect.transform.SetParent(this.gameObject.transform);

        for (int i = 0; i < dotTime; i++)
        {
            TakeDamage(dotDamage);
            yield return new WaitForSeconds(0.5f);
        }
        Destroy(CCEffect);
    }
    /// <summary>
    /// 방깎
    /// </summary>
    /// <param name="reducedDefenseDuration">방깎 지속시간(기본3f)</param>
    /// <param name="ReduedDefensePercent">방깎 퍼센트(기본0.3f>>받는 데미지30%증가)</param>
    /// <returns></returns>
    private IEnumerator ReducedDefense(float reducedDefenseDuration = 3f, float ReduedDefensePercent = 0.3f)
    {
        isDefDecrease = true;
        DefDcreasePercent = ReduedDefensePercent;

        GameObject CCEffect = Instantiate(reducedDefenseEffect, this.transform.position, Quaternion.identity) as GameObject;
        CCEffect.transform.SetParent(this.gameObject.transform);

        yield return new WaitForSeconds(reducedDefenseDuration);
        isDefDecrease = false;
        Destroy(CCEffect);
    }
    private IEnumerator Die()
    {
        // 몬스터가 죽을 때의 처리
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
        Instantiate(DeadMob, transform.position, Quaternion.identity); // 시체 생성
        //dropItem.ItemDrop();//아이템 드롭

    }
    private IEnumerator HitAlphaAnimation()
    {

        //현재 적의 색상을 color변수에 저장
        Color color = spriteRenderer.color;

        //적 투명도 4할
        color.a = 0.4f;
        spriteRenderer.color = color;
        //0.05ch eorl
        yield return new WaitForSeconds(0.05f);

        //적의 투명도 10할
        color.a = 1.0f;
        spriteRenderer.color = color;

        isHit = true;
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(0.6f);
        isHit = false;
    }
}
