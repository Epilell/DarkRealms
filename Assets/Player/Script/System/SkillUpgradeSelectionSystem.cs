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
    /// 선택한 스킬의 리스트가 ApplyUpgradeList에 있는지 확인
    /// </summary>
    /// <param name="_index">선택한 리스트 인덱스</param>
    /// <returns>있으면 true 없으면 false</returns>
    private bool CheckUpgradeList(int _index)
    {
        for (int i = 0; i < data.ApplyUpgradeList.Count; i++)
        {
            // 같은 스킬이 있다면
            if (data.ApplyUpgradeList[i].SkillName == data.Upgradablelist[_index].SkillName)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 적용할 업그레이드의 스킬 인덱스 리턴
    /// </summary>
    /// <param name="_index"></param>
    /// <returns>해당 스킬의 인덱스 리턴</returns>
    private int CheckApplyUpgradeIndex(int _index)
    {
        for (int i = 0; i < data.ApplyUpgradeList.Count; i++)
        {
            // 같은 스킬이 있다면
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
    
    //업그레이드 버튼 초기화
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
    /// 텍스트를 업데이트하고 버튼에 기능을 추가
    /// </summary>
    /// <param name="_count">변경할 버튼</param>
    /// <param name="_raw">스킬 번호</param>
    /// <param name="_column">업그레이드 번호</param>
    private void UpdateText(int _count, int _raw, int _column)
    {
        //스킬 이미지 설정
        upgradeImage[_count].sprite = data.Upgradablelist[_raw].Icon;

        //스킬 업그레이드 내용 할당
        string upgradeName = data.Upgradablelist[_raw].UpgradeList[_column].Name;
        TextMeshProUGUI targetText = upgradeButtons[_count].GetComponentInChildren<TextMeshProUGUI>();
        targetText.text = "Upgrade Name : " + upgradeName;

        //스킬 업그레이드 기능 버튼에 할당
        upgradeButtons[_count].onClick.AddListener(() => ApplyUpgrade(_raw,_column));
    }
    
    /// <summary>
    /// 업그레이드 적용
    /// </summary>
    /// <param name="_raw">스킬 번호</param>
    /// <param name="_column">업그레이드 번호</param>
    private void ApplyUpgrade(int _raw, int _column)
    {
        // 1-1-1. 선택한 스킬의 이름을 가진 리스트가 있다면
        if (CheckUpgradeList(_raw))
        {
            // 선택한 업그레이드를 적용칸으로 이동
            SkillUpgrade newUpgrade = new()
            {
                Name = data.Upgradablelist[_raw].UpgradeList[_column].Name,
                IsUpgrade = true
            };
            data.ApplyUpgradeList[CheckApplyUpgradeIndex(_raw)].UpgradeList.Add(newUpgrade);

            // 선택 업그레이드 제거
            data.Upgradablelist[_raw].UpgradeList.RemoveAt(_column);
        }
        // 1-1-2. 선택한 스킬의 이름을 가진 리스트가 없다면
        else
        {
            // 비교 과정에 이름이 없다면 해당 스킬 이름의 목록 생성
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

            // 선택 업그레이드 제거
            data.Upgradablelist[_raw].UpgradeList.RemoveAt(_column);
        }

        ResetAllButtonsFunction();
        OnOffPanel();
    }

    /// <summary>
    /// 선택할 업그레이드 3가지를 선정
    /// </summary>
    public void SelectRandomNum()
    {
        //선택전 기존의 버튼에 있는 기능 초기화
        ResetAllButtonsFunction();

        // [스킬 리스트, 각 스킬별 업그레이드 리스트] 2차원 배열
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
            //선택창 텍스트 업데이트
            UpdateText(i, selectedCells[i].Item1, selectedCells[i].Item2);
        }
    }

    /// <summary>
    /// 스킬 선택 패널 열기/닫기
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
