using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//추가사항 ------------------



//---------------------------

public class P_Watch : MonoBehaviour
{
    //마우스 및 플레이어 위치 변수
    private Vector3 Mouse_Position;
    private Vector3 P_Position;

    //플레이어 중심 지정
    private GameObject Center;

    //방향벡터 계산 함수
    private void CalcVec()
    {
        //마우스 위치와 플레이어 위치 입력
        Mouse_Position = Input.mousePosition;
        P_Position = this.transform.position;

        //마우스의 z값을 카메라 앞으로 위치
        Mouse_Position.z = P_Position.z - Camera.main.transform.position.z;

        //실제 마우스 위치 입력
        Vector3 target = Camera.main.ScreenToWorldPoint(Mouse_Position);

        //마우스 방향 계산
        float dx = target.x - P_Position.x;
        float dy = target.y - P_Position.y;

        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Center.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);

        //마우스위치에 따라 좌우 반전
        if(dx < 0f)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //-------------------------------------------------------------------------------------------

    //시작시 초기화
    private void Start()
    {
        //플레이어 중심 찾기
        Center = GameObject.FindWithTag("Center");
    }

    //프레임마다
    private void Update()
    {
        //지속적인 방향 계산
        CalcVec();
    }
}
