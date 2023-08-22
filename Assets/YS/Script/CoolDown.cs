using System.Collections;
using System.Collections.Generic;
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

    private float cooldownTimeQ, cooldownTimeE, cooldownTimeSpace, cooldownTimeMouseRight; // �� ��ų�� ��ٿ� �ð�
    private float currentTimeQ, currentTimeE, currentTimeSpace, currentTimeMouseRight; // ������ ���� ��ٿ� �ð�
    private bool isCooldownQ, isCooldownE, isCooldownSpace, isCooldownMouseRight; // ������ ��ٿ� ����

    public bool isSkillUse;

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
        if (FindObjectOfType<SkillManager>().isSkillCanUse) // �κ��丮, ����â �� �ϳ��� ���������� ��ٿ� UI �۵� ����
        {
            // �� ��ų ��� UI: ��ų ����� ������ �� ��ų Ű ������ �۵���
            if (FindObjectOfType<SkillManager>().molotovTS.canUse == true) if (Input.GetKeyDown(KeyCode.Q)) { UseSkill(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ, iconQ, offSpriteQ); }
            if (FindObjectOfType<SkillManager>().siegemodeTS.canUse == true && FindObjectOfType<SkillManager>().siegemodeTS.isActive == true) if (Input.GetKeyDown(KeyCode.E)) { UseSkill(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE, iconE, offSpriteE); }
            if (FindObjectOfType<SkillManager>().dodgeTS.canUse == true && !FindObjectOfType<SkillManager>().siegemodeTS.isActive) if (Input.GetKeyDown(KeyCode.Space)) { UseSkill(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace, iconSpace, offSpriteSpace); }
            if (FindObjectOfType<SkillManager>().evdshotTS.canUse == true && !FindObjectOfType<SkillManager>().siegemodeTS.isActive) if (Input.GetKeyDown(KeyCode.Mouse1)) { UseSkill(ref isCooldownMouseRight, ref currentTimeMouseRight, cooldownImageMouseRight, cooldownTimeMouseRight, iconMouseRight, offSpriteMouseRight); }

            // �� ��ٿ� ������Ʈ
            UpdateCooldown(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ, iconQ, onSpriteQ);
            UpdateCooldown(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE, iconE, onSpriteE);
            UpdateCooldown(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace, iconSpace, onSpriteSpace);
            UpdateCooldown(ref isCooldownMouseRight, ref currentTimeMouseRight, cooldownImageMouseRight, cooldownTimeMouseRight, iconMouseRight, onSpriteMouseRight);

            if (isSkillUse == true) // ��ü ��Ÿ�� �ʱ�ȭ
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
}