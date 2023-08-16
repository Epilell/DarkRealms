using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityEffect : MonoBehaviour
{
    public Player player;

    private bool immunityActive;

    private void Start() { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }

    public void Immunity(float duration) { if (!immunityActive) { StartCoroutine(ImmunityCoroutine(duration)); } }

    private IEnumerator ImmunityCoroutine(float duration)
    {
        immunityActive = true;

        float initialArmorReduction = player.ArmorReductionBySkill;

        player.ChangeDamageReduction(initialArmorReduction * 100 + 50f);

        yield return new WaitForSeconds(duration); // ���� �ð� ���� ���

        player.ChangeDamageReduction(initialArmorReduction * 100); // �ʱⰪ���� �ǵ�����

        immunityActive = false;
    }
}