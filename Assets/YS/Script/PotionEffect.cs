using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem; // PortionItemData 가져오기 위해 추가
using System.Text.RegularExpressions; // 정규 표현식 사용하기 위해 추가

public class PotionEffect : MonoBehaviour
{
    public Player player; // 플레이어 객체
    // public PortionItemData portionItemData; // 아이템 ← 변수당 아이템 데이터 하나만 가능
    public List<PortionItemData> portionItemDataList; // 아이템 데이터 목록 리스트

    public void UseEffect(string itemName)
    {
        PortionItemData targetItemData = null; // 원하는 아이템 데이터를 저장할 변수

        // 아이템 데이터 리스트를 돌면서 매개변수로 받은 itemName과 이름이 일치하는 아이템 찾기
        foreach (PortionItemData itemData in portionItemDataList)
        {
            if (itemData.Name == itemName) // 매개변수로 받은 itemName과 이름이 일치하는 아이템이 있는지 확인
            {
                Debug.Log(itemData.Name); // 테스트용 로그 출력 (아이템 이름) ← 얜 추후에 지울 거임
                Debug.Log(itemData.Value); // 테스트용 로그 출력 (아이템 값) ← 얘도
                targetItemData = itemData; // 찾은 아이템 데이터 저장
                break; // 아이템 찾았으면 종료
            }
        }

        if (targetItemData != null) // 찾은 아이템 데이터가 있다면
        {
            // 아이템 종류 별로 효과 넣기 ← itemType 등 프로퍼티 추가해도 될 듯
            // e.g. 이름에 hp가 포함되면 체력 회복 아이템
            // if (itemName.Contains("hp")) { player.CurrentHp += targetItemData.Value; }

            // 효과 종류 결정 하기
            string containWord = Regex.Match(itemName, "hp|mp|cooldown||power||blood||immunity||release||undying", RegexOptions.IgnoreCase).Value.ToLower();

            switch (containWord)
            {
                // 종류 별로 선택: 불필요한 것들은 지울 예정
                case "hp": // 체력 회복
                    Debug.Log("hp!"); // 테스트용 로그 ← 나중에 지울 거임
                    player.CurrentHp += targetItemData.Value; // 플레이어 체력 회복
                    break;
                case "mp": // 마나 회복
                    Debug.Log("mp!");
                    break;
                case "cooldown": // 쿨타임 감소
                    Debug.Log("cooldown!");
                    break;
                case "power": // 공격력 증가
                    Debug.Log("power!");
                    break;
                case "blood": // 흡혈
                    Debug.Log("blood!");
                    break;
                case "immunity": // 피해 면역
                    Debug.Log("immunity!");
                    break;
                case "release": // 이상 해제
                    Debug.Log("release!");
                    break;
                case "undying": // 불사
                    Debug.Log("undying!");
                    break;
                default: break;
            }
            
            /* 정규 표현식으로 itemName에 포함된 키워드 찾기
            Match(검색할 문자열, 찾을 패턴, RegexOptions: 일치 옵션을 제공하는 열거형 값의 비트 조합);
            RegexOptions.IgnoreCase: 대소문자를 구분 없이 일치 항목을 찾도록 지정
            .Value.ToLower(); : 문자열을 소문자로 반환 받음
            Regex.Match(itemName, "hp", RegexOptions.IgnoreCase).Value.ToLower()
            : itemName에서 "hp"라는 패턴과 대소문자 구분 없이 일치하는 문자열을 찾아서 소문자로 받겠다. */
        }
        else { Debug.Log("사용 불가한 아이템"); }
    }
}
/*
    - 참고하려고 적어 놓음 -

    public int ID => _id;
    public string Name => _name;
    public string Tooltip => _tooltip;
    public Sprite IconSprite => _iconSprite;

    [SerializeField] private int      _id;
    [SerializeField] private string   _name;    // 아이템 이름
    [Multiline]
    [SerializeField] private string   _tooltip; // 아이템 설명
    [SerializeField] private Sprite   _iconSprite; // 아이템 아이콘
    [SerializeField] private GameObject _dropItemPrefab; // 바닥에 떨어질 때 생성할 프리팹

    CountableItemData : ItemData 영역
    {
        public int MaxAmount => _maxAmount;
        [SerializeField] private int _maxAmount = 99;
    }

    PortionItemData : CountableItemData 영역
    {
        효과량(회복량 등)

        public float Value => _value;
        [SerializeField] private float _value;
        public override Item CreateItem()
        {
            return new PortionItem(this);
        }
    }
*/