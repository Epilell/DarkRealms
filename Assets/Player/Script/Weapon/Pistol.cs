using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{
    public override void Attack()
    {
        float damage = data.Damage;
        if (FindObjectOfType<AttackEffect>().powerUp) damage = data.Damage * 1.5f;

        GameObject Bullet;
        Bullet = Instantiate(bullet, firePos.transform.position, Quaternion.Euler(0f, 0f, rotateDeg - 90f));
        Bullet.GetComponent<Bullet>().SetStats(damage, 0.25f);

        curTime = 0f;
    }
}
