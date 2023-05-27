using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndyingEffect : MonoBehaviour
{
    public Player player; // �÷��̾�

    private float maxHealth; // �ִ� ü��
    private float currentHealth; // ���� ü��
    private bool undyingActive; // �һ� ȿ�� Ȱ��ȭ ����

    public void Undying(float duration) // �һ� ȿ�� ����
    {
        if (!undyingActive)
        {
            undyingActive = true;

            maxHealth = player.MaxHP;
            currentHealth = player.CurrentHp;

            Invoke(nameof(EndUndying), duration); // ������ �ð���ŭ ȿ�� �ο�
        }
    }

    // ���� ü���� �ִ� ü���� 20% �̸��� ��� ü�� �������� ����
    private void Update() { if (undyingActive && currentHealth < maxHealth * 0.2f) { player.CurrentHp = currentHealth; } }

    private void EndUndying() // ȿ�� ����
    {
        undyingActive = false;
        // player.CurrentHp = maxHealth * 0.35f; // ü�� ȸ��
    }
}