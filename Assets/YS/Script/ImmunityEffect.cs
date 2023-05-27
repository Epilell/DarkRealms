using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmunityEffect : MonoBehaviour
{
    public Player player;

    private bool immunityActive;

    public void Immunity(float duration)
    {
        if (!immunityActive)
        {
            immunityActive = true;
            Invoke(nameof(EndImmunity), duration); // 지정된 시간 후 효과 종료
        }
    }

    /*private void Update()
    {
        if (immunityActive)
        {
            player.ChangeArmorReduction(0.6f);
        }
        else { }
    }*/

    // 효과 종료
    private void EndImmunity()
    {
        immunityActive = false;
    }
}