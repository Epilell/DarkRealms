using System.Collections;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public bool powerUp; // 공증

    public void IncreaseDamage(float value) { StartCoroutine(DamageCoroutine(value)); }

    private IEnumerator DamageCoroutine(float duration)
    {
        powerUp = true; // 적용
        
        yield return new WaitForSeconds(duration);
        
        powerUp = false; // 해제
    }
}