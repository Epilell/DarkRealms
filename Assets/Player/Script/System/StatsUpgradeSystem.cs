using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpgradeSystem : MonoBehaviour
{
    //Field
    #region .
    public static StatsUpgradeSystem instance;
    public PlayerData playerData;

    [TextArea(1, 5), Space(5)]
    public string describe;

    private int[] UpgradePrice = { 10, 10, 10, 10 };

    [SerializeField] private ItemData Coin;
    [SerializeField] private List<GameObject> upgradePanel = new();
    [SerializeField] private List<TextMeshProUGUI> Descriptions = new();
    [SerializeField] private TextMeshProUGUI InfoPanel;

    private Inventory Inventory;
    private Inventory WareHouse;

    #endregion

    //Method
    #region .

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

    private void ApplyUpgrade(int num)
    {
        if (playerData.Stats[num].Level < 5)
        {
            if (Inventory.UseMaterial(Coin, UpgradePrice[num]) && Inventory != null)
            {
                playerData.Stats[num].Level++;
                UpgradePrice[num] += 10 + (10 * playerData.Stats[num].Level);
                UpdateInfoPanel("강화 성공"); UpdateDescription();
            }
            else if (WareHouse.UseMaterial(Coin, UpgradePrice[num]) && WareHouse != null)
            {
                playerData.Stats[num].Level++;
                UpgradePrice[num] += 10 + (10 * playerData.Stats[num].Level);
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
        //텍스트 수정
        Descriptions[0].text = "+" + playerData.Stats[0].Level * 10 + " % \n 체력 증가";
        Descriptions[1].text = "+" + playerData.Stats[1].Level * 2 + " % \n 이동속도 증가";
        Descriptions[2].text = "-" + playerData.Stats[2].Level * 2 + " % \n 스킬 쿨타임 감소";
        Descriptions[3].text = "+" + playerData.Stats[3].Level * 2 + " % \n 데미지 감소";
    }

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
