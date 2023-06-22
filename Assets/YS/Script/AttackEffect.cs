using System.Collections;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public bool powerUp; // ����

    public void IncreaseDamage(float value) { StartCoroutine(DamageCoroutine(value)); }

    private IEnumerator DamageCoroutine(float duration)
    {
        powerUp = true; // ����
        
        yield return new WaitForSeconds(duration);
        
        powerUp = false; // ����
    }
}