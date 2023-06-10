using UnityEngine;

public class MagneticField : MonoBehaviour
{
    public Player player;
    public Transform escape; // �ⱸ
    public GameObject filter;

    public float initialRadius = 600; // �ʱ� �ڱ��� ������: ���߿� private�� �ٲ� ����
    public float decreaseSpeed = 1; // �ڱ��� ���� �ӵ�
    private float currentRadius; // ���� �ڱ��� ������

    public float damage = 5; // ���ط�
    private float damageTimer; // Ÿ�̸�

    private bool isPlayerInsideField; // �÷��̾ �ڱ��� ���ο� �ִ��� ����

    private void Start()
    {
        filter.SetActive(false);
        currentRadius = initialRadius; // ������ �ʱ�ȭ
        escape = GameObject.FindWithTag("Escape").transform;
        transform.position = escape.position; // �ڱ��� �߽� ��ġ�� �ⱸ ��ġ�� ����
        damageTimer = 1f; // �ʱ� ���� ������ Ÿ�̸� ����
    }

    private void Update()
    {
        currentRadius -= decreaseSpeed * Time.deltaTime; // �ڱ��� ũ�� ����

        if (currentRadius <= 0f)
        {
            currentRadius = 0f; // ������ 0���Ϸ� �������� �ʰ� ��
            isPlayerInsideField = false; // �������� 0�� �Ǹ� �÷��̾�� �ڱ��� ���ο� ���ٰ� �Ǵ�
        }
        else { isPlayerInsideField = Vector3.Distance(transform.position, player.transform.position) <= transform.localScale.x * 2.5; } // �÷��̾ �ڱ��� ���ο� �ִ��� Ȯ��

        transform.localScale = new Vector3(currentRadius, currentRadius, 1f); // �ڱ��� ũ�� ����: Scale���� ����

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
        else { filter.SetActive(false); }
    }
}