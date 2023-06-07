using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public Player player;

    public bool isBlood;

    public void Blood(float damage)
    {
        if (isBlood)
        {
            player.P_Heal(damage / 4);
        }
    }

    public void SetBlood(float duration)
    {
        isBlood = true;

        StartCoroutine(ResetBlood(duration));
    }

    private IEnumerator ResetBlood(float duration)
    {
        yield return new WaitForSeconds(duration);

        isBlood = false;
    }
}