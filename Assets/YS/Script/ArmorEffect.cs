using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEffect : MonoBehaviour
{
    public P_Data p_data;

    private float originalHelmet;
    private float originalBody;
    private float originalLeg;
    private float originalShoes;

    public void SetArmor(float value)
    {
        // 원래의 값 저장
        originalHelmet = p_data.Helmet;
        originalBody = p_data.Body;
        originalLeg = p_data.Leg;
        originalShoes = p_data.Shoes;

        // 5초 동안 증가
        p_data.Helmet *= value / 100f + 1f;
        originalBody *= value / 100f + 1f;
        p_data.Leg *= value / 100f + 1f;
        p_data.Shoes *= value / 100f + 1f;

        StartCoroutine(ResetShoesAfterDuration(5f));
    }

    private IEnumerator ResetShoesAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        // 원래의 값으로 복원
        p_data.Helmet = originalHelmet;
        p_data.Body = originalBody;
        p_data.Leg = originalLeg;
        p_data.Shoes = originalShoes;
    }
}