using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualPistol : DualWeaponBase
{
    public override void Attack()
    {
        float damage = data.Damage;
        //if (FindObjectOfType<AttackEffect>().powerUp) damage = data.Damage * 1.5f;

        if (order == 1)
        {
            GameObject Bullet;
            Bullet = Instantiate(bullet, firePos1.transform.position, Quaternion.Euler(0f, 0f, rotateDeg - 90f));

            Bullet.GetComponent<Bullet>().SetStats(damage);
            order++;
        }
        else
        {
            GameObject Bullet;
            Bullet = Instantiate(bullet, firePos2.transform.position, Quaternion.Euler(0f, 0f, rotateDeg - 90f));

            Bullet.GetComponent<Bullet>().SetStats(damage);
            order--;
        }
        curTime = 0f;
    }
}
