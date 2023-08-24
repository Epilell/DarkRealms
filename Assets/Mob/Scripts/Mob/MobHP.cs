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
    private bool isStun = false;//���� ����

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

    //���� ü�� ������ �ܺ� Ŭ�������� Ȯ���� �� �ֵ��� ������Ƽ ����
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    public bool IsDie => isDie;
    public bool IsHit => isHit;
    public bool IsStun => isStun;
    private void Awake()
    {
        currentHP = maxHP; // ���� ü���� �ִ� ä������ 
        mob = GetComponent<MobAI>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //dropItem = GetComponent<MobDropItem>();
    }

    /// <summary>
    /// CC�� �ִ� ����� ȣ��
    /// </summary>
    /// <param name="cc">freezing(���� ���ο�), stun(����), burn(ȭ��), reducedDefense(���)</param>
    /// <param name="duration">���ӽð�</param>
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
        //�׾����� ����x
        if (isDie == true) return;

        if (isDefDecrease)
        {
            float damageD = damage + (damage * DefDcreasePercent);
            currentHP -= damageD;
        }
        else
        {
            //���� ü���� damage��ŭ ����;
            currentHP -= damage;
        }

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
    /// <summary>
    /// ���ο�
    /// </summary>
    /// <param name="slowDuration">���ο� ���ӽð�(�⺻3f)</param>
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
    /// ����
    /// </summary>
    /// <param name="stunDuration">���ӽð�(�⺻1.5f)</param>
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
    /// ȭ��
    /// </summary>
    /// <param name="burnDuration">ȭ�� ���ӽð� (�⺻3f)</param>
    /// <param name="burnDamage">ȭ�� ������ (�⺻30f)</param>
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
    /// ���
    /// </summary>
    /// <param name="reducedDefenseDuration">��� ���ӽð�(�⺻3f)</param>
    /// <param name="ReduedDefensePercent">��� �ۼ�Ʈ(�⺻0.3f>>�޴� ������30%����)</param>
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
        // ���Ͱ� ���� ���� ó��
        animator.SetBool("IsDead", true);
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
