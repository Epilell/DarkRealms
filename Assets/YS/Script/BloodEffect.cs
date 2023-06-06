using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public Player player;
    public Bullet bullet;
    public bool isBlood;

    public void Blood(float damage)
    {
        if (isBlood)
        {
            player.P_Heal(damage / 2);
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