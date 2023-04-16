using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*추가 사항 ---------------------------



----------------------------------------*/

public class W_Rifle : MonoBehaviour
{
    //무기 기본 데이터 저장
    public W_Data Data;

    //-----------------------------------<무기 기능>--------------------------------------------------

    //발사위치 저장
    private GameObject Fire_Position;
    private float CurTime = 0;

    private void Attack()
    {
        if (CurTime <= 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //총알 생성
                GameObject Bullet;
                Bullet = Instantiate(Data.Bullet, Fire_Position.transform.position, Quaternion.Euler(0f, 0f, rotateDegree - 90f));

                //총알 데이터 입력
                Bullet.GetComponent<Bullet>().SetStats(Data.W_Speed,Data.W_Damage,Data.W_Distance);

                //발사시간 초기화
                CurTime = Data.W_AttackSpeed;
            }
        }
        else
        {
            CurTime -= Time.deltaTime;
        }
    }

    //-----------------------------------<애니메이션>--------------------------------------------------

    //회전각도 저장
    private float rotateDegree;

    //마우스 및 플레이어 위치 변수
    private Vector3 Mouse_Position;
    private Vector3 W_Position;

    //플레이어 무기 방향 오브젝트 저장
    private GameObject Hand;

    //방향벡터 계산 함수
    private void CalcVec()
    {
        //마우스 위치와 플레이어 위치 입력
        Mouse_Position = Input.mousePosition;
        W_Position = this.transform.position;

        //마우스의 z값을 카메라 앞으로 위치
        Mouse_Position.z = W_Position.z - Camera.main.transform.position.z;

        //실제 마우스 위치 입력
        Vector3 target = Camera.main.ScreenToWorldPoint(Mouse_Position);

        //마우스 방향 계산
        float dx = target.x - W_Position.x;
        float dy = target.y - W_Position.y;

        rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Hand.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);

        //마우스위치에 따라 좌우 반전
        if (dx < 0f)
        {
            Hand.transform.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            Hand.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //-----------------------------------<초기화>--------------------------------------------------


    private void Awake()
    {
        Hand = GameObject.FindWithTag("Hand");
        Fire_Position = GameObject.FindWithTag("Fire Position");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        CalcVec();
        Attack();
    }
}
