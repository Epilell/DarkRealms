using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{
    public override void Attack()
    {
        float damage = data.Damage;
        if (FindObjectOfType<AttackEffect>().powerUp) damage = data.Damage * 1.5f;
        int pelletNum = data.PelletNum;

        GameObject[] Bullet = new GameObject[pelletNum];
        float deg;
        if (pelletNum % 2 == 0)
        {
            deg = -(((pelletNum - 2) * 5) - 5);
        }
        else if (pelletNum % 2 == 1)
        {
            deg = -((int)(pelletNum / 2) * 5);
        }
        else
        {
            deg = 0;
        }
        //총알 생성
        for (int i = 0; i < pelletNum; i++)
        {
            Bullet[i] = Instantiate(bullet, firePos.transform.position, Quaternion.Euler(0f, 0f, rotateDeg - 90f + deg));

            //총알 데이터 입력
            Bullet[i].GetComponent<Bullet>().SetStats(damage);
            deg += 5f;
        }

        //발사시간 초기화
        curTime = 0f;
    }
}
