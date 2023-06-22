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
        cooldownTimeQ = skillManager.molotovdata.CoolTime; // 10��
        cooldownTimeE = skillManager.siegemodedata.CoolTime; // 20��
        cooldownTimeSpace = skillManager.dodgedata.CoolTime; // 3��
        cooldownTimeMouseRight = skillManager.evdshotdata.CoolTime; // 10��

        // �����ϰ� �ʱ�ȭ
        cooldownImageQ.fillAmount = 0;
        cooldownImageE.fillAmount = 0;
        cooldownImageSpace.fillAmount = 0;
        cooldownImageMouseRight.fillAmount = 0;
    }

    void Update()
    {
        // �� ��ų ��� UI: ��ų ����� ������ �� ��ų Ű ������ �۵���
        if (FindObjectOfType<SkillManager>().molotovdata.CanUse == true) if (Input.GetKeyDown(KeyCode.Q)) { UseSkill(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ, iconQ, offSpriteQ); }
        if (FindObjectOfType<SkillManager>().siegemodedata.CanUse == true && FindObjectOfType<SkillManager>().siegemodedata.IsActive == true) if (Input.GetKeyDown(KeyCode.E)) { UseSkill(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE, iconE, offSpriteE); }
        if (FindObjectOfType<SkillManager>().dodgedata.CanUse == true && !FindObjectOfType<SkillManager>().siegemodedata.IsActive) if (Input.GetKeyDown(KeyCode.Space)) { UseSkill(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace, iconSpace, offSpriteSpace); }
        if (FindObjectOfType<SkillManager>().evdshotdata.CanUse == true && !FindObjectOfType<SkillManager>().siegemodedata.IsActive) if (Input.GetKeyDown(KeyCode.Mouse1)) { UseSkill(ref isCooldownMouseRight, ref currentTimeMouseRight, cooldownImageMouseRight, cooldownTimeMouseRight, iconMouseRight, offSpriteMouseRight); }

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