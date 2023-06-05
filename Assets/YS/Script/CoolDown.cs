using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    [SerializeField] private Image iconQ, iconE, iconSpace, iconMouseRight; // ��ų ������
    [SerializeField] private Image cooldownImageQ, cooldownImageE, cooldownImageSpace, cooldownImageMouseRight; // �� ��ų�� ��ٿ� �̹���
    [SerializeField] private Sprite onSpriteQ, offSpriteQ, onSpriteE, offSpriteE, onSpriteSpace, offSpriteSpace, onSpriteMouseRight, offSpriteMouseRight; // ��������Ʈ

    [SerializeField] private float cooldownTimeQ, cooldownTimeE, cooldownTimeSpace, cooldownTimeMouseRight; // �� ��ų�� ��ٿ� �ð�
    private float currentTimeQ, currentTimeE, currentTimeSpace, currentTimeMouseRight; // ������ ���� ��ٿ� �ð�
    private bool isCooldownQ, isCooldownE, isCooldownSpace, isCooldownMouseRight; // ������ ��ٿ� ����

    void Start() // �����ϰ� �ʱ�ȭ
    {
        cooldownImageQ.fillAmount = 0;
        cooldownImageE.fillAmount = 0;
        cooldownImageSpace.fillAmount = 0;
        cooldownImageMouseRight.fillAmount = 0;
    }

    void Update()
    {
        // �� ��ų ��� UI
        if (Input.GetKeyDown(KeyCode.Q)) { UseSkill(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ, iconQ, offSpriteQ); }
        if (Input.GetKeyDown(KeyCode.E)) { UseSkill(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE, iconE, offSpriteE); }
        if (Input.GetKeyDown(KeyCode.Space)) { UseSkill(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace, iconSpace, offSpriteSpace); }
        if (Input.GetKeyDown(KeyCode.Mouse1)) { UseSkill(ref isCooldownMouseRight, ref currentTimeMouseRight, cooldownImageMouseRight, cooldownTimeMouseRight, iconMouseRight, offSpriteMouseRight); }

        // �� ��ٿ� ������Ʈ
        UpdateCooldown(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ, iconQ, onSpriteQ);
        UpdateCooldown(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE, iconE, onSpriteE);
        UpdateCooldown(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace, iconSpace, onSpriteSpace);
        UpdateCooldown(ref isCooldownMouseRight, ref currentTimeMouseRight, cooldownImageMouseRight, cooldownTimeMouseRight, iconMouseRight, onSpriteMouseRight);
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