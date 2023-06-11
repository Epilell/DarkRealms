using System.Collections;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    public Player player;
    public Transform escape; // �ⱸ
    //public GameObject filter;

    private float initialRadius = 100; // �ʱ� �ڱ��� ������
    public float decreaseSpeed = 1; // �ڱ��� ���� �ӵ�
    private float currentRadius; // ���� �ڱ��� ������

    public float damage = 5; // ���ط�
    private float damageTimer; // Ÿ�̸�

    private bool isPlayerInsideField; // �÷��̾ �ڱ��� ���ο� �ִ��� ����

    private void Start()
    {
        //filter.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        currentRadius = initialRadius; // ������ �ʱ�ȭ
        escape = GameObject.FindWithTag("Escape").transform;
        transform.position = escape.position; // �ڱ��� �߽� ��ġ�� �ⱸ ��ġ�� ����
        damageTimer = 1f; // �ʱ� ���� ������ Ÿ�̸� ����
/*
        FindObjectOfType<MagneticField>().escape = Instantiate(route[routenum]).transform;
        FindObjectOfType<MagneticField>().transform.position = FindObjectOfType<MagneticField>().escape.position;*/
        isPlayerInsideField = true;

        StartCoroutine(DecreaseMagneticField());
    }

    private IEnumerator DecreaseMagneticField()
    {
        yield return new WaitForSeconds(300f); // 5�� ���

        while (currentRadius > 0f)
        {
            currentRadius -= decreaseSpeed * Time.deltaTime / 6f; // �ڱ��� ũ�� ����: 3f�� 5�� 6f�� 10��

            if (currentRadius <= 0f)
            {
                currentRadius = 0f; // ������ 0���Ϸ� �������� �ʰ� ��
                isPlayerInsideField = false; // �������� 0�� �Ǹ� �÷��̾�� �ڱ��� ���ο� ���ٰ� �Ǵ�
            }
            else { isPlayerInsideField = Vector3.Distance(transform.position, player.transform.position) <= transform.localScale.x * 2.5; } // �÷��̾ �ڱ��� ���ο� �ִ��� Ȯ��

            transform.localScale = new Vector3(currentRadius, currentRadius, 1f); // �ڱ��� ũ�� ����: Scale���� ����

            yield return null;
        }
    }

    private void Update()
    {
        if (!isPlayerInsideField) // �÷��̾ �ڱ��� ���̸�
        {
            //filter.SetActive(true);

            damageTimer -= Time.deltaTime; // Ÿ�̸� ����

            if (damageTimer <= 0f) // 1�� ������
            {
                player.P_TakeDamage(damage); // �÷��̾�� ���ظ� ����
                damageTimer = 1f; // Ÿ�̸� �缳��
            }
        }
        else { 
            //filter.SetActive(false); 
        }
    }
}