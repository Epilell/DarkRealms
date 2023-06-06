using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    public SkillManager skillManager;

    [Header("SkillIcon")] // 스킬 아이콘
    [SerializeField] private Image iconQ;
    [SerializeField] private Image iconE;
    [SerializeField] private Image iconSpace;
    [SerializeField] private Image iconMouseRight;

    [Header("SkillImage")] // 스킬 이미지 스프라이트
    [SerializeField] private Sprite onSpriteQ;
    [SerializeField] private Sprite offSpriteQ;
    [SerializeField] private Sprite onSpriteE;
    [SerializeField] private Sprite offSpriteE;
    [SerializeField] private Sprite onSpriteSpace;
    [SerializeField] private Sprite offSpriteSpace;
    [SerializeField] private Sprite onSpriteMouseRight;
    [SerializeField] private Sprite offSpriteMouseRight;

    [Header("CoolDownImage")] // 쿨다운 이미지
    [SerializeField] private Image cooldownImageQ;
    [SerializeField] private Image cooldownImageE;
    [SerializeField] private Image cooldownImageSpace;
    [SerializeField] private Image cooldownImageMouseRight;

    private float cooldownTimeQ, cooldownTimeE, cooldownTimeSpace, cooldownTimeMouseRight; // 각 스킬의 쿨다운 시간
    private float currentTimeQ, currentTimeE, currentTimeSpace, currentTimeMouseRight; // 각각의 남은 쿨다운 시간
    private bool isCooldownQ, isCooldownE, isCooldownSpace, isCooldownMouseRight; // 각각의 쿨다운 상태

    void Start()
    {
        // 초기 쿨타임 초기화
        cooldownTimeQ = skillManager.molotovdata.CoolTime;
        cooldownTimeE = skillManager.siegemodedata.CoolTime;
        cooldownTimeSpace = skillManager.dodgedata.CoolTime;
        cooldownTimeMouseRight = skillManager.evdshotdata.CoolTime;

        // 투명하게 초기화
        cooldownImageQ.fillAmount = 0;
        cooldownImageE.fillAmount = 0;
        cooldownImageSpace.fillAmount = 0;
        cooldownImageMouseRight.fillAmount = 0;
    }

    void Update()
    {
        // 각 스킬 사용 UI
        if (Input.GetKeyDown(KeyCode.Q)) { UseSkill(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ, iconQ, offSpriteQ); }
        if (Input.GetKeyDown(KeyCode.E)) { UseSkill(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE, iconE, offSpriteE); }
        if (Input.GetKeyDown(KeyCode.Space)) { UseSkill(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace, iconSpace, offSpriteSpace); }
        if (Input.GetKeyDown(KeyCode.Mouse1)) { UseSkill(ref isCooldownMouseRight, ref currentTimeMouseRight, cooldownImageMouseRight, cooldownTimeMouseRight, iconMouseRight, offSpriteMouseRight); }

        // 각 쿨다운 업데이트
        UpdateCooldown(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ, iconQ, onSpriteQ);
        UpdateCooldown(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE, iconE, onSpriteE);
        UpdateCooldown(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace, iconSpace, onSpriteSpace);
        UpdateCooldown(ref isCooldownMouseRight, ref currentTimeMouseRight, cooldownImageMouseRight, cooldownTimeMouseRight, iconMouseRight, onSpriteMouseRight);
    }

    private void UseSkill(ref bool isCooldown, ref float currentTime, Image cooldownImage, float cooldownTime, Image skillIcon, Sprite offSprite) // 스킬 사용 및 쿨다운 시작
    {
        if (!isCooldown)
        {
            isCooldown = true;
            currentTime = cooldownTime;
            skillIcon.sprite = offSprite;
        }
    }

    private void UpdateCooldown(ref bool isCooldown, ref float currentTime, Image cooldownImage, float cooldownTime, Image skillIcon, Sprite onSprite) // 쿨다운 상태 업데이트 및 이미지 상태 업데이트
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
                skillIcon.sprite = onSprite;
            }
        }
    }
}