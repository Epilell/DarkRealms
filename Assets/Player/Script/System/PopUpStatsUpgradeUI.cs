using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpStatsUpgradeUI : MonoBehaviour
{
    //Public Field
    #region
    public static PopUpStatsUpgradeUI Instance;
    #endregion

    //Public Method
    #region

    /// <summary>
    /// 스킬 강화 패널 열기/닫기
    /// </summary>
    public void PopUpStatsUpgradePanel()
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
}
