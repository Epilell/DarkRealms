using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSkillUpgradeUI : MonoBehaviour
{

    //Public Field
    #region
    public static PopUpSkillUpgradeUI Instance;
    #endregion

    //Public Method
    #region

    /// <summary>
    /// 스킬 강화 패널 열기/닫기
    /// </summary>
    public void PopUpSkillUpgradePanel()
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
