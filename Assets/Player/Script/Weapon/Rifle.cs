using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*�߰� ���� ---------------------------



----------------------------------------*/

public class Rifle : WeaponBase
{
    public override void Attack()
    {
        float damage = data.Damage;
        if (FindObjectOfType<AttackEffect>().powerUp) damage = data.Damage * 1.5f;

        //�Ѿ� ����
        GameObject Bullet;
        Bullet = Instantiate(bullet, firePos.transform.position, Quaternion.Euler(0f, 0f, rotateDeg - 90f));

        //�Ѿ� ������ �Է�
        Bullet.GetComponent<Bullet>().SetStats(damage);
        //�߻�ð� �ʱ�ȭ
        curTime = 0f;
    }
}
