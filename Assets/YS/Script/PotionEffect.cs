using Rito.InventorySystem; // PortionItemData �������� ���� �߰�
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions; // ���� ǥ���� ����ϱ� ���� �߰�
using UnityEngine;

public class PotionEffect : MonoBehaviour
{
    public Player player; // �÷��̾� ��ü
    public List<PortionItemData> portionItemDataList; // ������ ������ ��� ����Ʈ

    private void Start() { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }

    public void UseEffect(string itemName)
    {
        PortionItemData targetItemData = null; // ���ϴ� ������ �����͸� ������ ����

        // ������ ������ ����Ʈ�� ���鼭 itemName�� �̸��� ��ġ�ϴ� ������ ã��
        foreach (PortionItemData itemData in portionItemDataList)
        {
            if (itemData.Name == itemName) // �Ű������� ���� itemName�� �̸��� ��ġ�ϴ� �������� �ִ��� Ȯ��
            {
                targetItemData = itemData; // ã�� ������ ������ ����
                break; // ������ ã������ ����
            }
        }

        if (targetItemData != null) // ã�� ������ �����Ͱ� �ִٸ�
        {
            // ������ ���� ���� ȿ�� ����: e.g. �̸��� hp�� ���ԵǸ� ü�� ȸ�� ������
            string containWord = Regex.Match(itemName, "hp|power|armor|cooldown|blood|immunity|undying", RegexOptions.IgnoreCase).Value.ToLower();

            switch (containWord) // ���� ���� ����
            {
                case "hp": // ȸ��
                    player.P_Heal(targetItemData.Value);
                    break;
                case "power": // ���ݷ� ����
                    GetComponent<AttackEffect>().IncreaseDamage(targetItemData.Value);
                    break;
                case "armor": // ���� ����
                    GetComponent<ArmorEffect>().SetArmor(targetItemData.Value);
                    break;
                case "cooldown": // ��Ÿ�� ����
                    GetComponent<CoolDownEffect>().SkillCoolDown();
                    break;
                case "blood": // ����
                    GetComponent<BloodEffect>().SetBlood(targetItemData.Value);
                    break;
                case "immunity": // �鿪
                    GetComponent<ImmunityEffect>().Immunity(targetItemData.Value);
                    break;
                case "undying": // �һ�
                    GetComponent<UndyingEffect>().Undying(targetItemData.Value);
                    break;
                default: break;
            }
        }
        else { Debug.Log("��� �Ұ�"); }
    }
}