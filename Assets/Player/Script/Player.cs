using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal;
using UnityEngine;

/*추가 사항 ---------------------------



----------------------------------------*/

public class Player : MonoBehaviour
{
    //플레이어블 캐릭터 기본 데이터
    public P_Data Data;

    //--------------------------------------<체력>---------------------------------------------------
    private float MaxHp, CurrentHp;

    //게임 시작시 데이터 불러오는 용도
    private void StartSetting()
    {
        MaxHp = Data.P_MaxHp + (Data.Strength_Level * 10f);
        CurrentHp = Data.P_CurrentHp;
    }

    //게임종료시 또는 사망시 사용
    private void UpdateDB()
    {
        Data.P_MaxHp = MaxHp;
        Data.P_CurrentHp = CurrentHp;
    }

    //레벨업시 능력치 반영시
    private void UpdateMaxHp() { MaxHp = Data.P_MaxHp + (Data.Strength_Level * 10f); }

    //데미지 입을시 반영
    private void UpdateCurrentHp() { Data.P_CurrentHp = CurrentHp; }

    public void P_TakeDamage(float damage)
    {
        CurrentHp -= damage;
        if(CurrentHp <=0) { CurrentHp = 0; }
        UpdateCurrentHp();
    }

    public void P_Heal(float Amount)
    {
        CurrentHp += Amount;
        if(CurrentHp > MaxHp) { CurrentHp = MaxHp; }
        UpdateCurrentHp();
    }

    //--------------------------------------<이동속도>-------------------------------------------------
    // 플레이어 이동시 대입할 변수
    private float Speed, P_XSpeed, P_YSpeed;

    private void UpdateSpeed() => Speed = Data.P_Speed + (Data.Agility_Level * 0.1f);

    private void UpSpeed()
    {
        Data.P_Speed += 1;
    }

    private void InputSpeed()
    {
        //이동속도 입력
        UpdateSpeed();

        //상하 움직임 속도 대입
        if (Input.GetKey(KeyCode.W) && P_YSpeed >= 0)
        {
            P_YSpeed = Speed;
        }
        else if (Input.GetKey(KeyCode.S) && P_YSpeed <= 0)
        {
            P_YSpeed = -Speed;
        }
        else
        {
            P_YSpeed = 0;
        }

        //좌우 움직임 속도 대입
        if (Input.GetKey(KeyCode.D) && P_XSpeed >= 0)
        {
            P_XSpeed = Speed;
        }
        else if (Input.GetKey(KeyCode.A) && P_XSpeed <= 0)
        {
            P_XSpeed = -Speed;
        }
        else
        {
            P_XSpeed = 0;
        }
    }

    //--------------------------------------<능력치 레벨>--------------------------------------------

    private void UpdateStrLevel() { Data.Strength_Level += 1; }
    private void UpdateAgiLevel() { Data.Agility_Level += 1; }
    private void UpdateIntLevel() { Data.Intelligent_Level += 1; }

    //능력치 레벨업 기능
    private void UpdateStats()
    {
        if(Data.Str_Exp >= 100) { UpdateStrLevel(); Data.Str_Exp = 0; }
        UpdateMaxHp();
        if(Data.Agi_Exp >= 100) { UpdateAgiLevel(); Data.Agi_Exp = 0; }
        UpdateSpeed();
        if(Data.Int_Exp >= 100) { UpdateIntLevel(); Data.Int_Exp = 0; }
    }

    //--------------------------------------<애니메이션>---------------------------------------------
    //플레이어 애니메이터
    private Animator P_Ani;

    //마우스 및 플레이어 위치 변수
    private Vector3 Mouse_Position, P_Position;

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

        //마우스위치에 따라 좌우 반전
        if (dx < 0f)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //캐릭터가 움직이는지
    private void IsMove()
    {
        if (P_XSpeed != 0f || P_YSpeed != 0f)
        {
            P_Ani.SetBool("IsMove", true);
        }
        else
        {
            P_Ani.SetBool("IsMove", false);
        }
    }

    //------------------------------------------<초기화>-------------------------------------------
    private void Awake()
    {
        //애니메이터 지정
        P_Ani = GetComponent<Animator>();

        StartSetting();

        //플레이어 중력 및 축 회전 제외
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;

    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //<애니메이션 관련>
        CalcVec();
        IsMove();

        //<이동 관련>
        InputSpeed();

        //<능력치 관련>
        UpdateStats();

        if (Input.GetKey(KeyCode.V))
        {
            UpSpeed();
        }
    }

    private void FixedUpdate()
    {
        //플레이어 움직임 변경
        this.transform.Translate(P_XSpeed * Time.deltaTime, P_YSpeed * Time.deltaTime, 0);
    }
}
