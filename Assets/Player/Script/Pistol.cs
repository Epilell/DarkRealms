using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{
    public override void Attack()
    {
        float damage = data.upgradeList[data.upgradeNum].damage;
        if (Input.GetMouseButton(0))
        {
            GameObject Bullet;
            Bullet = Instantiate(data.bulletPrefab, firePos.transform.position, Quaternion.Euler(0f, 0f, rotateDeg - 90f));

            Bullet.GetComponent<Bullet>().SetStats(damage);
            curTime = 0f;
        }

    }
}
