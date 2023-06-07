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
        // get
        float initialDamage = weaponItemData.Damage;

        //set
        //weaponItemData._damage *= 2; // 공증

        yield return new WaitForSeconds(dutation);

        // set
        //weaponItemData._damage = initialDamage; // 원래대로 복구
    }
}