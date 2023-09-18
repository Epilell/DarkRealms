using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public float interactionDistance = 2.0f; // �÷��̾�� ��ȣ�ۿ� ������ �Ÿ�
    private Animator animator; // �ִϸ����� ������Ʈ
    [Header("���� ����Ʈ")]
    public GameObject ArrowPoint;//��ǥ�� ���ϴ� ȭ��ǥ
    [Header("SpriteRenderer�� ȭ��ǥ")]
    public GameObject arrow;
    private SpriteRenderer srA;
    [Header("�ȳ�â")]
    [SerializeField]
    private GameObject pressF;
    private bool once = true;

    private bool isOn = false;
    private GameObject escapeTarget;//Ż�ⱸ ��Ż ���ӿ�����Ʈ
    private GameObject bossTarget;//������ ��Ż ���ӿ�����Ʈ
    private Vector3 direction;//target��ǥ
    public bool isEscape;

    private GameObject player; // �÷��̾� ������Ʈ

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        escapeTarget = GameObject.FindGameObjectWithTag("Escape");
        bossTarget = GameObject.FindGameObjectWithTag("BossPortal");

        if (isEscape)
        {
            direction = escapeTarget.transform.position - transform.position;
        }
        else
        {
            direction = bossTarget.transform.position - transform.position;
        }
        //ȭ��ǥ ���⼳��
        srA = arrow.GetComponent<SpriteRenderer>();
        StartCoroutine(colorChanger());
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        ArrowPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }
    private RectTransform rectTransform;
    private Vector3 distance = Vector3.down * 2000.0f;
    private void Update()
    {
        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= interactionDistance && once)
        {
            pressF.SetActive(true);
            once = false;
        }
        // F Ű�� ������ �ִϸ��̼� ����
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (distanceToPlayer <= interactionDistance && isOn == false) //&& Input.GetKeyDown(KeyCode.F))
            {
                animator.SetBool("On", true); // "Interact"��� Ʈ���� �̸� ��� 
                StartCoroutine(colorChanger(1f));
                pressF.SetActive(false);
                isOn = true;
            }
        }

        if (distanceToPlayer > interactionDistance && isOn == true)
        {
            animator.SetBool("On", false);

            pressF.SetActive(false);
            StartCoroutine(colorChanger());
            isOn = false;
            once = true;
        }
    }
    private IEnumerator colorChanger(float alpha = 0)
    {
        if (alpha == 0)
        {
            yield return new WaitForSeconds(0.01f);
            Color c = srA.color;
            c.a = alpha;
            srA.color = c;
        }
        else
        {
            yield return new WaitForSeconds(1.8f);
            Color c = srA.color;
            c.a = alpha;
            srA.color = c;
        }

    }
}
