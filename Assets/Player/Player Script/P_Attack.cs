using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//추가사항 ------------------



//---------------------------



public class P_Attack : MonoBehaviour
{

    //마우스 및 플레이어 위치 변수
    private Vector3 Mouse_Position;
    private Vector3 P_Position;

    //플레이어 오브젝트 저장
    private GameObject Player;

    //플레이어 무기 방향 오브젝트 저장
    private GameObject Weapon;

    //플레이어 좌우 반전 변수
    private bool Rev_Flag = false;

    //방향벡터 계산 함수
    private void CalcWatchVec()
    {
        //마우스 위치와 플레이어 위치 입력
        Mouse_Position = Input.mousePosition;
        P_Position = Player.transform.position;

        //마우스의 z값을 카메라 앞으로 위치
        Mouse_Position.z = P_Position.z - Camera.main.transform.position.z;

        //실제 마우스 위치 입력
        Vector3 target = Camera.main.ScreenToWorldPoint(Mouse_Position);

        //마우스 방향 계산
        float dx = target.x - P_Position.x;
        float dy = target.y - P_Position.y;

        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);

        //마우스위치에 따라 좌우 반전
        if(dx< 0)
        {
            Rev_Flag = true;
        }
        else
        {
            Rev_Flag= false;
        }
        Player.GetComponent<SpriteRenderer>().flipX = Rev_Flag;

    }
    
    //시작시 초기화
    private void Start()
    {
        //플레이어와 무기 찾기
        Player = GameObject.FindWithTag("Player");
        Weapon = GameObject.FindWithTag("Weapon");
    }

    //프레임마다
    private void Update()
    {
        //지속적인 방향 계산
        CalcWatchVec();
    }
}
