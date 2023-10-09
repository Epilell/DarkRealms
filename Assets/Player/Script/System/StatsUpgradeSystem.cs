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

    [Header("���׷��̵� ��� ����Ʈ", order = 1), Space(5)]
    [SerializeField]
    private List<UpgradeCost> upgradeCostList;

    [Header("������", order = 2), Space(5)]
    [SerializeField] private ItemData Coin;
    [SerializeField] private List<GameObject> upgradePanel = new();
    [SerializeField] private List<TextMeshProUGUI> Descriptions = new();
    [SerializeField] private TextMeshProUGUI InfoPanel;

    private Inventory Inventory;
    private Inventory WareHouse;

    #endregion

    //Method
    #region .

    //��ư�� �޼ҵ� �Ҵ�
    private void UpdateButtons()
    {
        for(int i = 0; i < playerData.Stats.Count; i++)
        {
            //Ŭ���� ���� �ذ�� ����
            int index = i;

            //��ư�� ��� �Ҵ�
            Button btn = upgradePanel[i].GetComponentInChildren<Button>();
            btn.onClick.AddListener(() => ApplyUpgrade(index));
        }

        //�ؽ�Ʈ ����
        UpdateDescription();
    }

    //���׷��̵� ���
    private void ApplyUpgrade(int num)
    {
        int cost = upgradeCostList[num].StartCost + (upgradeCostList[num].IncreaseCost * playerData.Stats[num].Level);
        if (playerData.Stats[num].Level < 5)
        {
            if (Inventory != null && Inventory.UseMaterial(Coin, cost))
            {
                playerData.Stats[num].Level++;
                UpdateInfoPanel("��ȭ ����"); UpdateDescription();
            }
            else if (WareHouse != null && WareHouse.UseMaterial(Coin, cost))
            {
                playerData.Stats[num].Level++;
                UpdateInfoPanel("��ȭ ����"); UpdateDescription();
            }
            else
            {
                UpdateInfoPanel("��ȭ ���� \n ��� ����");
            }
        }
        else
        {
            UpdateInfoPanel("�ִ� ����");
        }
    }

    private void UpdateDescription()
    {
        int[] num = { 10, 2, 2, 2 };
        string[] text = { "ü�� ����", "�̵��ӵ� ����", "��ų ��Ÿ�� ����", "������ ����" };
        //�ؽ�Ʈ ����
        for(int i = 0; i < 4; i++)
        {
            int cost = upgradeCostList[i].StartCost + (upgradeCostList[i].IncreaseCost * playerData.Stats[i].Level);
            Descriptions[i].text = text[i]+ "\n" + playerData.Stats[i].Level * num[i] + "% \n"+"cost : " + cost  ;
        }
    }

    //���׷��̵� ���� �г�Ȱ��ȭ
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
    /// ��ų ��ȭ �г� ����/�ݱ�
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
