using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillUpgradeSelectionSystem : MonoBehaviour
{
    //Field
    #region

    [SerializeField] private SkillUpgradeList data;
    [SerializeField] private int numberToChoose;
    [SerializeField] private List<Button> upgradeButtons;
    [SerializeField] private List<Image> upgradeImage;
    #endregion

    //Check Method
    #region

    /// <summary>
    /// ������ ��ų�� ����Ʈ�� ApplyUpgradeList�� �ִ��� Ȯ��
    /// </summary>
    /// <param name="_index">������ ����Ʈ �ε���</param>
    /// <returns>������ true ������ false</returns>
    private bool CheckUpgradeList(int _index)
    {
        for (int i = 0; i < data.ApplyUpgradeList.Count; i++)
        {
            // ���� ��ų�� �ִٸ�
            if (data.ApplyUpgradeList[i].SkillName == data.Upgradablelist[_index].SkillName)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// ������ ���׷��̵��� ��ų �ε��� ����
    /// </summary>
    /// <param name="_index"></param>
    /// <returns>�ش� ��ų�� �ε��� ����</returns>
    private int CheckApplyUpgradeIndex(int _index)
    {
        for (int i = 0; i < data.ApplyUpgradeList.Count; i++)
        {
            // ���� ��ų�� �ִٸ�
            if (data.ApplyUpgradeList[i].SkillName == data.Upgradablelist[_index].SkillName)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    //Method
    #region
    
    //���׷��̵� ��ư �ʱ�ȭ
    private void ResetAllButtonsFunction()
    {
        foreach (Button button in upgradeButtons)
        {
            button.onClick.RemoveAllListeners();
            TextMeshProUGUI targetText = button.GetComponentInChildren<TextMeshProUGUI>();
            targetText.text = "";
        }
    }

    /// <summary>
    /// �ؽ�Ʈ�� ������Ʈ�ϰ� ��ư�� ����� �߰�
    /// </summary>
    /// <param name="_count">������ ��ư</param>
    /// <param name="_raw">��ų ��ȣ</param>
    /// <param name="_column">���׷��̵� ��ȣ</param>
    private void UpdateText(int _count, int _raw, int _column)
    {
        //��ų �̹��� ����
        upgradeImage[_count].sprite = data.Upgradablelist[_raw].Icon;

        //��ų ���׷��̵� ���� �Ҵ�
        string upgradeName = data.Upgradablelist[_raw].UpgradeList[_column].Name;
        TextMeshProUGUI targetText = upgradeButtons[_count].GetComponentInChildren<TextMeshProUGUI>();
        targetText.text = "Upgrade Name : " + upgradeName;

        //��ų ���׷��̵� ��� ��ư�� �Ҵ�
        upgradeButtons[_count].onClick.AddListener(() => ApplyUpgrade(_raw,_column));
    }
    
    /// <summary>
    /// ���׷��̵� ����
    /// </summary>
    /// <param name="_raw">��ų ��ȣ</param>
    /// <param name="_column">���׷��̵� ��ȣ</param>
    private void ApplyUpgrade(int _raw, int _column)
    {
        // 1-1-1. ������ ��ų�� �̸��� ���� ����Ʈ�� �ִٸ�
        if (CheckUpgradeList(_raw))
        {
            // ������ ���׷��̵带 ����ĭ���� �̵�
            SkillUpgrade newUpgrade = new()
            {
                Name = data.Upgradablelist[_raw].UpgradeList[_column].Name,
                IsUpgrade = true
            };
            data.ApplyUpgradeList[CheckApplyUpgradeIndex(_raw)].UpgradeList.Add(newUpgrade);

            // ���� ���׷��̵� ����
            data.Upgradablelist[_raw].UpgradeList.RemoveAt(_column);
        }
        // 1-1-2. ������ ��ų�� �̸��� ���� ����Ʈ�� ���ٸ�
        else
        {
            // �� ������ �̸��� ���ٸ� �ش� ��ų �̸��� ��� ����
            SkillUpgrade newUpgrade = new()
            {
                Name = data.Upgradablelist[_raw].UpgradeList[_column].Name,
                IsUpgrade = true
            };
            Skill newList = new()
            {
                SkillName = data.Upgradablelist[_raw].SkillName,
                UpgradeList = new()
            };
            newList.UpgradeList.Add(newUpgrade);
            data.ApplyUpgradeList.Add(newList);

            // ���� ���׷��̵� ����
            data.Upgradablelist[_raw].UpgradeList.RemoveAt(_column);
        }

        ResetAllButtonsFunction();
        OnOffPanel();
    }

    /// <summary>
    /// ������ ���׷��̵� 3������ ����
    /// </summary>
    public void SelectRandomNum()
    {
        //������ ������ ��ư�� �ִ� ��� �ʱ�ȭ
        ResetAllButtonsFunction();

        // [��ų ����Ʈ, �� ��ų�� ���׷��̵� ����Ʈ] 2���� �迭
        int[][] grid = new int[data.Upgradablelist.Count][];

        for (int i = 0; i < data.Upgradablelist.Count; i++)
        {
            int upgradeCount = data.Upgradablelist[i].UpgradeList.Count;
            grid[i] = new int[upgradeCount];

            for (int j = 0; j < upgradeCount; j++)
            {
                grid[i][j] = j + 1;
            }
        }

        List<Tuple<int, int>> selectedCells = new List<Tuple<int, int>>();

        System.Random random = new();
        int emptyRow = grid.Length;

        while (selectedCells.Count < 3)
        {
            int randomRow = random.Next(grid.Length);
            

            if (grid[randomRow].Length > 0)
            {
                int randomColumn = random.Next(grid[randomRow].Length);

                selectedCells.Add(new Tuple<int, int>(randomRow, randomColumn));

                grid[randomRow][randomColumn] = grid[randomRow][grid[randomRow].Length - 1];
                Array.Resize(ref grid[randomRow], grid[randomRow].Length - 1);
            }
            else
            {
                emptyRow--;
                if(emptyRow < 0)
                {
                    break;
                }
            }

        }

        for(int i = 0; i < selectedCells.Count; i++)
        {
            //����â �ؽ�Ʈ ������Ʈ
            UpdateText(i, selectedCells[i].Item1, selectedCells[i].Item2);
        }
    }

    /// <summary>
    /// ��ų ���� �г� ����/�ݱ�
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
    #region

    private void OnEnable()
    {
        SelectRandomNum();
    }

    #endregion
}
