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
    //public 
    //플레이어블 캐릭터 기본 데이터
    public P_Data data;

    private Animator ani;
    private Player P;
    private GameObject weaponCase;

    //--------------------------------------<체력>---------------------------------------------------
    public float MaxHP, CurrentHp;

    
    //체력관련
    #region
    [Range(0f,1f)]
    private float ArmorReduction;

    //게임 시작시 데이터 불러오는 용도
    private void UpdateSetting()
    {
        MaxHP = data.maxHP + (data.GetStrLevel() * 10f);
        Speed = data.speed + (data.GetAgiLevel() * 0.1f);
    }


    public void ChangeArmorReduction(float amount)
    {
        ArmorReduction = amount;
    }

    public void P_TakeDamage(float damage)
    {
        if((damage - data.GetArmor()) * (1 - ArmorReduction) <= 1 ) { CurrentHp -= 1; }
        else { CurrentHp -= (damage - data.GetArmor()) * (1 - ArmorReduction); }
        if(CurrentHp <=0) { CurrentHp = 0; IsDead();}
    }

    public void P_Heal(float Amount)
    {
        CurrentHp += Amount;
        if(CurrentHp > MaxHP) { CurrentHp = MaxHP; }
    }
    #endregion

    //이동관련
    #region
    // 플레이어 이동시 대입할 변수
    private float Speed, P_XSpeed, P_YSpeed;
    [Range(0f,1f)]
    private float SpeedReduction;

    public void ChangeSpeedReduction(float amount)
    {
        SpeedReduction = amount;
    }

    private void InputSpeed()
    {

        //좌우 움직임 속도 대입
        if (Input.GetKey(KeyCode.D) && P_XSpeed >= 0)
        {
            if(dx >= 0)
            { P_XSpeed = (Speed) * (1 - SpeedReduction); }
            else
            { P_XSpeed = (float)(Speed * 0.9) * (1 - SpeedReduction); }
        }
        else if (Input.GetKey(KeyCode.A) && P_XSpeed <= 0)
        {
            if(dx < 0)
            { P_XSpeed = -Speed * (1 - SpeedReduction); }
            else
            { P_XSpeed = -(float)(Speed * 0.9) * (1 - SpeedReduction); }
        }
        else
        { P_XSpeed = 0; }

        //상하 움직임 속도 대입
        if (Input.GetKey(KeyCode.W) && P_YSpeed >= 0)
        {
            if(dy >= 0)
            { P_YSpeed = Speed * (1 - SpeedReduction); }
            else
            { P_YSpeed = (float)(Speed * 0.9) * (1 - SpeedReduction); }
        }
        else if (Input.GetKey(KeyCode.S) && P_YSpeed <= 0)
        {
            if(dy < 0)
            { P_YSpeed = -Speed * (1 - SpeedReduction); }
            else
            { P_YSpeed = -(float)(Speed * 0.9) * (1 - SpeedReduction); }
        }
        else
        { P_YSpeed = 0; }
    }
    #endregion

    //능력치 레벨
    #region

    //능력치 레벨업 기능
    private void UpdateStats()
    {
        if(data.Str_Exp >= 100) { data.StrLevelUP(); }
        
        if(data.Agi_Exp >= 100) { data.AgiLevelUP(); }
        
        if(data.Int_Exp >= 100) { data.IntLevelUP(); }

        UpdateSetting();
    }
    #endregion

    //애니메이션
    #region
    //마우스 및 플레이어 위치 변수
    private Vector3 Mouse_Position, P_Position;
    [HideInInspector]
    public float dx, dy;

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
        dx = target.x - P_Position.x;
        dy = target.y - P_Position.y;

        //마우스위치에 따라 좌우 반전
        if (dx < 0f)
        { 
            //this.transform.localScale = new Vector3(-1, 1, 1);
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        { 
            //this.transform.localScale = new Vector3(1, 1, 1);
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    //캐릭터가 움직이는지
    private void IsMove()
    {
        if (P_XSpeed != 0f || P_YSpeed != 0f)
        { ani.SetBool("IsMove", true); }
        else
        { ani.SetBool("IsMove", false); }
    }

    private void IsDead() 
    {
        if (CurrentHp <= 0) 
        {
            ani.SetBool("IsDead", true);
            P.enabled = false;
            weaponCase.SetActive(false);
        }
    }

    #endregion

    //Unity Event
    #region
    private void Awake()
    {
        //애니메이터 지정
        ani = GetComponent<Animator>();
        P = GetComponent<Player>();
        weaponCase = transform.Find("Weapon Case").gameObject;
        

        P.enabled = true;
        weaponCase.SetActive(true);

        UpdateSetting();

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
    }

    private void FixedUpdate()
    {
        //플레이어 움직임 변경
        this.transform.Translate(P_XSpeed * Time.deltaTime, P_YSpeed * Time.deltaTime, 0);
    }
    #endregion
}
