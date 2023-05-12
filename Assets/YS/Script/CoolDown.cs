using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    [SerializeField] private float cooldownTimeQ, cooldownTimeE, cooldownTimeSpace; // 각 스킬의 쿨다운 시간
    [SerializeField] private Image cooldownImageQ, cooldownImageE, cooldownImageSpace; // 각 스킬의 쿨다운 이미지

    private float currentTimeQ, currentTimeE, currentTimeSpace; // 각각의 남은 쿨다운 시간
    private bool isCooldownQ, isCooldownE, isCooldownSpace; // 각각의 쿨다운 상태

    void Start()
    {
        // 처음에는 투명하게 초기화
        cooldownImageQ.fillAmount = 0;
        cooldownImageE.fillAmount = 0;
        cooldownImageSpace.fillAmount = 0;
    }

    void Update()
    {
        // Q, E, Space 키 별로 스킬 사용
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSkill(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSkill(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace);
        }

        // 각 쿨다운 업데이트
        UpdateCooldown(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ);
        UpdateCooldown(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE);
        UpdateCooldown(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace);
    }

    private void UseSkill(ref bool isCooldown, ref float currentTime, Image cooldownImage, float cooldownTime) // 스킬 사용 및 쿨다운 시작
    {
        if (!isCooldown)
        {
            isCooldown = true;
            currentTime = cooldownTime;
            // 스킬 호출 부분
        }
    }

    private void UpdateCooldown(ref bool isCooldown, ref float currentTime, Image cooldownImage, float cooldownTime) // 쿨다운 상태 업데이트 및 이미지 상태 업데이트
    {
        if (isCooldown)
        {
            currentTime -= Time.deltaTime; // 쿨다운 시간 감소
            cooldownImage.fillAmount = currentTime / cooldownTime; // 쿨다운 이미지 투명도 변경

            if (currentTime <= 0) // 쿨다운이 끝난 경우 초기화
            {
                currentTime = 0;
                isCooldown = false;
                cooldownImage.fillAmount = 0;
            }
        }
    }
}