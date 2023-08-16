using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUpgradeSystem : MonoBehaviour
{
    //Method
    #region

    public void UpgradeStrength()
    {
        Player.Instance.data.StrLevel++;
    }

    public void UpgradeAgility()
    {
        Player.Instance.data.AgiLevel++;
    }

    public void UpgradeIntelligence()
    {
        Player.Instance.data.IntLevel++;
    }

    public void UpgradeArmorMastery()
    {
        Player.Instance.data.ArmorMasteryLevel++;
    }

    #endregion
}
