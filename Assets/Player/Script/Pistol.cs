using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{
    public override void Attack()
    {
        float damage = data.Damage;

        GameObject Bullet;
        Bullet = Instantiate(bullet, firePos.transform.position, Quaternion.Euler(0f, 0f, rotateDeg - 90f));
        Bullet.GetComponent<Bullet>().SetStats(damage);

        curTime = 0f;
    }
}
