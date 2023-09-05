using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    public SkillManager skillManager;

    [Header("SkillIcon")] // ��ų ������
    [SerializeField] private Image iconQ;
    [SerializeField] private Image iconE;
    [SerializeField] private Image iconSpace;
    [SerializeField] private Image iconMouseRight;

    [Header("SkillImage")] // ��ų �̹��� ��������Ʈ
    [SerializeField] private Sprite onSpriteQ;
    [SerializeField] private Sprite offSpriteQ;
    [SerializeField] private Sprite onSpriteE;
    [SerializeField] private Sprite offSpriteE;
    [SerializeField] private Sprite onSpriteSpace;
    [SerializeField] private Sprite offSpriteSpace;
    [SerializeField] private Sprite onSpriteMouseRight;
    [SerializeField] private Sprite offSpriteMouseRight;

    [Header("CoolDownImage")] // ��ٿ� �̹���
    [SerializeField] private Image cooldownImageQ;
    [SerializeField] private Image cooldownImageE;
    [SerializeField] private Image cooldownImageSpace;
    [SerializeField] private Image cooldownImageMouseRight;

    [Header("CoolDownBar")] // ��ٿ� ��
    [SerializeField] private Slider siegeBar; // ������ ���ӽð� ǥ��

    private float cooldownTimeQ, cooldownTimeE, cooldownTimeSpace, cooldownTimeMouseRight; // �� ��ų�� ��ٿ� �ð�
    private float currentTimeQ, currentTimeE, currentTimeSpace, currentTimeMouseRight; // ������ ���� ��ٿ� �ð�
    private bool isCooldownQ, isCooldownE, isCooldownSpace, isCooldownMouseRight; // ������ ��ٿ� ����

    public bool isSkillUse; // ��ü ��Ÿ�� �ʱ�ȭ ����
    public bool molotovActive, siegeActive, dodgeActive, evdshotActive; // ��ų ��� ���� üũ��
    public bool siegeCoolDown = false; // ó������ ��ٿ� ���� �ʰ� �ϱ� ���� ����

    void Start()
    {
        // �ʱ� ��Ÿ�� �ʱ�ȭ
        cooldownTimeQ = skillManager.molotovData.CoolTime; // 10��
        cooldownTimeE = skillManager.siegemodeData.CoolTime; // 20��
        cooldownTimeSpace = skillManager.dodgeData.CoolTime; // 3��
        cooldownTimeMouseRight = skillManager.evdshotData.CoolTime; // 10��

        // �����ϰ� �ʱ�ȭ
        cooldownImageQ.fillAmount = 0;
        cooldownImageE.fillAmount = 0;
        cooldownImageSpace.fillAmount = 0;
        cooldownImageMouseRight.fillAmount = 0;
    }

    void Update()
    {
        if (skillManager.isSkillCanUse) // �κ��丮, ����â �� �ϳ��� ���������� ��ٿ� UI �۵� ����
        {
            // �� ��ų ��� UI: ��ų ����� ������ �� ��ų Ű ������ �۵� �� ��ų ��� ���� üũ�� ����
            if (molotovActive) // ȭ����
            {
                UseSkill(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ, iconQ, offSpriteQ);
                molotovActive = false;
            }
            if (!siegeActive && siegeCoolDown) // ������
            {
                UseSkill(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE, iconE, offSpriteE);
                siegeActive = true;
            }
            if (dodgeActive && !skillManager.siegemodeTS.isActive) // ȸ��
            {
                UseSkill(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace, iconSpace, offSpriteSpace);
                dodgeActive = false;
            }
            if (evdshotActive && !skillManager.siegemodeTS.isActive) // ȸ�ǻ��
            {
                UseSkill(ref isCooldownMouseRight, ref currentTimeMouseRight, cooldownImageMouseRight, cooldownTimeMouseRight, iconMouseRight, offSpriteMouseRight);
                evdshotActive = false;
            }

            // �� ��ٿ� ������Ʈ
            UpdateCooldown(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ, iconQ, onSpriteQ);
            UpdateCooldown(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE, iconE, onSpriteE);
            UpdateCooldown(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace, iconSpace, onSpriteSpace);
            UpdateCooldown(ref isCooldownMouseRight, ref currentTimeMouseRight, cooldownImageMouseRight, cooldownTimeMouseRight, iconMouseRight, onSpriteMouseRight);

            if (isSkillUse == true) // ��ü ��Ÿ�� �ʱ�ȭ (����)
            {
                isSkillUse = false;
                isCooldownQ = false; cooldownImageQ.fillAmount = 0; iconQ.sprite = onSpriteQ;
                isCooldownE = false; cooldownImageE.fillAmount = 0; iconE.sprite = onSpriteE;
                isCooldownSpace = false; cooldownImageSpace.fillAmount = 0; iconSpace.sprite = onSpriteSpace;
                isCooldownMouseRight = false; cooldownImageMouseRight.fillAmount = 0; iconMouseRight.sprite = onSpriteMouseRight;
            }
        }
    }

    private void UseSkill(ref bool isCooldown, ref float currentTime, Image cooldownImage, float cooldownTime, Image skillIcon, Sprite offSprite) // ��ų ��� �� ��ٿ� ����
    {
        if (!isCooldown)
        {
            isCooldown = true;
            currentTime = cooldownTime;
            skillIcon.sprite = offSprite;
        }
    }

    private void UpdateCooldown(ref bool isCooldown, ref float currentTime, Image cooldownImage, float cooldownTime, Image skillIcon, Sprite onSprite) // ��ٿ� ���� ������Ʈ �� �̹��� ���� ������Ʈ
    {
        if (isCooldown)
        {
            currentTime -= Time.deltaTime; // ��ٿ� �ð� ����
            cooldownImage.fillAmount = currentTime / cooldownTime; // ��ٿ� �̹��� ���� ����

            if (currentTime <= 0) // ��ٿ��� ���� ��� �ʱ�ȭ
            {
                currentTime = 0;
                isCooldown = false;
                cooldownImage.fillAmount = 0;
                skillIcon.sprite = onSprite;
            }
        }
    }

    public IEnumerator SiegeCool(float duration) // ������ ���ӽð� ǥ��
    {
        siegeBar.gameObject.SetActive(true); // �����̴� �� ǥ��
        siegeBar.value = 1;

        float time = 0; // ��� �ð�
        float coolTime = duration; // ��ٿ� �ð�

        while (siegeBar.value > 0 && siegeActive)
        {
            time += Time.deltaTime / coolTime; // �ð� ���
            siegeBar.value = Mathf.Lerp(1f, 0f, time); // �����̴� �� �� ����

            if (siegeBar.value <= 0)
            {
                siegeBar.gameObject.SetActive(false); // ��ٿ��� ������ ��Ȱ��ȭ
                time = 0f; // ��� �ð� �ʱ�ȭ
            }

            yield return null; // �� ������ ���
        }
        siegeBar.gameObject.SetActive(false); // ��ٿ��� ������ ��Ȱ��ȭ
    }
}