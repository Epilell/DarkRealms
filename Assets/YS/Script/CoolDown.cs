using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    [SerializeField] private float cooldownTimeQ, cooldownTimeE, cooldownTimeSpace; // �� ��ų�� ��ٿ� �ð�
    [SerializeField] private Image cooldownImageQ, cooldownImageE, cooldownImageSpace; // �� ��ų�� ��ٿ� �̹���

    private float currentTimeQ, currentTimeE, currentTimeSpace; // ������ ���� ��ٿ� �ð�
    private bool isCooldownQ, isCooldownE, isCooldownSpace; // ������ ��ٿ� ����

    void Start()
    {
        // ó������ �����ϰ� �ʱ�ȭ
        cooldownImageQ.fillAmount = 0;
        cooldownImageE.fillAmount = 0;
        cooldownImageSpace.fillAmount = 0;
    }

    void Update()
    {
        // Q, E, Space Ű ���� ��ų ���
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

        // �� ��ٿ� ������Ʈ
        UpdateCooldown(ref isCooldownQ, ref currentTimeQ, cooldownImageQ, cooldownTimeQ);
        UpdateCooldown(ref isCooldownE, ref currentTimeE, cooldownImageE, cooldownTimeE);
        UpdateCooldown(ref isCooldownSpace, ref currentTimeSpace, cooldownImageSpace, cooldownTimeSpace);
    }

    private void UseSkill(ref bool isCooldown, ref float currentTime, Image cooldownImage, float cooldownTime) // ��ų ��� �� ��ٿ� ����
    {
        if (!isCooldown)
        {
            isCooldown = true;
            currentTime = cooldownTime;
            // ��ų ȣ�� �κ�
        }
    }

    private void UpdateCooldown(ref bool isCooldown, ref float currentTime, Image cooldownImage, float cooldownTime) // ��ٿ� ���� ������Ʈ �� �̹��� ���� ������Ʈ
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
            }
        }
    }
}