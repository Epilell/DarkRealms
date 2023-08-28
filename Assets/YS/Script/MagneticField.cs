using System.Collections;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    public Player player;
    public Transform escape; // �ⱸ
    public GameObject filter;

    public float damage = 5; // ���ط�
    public float decreaseSpeed = 1; // �ڱ��� ���� �ӵ�

    private float initialRadius = 100; // �ʱ� �ڱ��� ������
    private float currentRadius; // ���� �ڱ��� ������
    private float damageTimer; // Ÿ�̸�

    private bool isPlayerInsideField; // �÷��̾ �ڱ��� ���ο� �ִ��� ����

    private void Start()
    {
        isPlayerInsideField = true;

        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        filter = GameObject.FindWithTag("Filter");
        filter.SetActive(false);

        currentRadius = initialRadius; // ������ �ʱ�ȭ

        damageTimer = 1f; // �ʱ� ���� ������ Ÿ�̸� ����

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
            filter.SetActive(true);

            damageTimer -= Time.deltaTime; // Ÿ�̸� ����

            if (damageTimer <= 0f) // 1�� ������
            {
                player.P_TakeDamage(damage); // �÷��̾�� ���ظ� ����
                damageTimer = 1f; // Ÿ�̸� �缳��
            }
        }
        else filter.SetActive(false);
    }
}