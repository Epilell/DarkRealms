using Rito.InventorySystem; // PortionItemData 가져오기 위해 추가
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions; // 정규 표현식 사용하기 위해 추가
using UnityEngine;

public class PotionEffect : MonoBehaviour
{
    public Player player; // 플레이어 객체
    public List<PortionItemData> portionItemDataList; // 아이템 데이터 목록 리스트

    private void Start() { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }

    public void UseEffect(string itemName)
    {
        PortionItemData targetItemData = null; // 원하는 아이템 데이터를 저장할 변수

        // 아이템 데이터 리스트를 돌면서 itemName과 이름이 일치하는 아이템 찾기
        foreach (PortionItemData itemData in portionItemDataList)
        {
            if (itemData.Name == itemName) // 매개변수로 받은 itemName과 이름이 일치하는 아이템이 있는지 확인
            {
                targetItemData = itemData; // 찾은 아이템 데이터 저장
                break; // 아이템 찾았으면 종료
            }
        }

        if (targetItemData != null) // 찾은 아이템 데이터가 있다면
        {
            // 아이템 종류 별로 효과 결정: e.g. 이름에 hp가 포함되면 체력 회복 아이템
            string containWord = Regex.Match(itemName, "hp|power|armor|cooldown|blood|immunity|undying", RegexOptions.IgnoreCase).Value.ToLower();

            switch (containWord) // 종류 별로 선택
            {
                case "hp": // 회복
                    player.P_Heal(targetItemData.Value);
                    break;
                case "power": // 공격력 증가
                    GetComponent<AttackEffect>().IncreaseDamage(targetItemData.Value);
                    break;
                case "armor": // 방어력 증가
                    GetComponent<ArmorEffect>().SetArmor(targetItemData.Value);
                    break;
                case "cooldown": // 쿨타임 감소
                    GetComponent<CoolDownEffect>().SkillCoolDown();
                    break;
                case "blood": // 흡혈
                    GetComponent<BloodEffect>().SetBlood(targetItemData.Value);
                    break;
                case "immunity": // 면역
                    GetComponent<ImmunityEffect>().Immunity(targetItemData.Value);
                    break;
                case "undying": // 불사
                    GetComponent<UndyingEffect>().Undying(targetItemData.Value);
                    break;
                default: break;
            }
        }
        else { Debug.Log("사용 불가"); }
    }
}