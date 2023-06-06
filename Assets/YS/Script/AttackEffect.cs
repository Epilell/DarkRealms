using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public WeaponItemData weaponItemData;

    public void IncreaseDamage(float value)
    {
        StartCoroutine(DamageCoroutine(value));
    }

    private IEnumerator DamageCoroutine(float dutation)
    {
        //int initialDamage = weaponItemData._damage;

        //weaponItemData._damage *= 2; // 공증

        yield return new WaitForSeconds(dutation);

        //weaponItemData._damage = initialDamage; // 원래대로 복구
    }
}