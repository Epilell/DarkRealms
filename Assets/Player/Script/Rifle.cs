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
        int pelletNum = data.upgradeList[data.upgradeNum].pelletNum;
        if (Input.GetMouseButton(0) && skill.siegeIsActive == false)
        {
            //총알 생성
            GameObject Bullet1;
            Bullet1 = Instantiate(data.bulletPrefab, firePos.transform.position, Quaternion.Euler(0f, 0f, rotateDeg - 90f));

            //총알 데이터 입력
            Bullet1.GetComponent<Bullet>().SetStats(damage);
            //발사시간 초기화
            curTime = 0f;
        }
        else if (Input.GetMouseButton(0) && skill.siegeIsActive == true)
        {
            GameObject[] Bullet2 = new GameObject[data.upgradeList[data.upgradeNum].pelletNum];
            float deg;
            if (pelletNum % 2 == 0)
            {
                deg = -(((pelletNum - 2) * 10) - 5);
            }
            else if (pelletNum % 2 == 1)
            {
                deg = -((int)(pelletNum / 2) * 10);
            }
            else
            {
                deg = 0;
            }
            //총알 생성
            for (int i = 0; i < pelletNum; i++)
            {
                Bullet2[i] = Instantiate(data.bulletPrefab, firePos.transform.position, Quaternion.Euler(0f, 0f, rotateDeg - 90f + deg));

                //총알 데이터 입력
                Bullet2[i].GetComponent<Bullet>().SetStats(damage);
                deg += 10f;
            }
            //발사시간 초기화
            curTime = 0f;
        }
    }
}
