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
    /// ��ų ��ȭ �г� ����/�ݱ�
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
