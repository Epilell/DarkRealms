using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobHP : MonoBehaviour
{
    public int maxHealth = 100; // ������ �ִ� ü��
    public int currentHealth; // ������ ���� ü��

    public Slider healthSlider; // ü�¹� �����̴�
    public Gradient gradient; // ü�¹� �׶��̼�
    public Image fill; // ü�¹� ä�� �̹���

    private void Start()
    {
        currentHealth = maxHealth; // ������ ü�� �ʱ�ȭ
        healthSlider.maxValue = maxHealth; // ü�¹� �����̴� �ִ밪 ����
        healthSlider.value = currentHealth; // ü�¹� �����̴� �ʱ�ȭ
        fill.color = gradient.Evaluate(1f); // ü�¹� �׶��̼� �ʱ�ȭ
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ������ ü�� ����

        healthSlider.value = currentHealth; // ü�¹� ����
        fill.color = gradient.Evaluate(healthSlider.normalizedValue); // ü�¹� �׶��̼� ����

        if (currentHealth <= 0)
        {
            Die(); // ���Ͱ� ����
        }
    }

    private void Die()
    {
        // ���Ͱ� ���� ���� ó��
        Destroy(gameObject);
    }
}
