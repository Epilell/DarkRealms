using System.Collections;
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

    [Header("CoolDownBar")] // 쿨다운 바
    [SerializeField] private Slider siegeBar; // 시즈모드 지속시간 표시

    private float cooldownTimeQ, cooldownTimeE, cooldownTimeSpace, cooldownTimeMouseRight; // 각 스킬의 쿨다운 시간
    private float currentTimeQ, currentTimeE, currentTimeSpace, currentTimeMouseRight; // 각각의 남은 쿨다운 시간
    private bool isCooldownQ, isCooldownE, isCooldownSpace, isCooldownMouseRight; // 각각의 쿨다운 상태

    public bool isSkillUse; // 전체 쿨타임 초기화 전용
    public bool molotovActive, siegeActive, dodgeActive, evdshotActive; // 스킬 사용 여부 체크용
    public bool siegeCoolDown = false; // 처음부터 쿨다운 되지 않게 하기 위한 변수

    void Start()
    {
        // 초기 쿨타임 초기화
        cooldownTimeQ = skillManager.molotovData.CoolTime; // 10초
        cooldownTimeE = skillManager.siegemodeData.CoolTime; // 20초
        cooldownTimeSpace = skillManager.dodgeData.CoolTime; // 3초
        cooldownTimeMouseRight = skillManager.evdshotData.CoolTime; // 10초

        // 투명하게 초기화
        cooldownImageQ.fillAmount = 0;
        cooldownImageE.fillAmount = 0;
        cooldownImageSpace.fillAmount = 0;
        cooldownImageMouseRight.fillAmount = 0;
    }

    void Update()
    {
        if (skillManager.isSkillCanUse) // 인벤토리, 설정창 중 하나라도 켜져있으면 쿨다운 UI 작동 중지
        {
            // 각 스킬 사용 UI: 스킬 사용이 가능할 때 스킬 키 누르면 작동 → 스킬 사용 여부 체크로 변경
            if (molotovActive) // 화염병
            {
                UseSkill(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ, iconQ, offSpriteQ);
                molotovActive = false;
            }
            if (!siegeActive && siegeCoolDown) // 시즈모드
            {
                UseSkill(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE, iconE, offSpriteE);
                siegeActive = true;
            }
            if (dodgeActive && !skillManager.siegemodeTS.isActive) // 회피
            {
                UseSkill(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace, iconSpace, offSpriteSpace);
                dodgeActive = false;
            }
            if (evdshotActive && !skillManager.siegemodeTS.isActive) // 회피사격
            {
                UseSkill(ref isCooldownMouseRight, ref currentTimeMouseRight, cooldownImageMouseRight, cooldownTimeMouseRight, iconMouseRight, offSpriteMouseRight);
                evdshotActive = false;
            }

            // 각 쿨다운 업데이트
            UpdateCooldown(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ, iconQ, onSpriteQ);
            UpdateCooldown(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE, iconE, onSpriteE);
            UpdateCooldown(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace, iconSpace, onSpriteSpace);
            UpdateCooldown(ref isCooldownMouseRight, ref currentTimeMouseRight, cooldownImageMouseRight, cooldownTimeMouseRight, iconMouseRight, onSpriteMouseRight);

            if (isSkillUse == true) // 전체 쿨타임 초기화 (포션)
            {
                isSkillUse = false;
                isCooldownQ = false; cooldownImageQ.fillAmount = 0; iconQ.sprite = onSpriteQ;
                isCooldownE = false; cooldownImageE.fillAmount = 0; iconE.sprite = onSpriteE;
                isCooldownSpace = false; cooldownImageSpace.fillAmount = 0; iconSpace.sprite = onSpriteSpace;
                isCooldownMouseRight = false; cooldownImageMouseRight.fillAmount = 0; iconMouseRight.sprite = onSpriteMouseRight;
            }
        }
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

    public IEnumerator SiegeCool(float duration) // 시즈모드 지속시간 표시
    {
        siegeBar.gameObject.SetActive(true); // 슬라이더 바 표시
        siegeBar.value = 1;

        float time = 0; // 경과 시간
        float coolTime = duration; // 쿨다운 시간

        while (siegeBar.value > 0 && siegeActive)
        {
            time += Time.deltaTime / coolTime; // 시간 계산
            siegeBar.value = Mathf.Lerp(1f, 0f, time); // 슬라이더 바 값 조절

            if (siegeBar.value <= 0)
            {
                siegeBar.gameObject.SetActive(false); // 쿨다운이 끝나면 비활성화
                time = 0f; // 경과 시간 초기화
            }

            yield return null; // 한 프레임 대기
        }
        siegeBar.gameObject.SetActive(false); // 쿨다운이 끝나면 비활성화
    }
}