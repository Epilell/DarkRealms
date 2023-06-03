using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*추가 사항 ---------------------------



----------------------------------------*/

public class Rifle : WeaponBase
{
    //-----------------------------------<무기 기능>--------------------------------------------------

    public override void Attack()
    {
        float damage = data.upgradeList[data.upgradeNum].damage;

        //총알 생성
        GameObject Bullet;
        Bullet = Instantiate(data.bulletPrefab, firePos.transform.position, Quaternion.Euler(0f, 0f, rotateDeg - 90f));

        //총알 데이터 입력
        Bullet.GetComponent<Bullet>().SetStats(damage);
        //발사시간 초기화
        curTime = 0f;
    }
}
