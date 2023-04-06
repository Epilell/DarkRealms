using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//추가 사항 -----------



//----------------------

public class P_Move : MonoBehaviour
{
    // 플레이어 이동속도 변수
    [SerializeField] private float P_Speed = 5;

    // 플레이어 이동속도 제한 변수
    private float Max_P_Speed = 10;
    private float Min_P_Speed = 2;

    // 플레이어 이동시 대입할 변수
    private float P_XSpeed = 0;
    private float P_YSpeed = 0;

    

    // 속도 증가 (아이템에서 참조하여 속도변경)
    public void Inc_Speed(float num)
    {
        P_Speed += num;
        if( P_Speed > Max_P_Speed)
        {
            P_Speed = Max_P_Speed;
        }
    }

    // 속도 감소
    public void Dec_Speed(float num)
    {
        P_Speed -= num;
        if (P_Speed < Min_P_Speed)
        {
            P_Speed = Min_P_Speed;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //플레이어 중력 및 축 회전 제외
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        //상하 움직임 속도 대입
        if (Input.GetKey(KeyCode.W) && P_YSpeed >= 0)
        {
            P_YSpeed = P_Speed;
        }
        else if (Input.GetKey(KeyCode.S) && P_YSpeed <= 0)
        {
            P_YSpeed = -P_Speed;
        }
        else
        {
            P_YSpeed = 0;
        }

        //좌우 움직임 속도 대입
        if (Input.GetKey(KeyCode.D) && P_XSpeed >= 0)
        {
            P_XSpeed = P_Speed;
        }
        else if (Input.GetKey(KeyCode.A) && P_XSpeed <= 0)
        {
            P_XSpeed = -P_Speed;
        }
        else
        {
            P_XSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        //플레이어 움직임 변경
        this.transform.Translate(P_XSpeed * Time.deltaTime, P_YSpeed * Time.deltaTime, 0);
    }
}
