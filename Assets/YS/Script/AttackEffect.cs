using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public Player player;
    public P_Data pData;

    public void IncreaseDamage(float value)
    {
        StartCoroutine(DamageCoroutine(value));
    }

    private IEnumerator DamageCoroutine(float dutation)
    {
        float originalDamage = pData.Damage;

        // 데미지 증가
        pData.Damage *= 1.25f;

        yield return new WaitForSeconds(dutation);

        // 원래의 데미지로 복구
        pData.Damage = originalDamage;
    }
}