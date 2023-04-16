using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobHP : MonoBehaviour
{
    public int maxHealth = 100; // 몬스터의 최대 체력
    public int currentHealth; // 몬스터의 현재 체력

    public Slider healthSlider; // 체력바 슬라이더
    public Gradient gradient; // 체력바 그라데이션
    public Image fill; // 체력바 채움 이미지

    private void Start()
    {
        currentHealth = maxHealth; // 몬스터의 체력 초기화
        healthSlider.maxValue = maxHealth; // 체력바 슬라이더 최대값 설정
        healthSlider.value = currentHealth; // 체력바 슬라이더 초기화
        fill.color = gradient.Evaluate(1f); // 체력바 그라데이션 초기화
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // 몬스터의 체력 감소

        healthSlider.value = currentHealth; // 체력바 갱신
        fill.color = gradient.Evaluate(healthSlider.normalizedValue); // 체력바 그라데이션 변경

        if (currentHealth <= 0)
        {
            Die(); // 몬스터가 죽음
        }
    }

    private void Die()
    {
        // 몬스터가 죽을 때의 처리
        Destroy(gameObject);
    }
}
