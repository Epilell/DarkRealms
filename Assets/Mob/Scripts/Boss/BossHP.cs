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
    public BossStat Stat;
    public bool CanDamage=true;
    public GameObject escapePortal;

    private GameObject mainCamera;
    private Camera camera;
    private Transform cameraTransform;

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
        maxHP = Stat.BossHP;
    }

    public void TakeDamage(float damage)
    {
        if(CanDamage == false)
        {
            return;
        }
        //�׾����� ����x
        if (isDie == true) return;

        //���� ü���� damage��ŭ ����;
        currentHP -= damage;

        //HitAlphaAnimation -> ���� ���� ��ȭ
        //StopCoroutine("HitAlphaAnimation");
        //StartCoroutine("HitAlphaAnimation");

        //ü�� 0���Ͻ� ���
        if (currentHP <= 0)
        {
            isDie = true;
            //�� ���
            StartCoroutine(Die());
        }
    }
    private IEnumerator Die()
    {
        // ���Ͱ� ���� ���� ó��
        animator.SetTrigger("IsDead");
        gameObject.transform.position += new Vector3(0, -3.5f, 0);

        StartCoroutine(returnCamera());
        yield return new WaitForSeconds(6.0f);
        Instantiate(escapePortal, transform.position+ new Vector3(0,0,-2), Quaternion.identity);
        Destroy(gameObject);
        Instantiate(DeadMob, transform.position, Quaternion.identity); // ��ü ����
        //dropItem.ItemDrop();//������ ���

    }
    private IEnumerator returnCamera()
    {
        mainCamera = GameObject.Find("Main Camera");
        camera = mainCamera.GetComponent<Camera>();
        cameraTransform = mainCamera.transform;

        for (int i = 100; i >= 50; i--)
        {
            camera.orthographicSize = i / 10;

            cameraTransform.localPosition = new Vector3(0, (i / 10) - 5, -10f);
            yield return new WaitForSeconds(0.02f);
        }
    }
    private IEnumerator HitAlphaAnimation()
    {
        //���� ���� ������ color������ ����
        Color color = spriteRenderer.color;

        //�� ���� 8��
        color.a = 0.8f;
        spriteRenderer.color = color;
        //0.05ch eorl
        yield return new WaitForSeconds(0.05f);

        //���� ���� 10��
        color.a = 1.0f;
        spriteRenderer.color = color;
    }
}
