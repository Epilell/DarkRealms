using Rito.InventorySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
class UpgradeCost
{
    public string StatsName;
    public int StartCost = 10;
    public int IncreaseCost = 30;
}

public class StatsUpgradeSystem : MonoBehaviour
{
    //Field
    #region .
    public static StatsUpgradeSystem instance;
    public PlayerData playerData;

    [TextArea(1, 5), Space(5)]
    public string describe;

    [Header("업그레이드 비용 리스트", order = 1), Space(5)]
    [SerializeField]
    private List<UpgradeCost> upgradeCostList;

    [Header("나머지", order = 2), Space(5)]
    [SerializeField] private ItemData Coin;
    [SerializeField] private List<GameObject> upgradePanel = new();
    [SerializeField] private List<TextMeshProUGUI> Descriptions = new();
    [SerializeField] private TextMeshProUGUI InfoPanel;

    private Inventory Inventory;
    private Inventory WareHouse;

    #endregion

    //Method
    #region .

    //버튼에 메소드 할당
    private void UpdateButtons()
    {
        for(int i = 0; i < playerData.Stats.Count; i++)
        {
            //클로저 동작 해결용 변수
            int index = i;

            //버튼에 기능 할당
            Button btn = upgradePanel[i].GetComponentInChildren<Button>();
            btn.onClick.AddListener(() => ApplyUpgrade(index));
        }

        //텍스트 수정
        UpdateDescription();
    }

    //업그레이드 기능
    private void ApplyUpgrade(int num)
    {
        int cost = upgradeCostList[num].StartCost + (upgradeCostList[num].IncreaseCost * playerData.Stats[num].Level);
        if (playerData.Stats[num].Level < 5)
        {
            if (Inventory != null && Inventory.UseMaterial(Coin, cost))
            {
                playerData.Stats[num].Level++;
                UpdateInfoPanel("강화 성공"); UpdateDescription();
            }
            else if (WareHouse != null && WareHouse.UseMaterial(Coin, cost))
            {
                playerData.Stats[num].Level++;
                UpdateInfoPanel("강화 성공"); UpdateDescription();
            }
            else
            {
                UpdateInfoPanel("강화 실패 \n 재료 부족");
            }
        }
        else
        {
            UpdateInfoPanel("최대 레벨");
        }
    }

    private void UpdateDescription()
    {
        int[] num = { 10, 2, 2, 2 };
        string[] text = { "체력 증가", "이동속도 증가", "스킬 쿨타임 감소", "데미지 감소" };
        //텍스트 수정
        for(int i = 0; i < 4; i++)
        {
            int cost = upgradeCostList[i].StartCost + (upgradeCostList[i].IncreaseCost * playerData.Stats[i].Level);
            Descriptions[i].text = text[i]+ "\n" + playerData.Stats[i].Level * num[i] + "% \n"+"cost : " + cost  ;
        }
    }

    //업그레이드 여부 패널활성화
    private void UpdateInfoPanel(string _desc, float _time = 2f)
    {
        InfoPanel.text = _desc;
        InfoPanel.gameObject.SetActive(true);
        Invoke("CloseInfoPanel", _time);
    }

    private void CloseInfoPanel()
    {
        InfoPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 스킬 강화 패널 열기/닫기
    /// </summary>
    public void OnOffPanel()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    #endregion

    //Unity Event
    #region .

    private void Start()
    {
        Inventory = GameObject.FindWithTag("GameController").GetComponentInChildren<Inventory>();
        WareHouse = GameObject.Find("WareHouse").GetComponent<Inventory>();
    }

    private void OnEnable()
    {
        UpdateButtons();
    }

    #endregion
}
