using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityEffect : MonoBehaviour
{
    public Player player;

    private bool immunityActive;

    public void Immunity(float duration) { if (!immunityActive) { StartCoroutine(ImmunityCoroutine(duration)); } }

    private IEnumerator ImmunityCoroutine(float duration)
    {
        immunityActive = true;

        float initialArmorReduction = player.ArmorReduction;

        player.ChangeArmorReduction(50f);

        yield return new WaitForSeconds(duration); // ���� �ð� ���� ���

        player.ChangeArmorReduction(initialArmorReduction); // �ʱⰪ���� �ǵ�����

        immunityActive = false;
    }
}