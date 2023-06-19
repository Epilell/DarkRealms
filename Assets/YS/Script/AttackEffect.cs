using Rito.InventorySystem;
using System.Collections;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    private WeaponItemData weaponItemData;

    private void Start() { weaponItemData = weaponItemData = new WeaponItemData(); }

    public void IncreaseDamage(float value) { StartCoroutine(DamageCoroutine(value)); }

    private IEnumerator DamageCoroutine(float dutation)
    {
        // get
        float initialDamage = weaponItemData.Damage;

        //set
        //weaponItemData._damage *= 2; // 공증

        yield return new WaitForSeconds(dutation);

        // set
        //weaponItemData._damage = (int)initialDamage; // 원래대로 복구
    }
}