using UnityEngine;

public class MagneticField : MonoBehaviour
{
    public Player player;
    public Transform escape; // �ⱸ

    public float initialRadius; // �ʱ� �ڱ��� ������
    public float decreaseSpeed; // �ڱ��� ���� �ӵ�
    private float currentRadius; // ���� �ڱ��� ������

    public float damage = 1; // ���ط�
    private float damageTimer; // Ÿ�̸�
    private bool isPlayerInsideField; // �÷��̾ �ڱ��� ���ο� �ִ��� ����

    private void Start()
    {
        currentRadius = initialRadius; // ������ �ʱ�ȭ
        transform.position = escape.position; // �ڱ��� �߽� ��ġ�� �ⱸ ��ġ�� ����
        damageTimer = 1f; // �ʱ� ���� ������ Ÿ�̸� ����
    }

    private void Update()
    {
        if (decreaseSpeed < 1) { decreaseSpeed = 1; }

        // Debug.Log("���� ���� �ӵ�: " + decreaseSpeed);
        currentRadius -= decreaseSpeed * Time.deltaTime; // �ڱ��� ũ�� ����

        if (currentRadius <= 0f)
        {
            currentRadius = 0f; // ������ 0���Ϸ� �������� �ʰ� ��
            isPlayerInsideField = false; // �������� 0�� �Ǹ� �÷��̾�� �ڱ��� ���ο� ���ٰ� �Ǵ�
        }
        else { isPlayerInsideField = Vector3.Distance(transform.position, player.transform.position) <= currentRadius; } // �÷��̾ �ڱ��� ���ο� �ִ��� Ȯ��

        transform.localScale = new Vector3(currentRadius * 2, currentRadius * 2, 1f); // �ڱ��� ũ�� ����: Scale���� �����̹Ƿ� 2�� ������

        if (!isPlayerInsideField) // �÷��̾ �ڱ��� ���̸�
        {
            damageTimer -= Time.deltaTime; // Ÿ�̸� ����

            if (damageTimer <= 0f) // 1�� ������
            {
                player.P_TakeDamage(damage); // �÷��̾�� ���ظ� ����
                damageTimer = 1f; // Ÿ�̸� �缳��
            }
        }
        /*Debug.Log("�� ������: " + currentRadius);
        Debug.Log("�ڱ��� �߽ɰ��� �Ÿ�: " + Vector3.Distance(transform.position, player.transform.position));*/
    }
}