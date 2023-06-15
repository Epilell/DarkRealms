using System.Collections;
using UnityEngine;

public class UndyingEffect : MonoBehaviour
{
    public Player player; // �÷��̾�

    private float maxHealth; // �ִ� ü��
    private float currentHealth; // ���� ü��
    private bool undyingActive; // �һ� ȿ�� Ȱ��ȭ ����

    private void Start() { player = GetComponent<PotionEffect>().player; }

    public void Undying(float duration) // �һ� ȿ�� ����
    {
        if (!undyingActive)
        {
            undyingActive = true;

            maxHealth = player.MaxHP;
            currentHealth = player.CurrentHp;

            StartCoroutine(EndUndyingCoroutine(duration)); // ������ ���ӽð���ŭ ȿ�� �ο�
        }
    }

    // ���� ü���� �ִ� ü���� 20% �̸��� ��� ü�� �������� ���� �� ���� �̻��ؼ� ���߿� ����
    private void Update() { if (undyingActive && currentHealth < maxHealth * 0.2f) { player.CurrentHp = currentHealth; } }

    private IEnumerator EndUndyingCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration); // ������ �ð���ŭ ���

        undyingActive = false; // ȿ�� ����
    }
}