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

    private Player P;
    private GameObject weaponCase;

    //--------------------------------------<체력>---------------------------------------------------
    private float MaxHp, CurrentHp, Helmet, Armor;

    //게임 시작시 데이터 불러오는 용도
    private void StartSetting()
    {
        MaxHp = Data.P_MaxHp + (Data.Strength_Level * 10f);
        CurrentHp = Data.P_CurrentHp;
        Helmet = Data.P_HelmetArmor;
        Armor = Data.P_BodyArmor;
    }

    //게임종료시 또는 사망시 사용
    private void UpdateDB()
    {
        Data.P_MaxHp = MaxHp;
        Data.P_CurrentHp = CurrentHp;
        Data.P_HelmetArmor = Helmet;
        Data.P_BodyArmor = Armor;
    }

    //레벨업시 능력치 반영시
    private void UpdateMaxHp() { MaxHp = Data.P_MaxHp + (Data.Strength_Level * 10f); }

    //데미지 입을시 반영
    private void UpdateCurrentHp() { Data.P_CurrentHp = CurrentHp; }

    //--------------------------------------<장비 능력치>-------------------------------------------------
    private void UpdateArmor() 
    {
        Data.P_HelmetArmor = Helmet;
        Data.P_BodyArmor = Armor;
    }

    private float TotalArmor() { return Helmet + Armor; }

    //--------------------------------------<체력 변경>-------------------------------------------------
    [Range(0f,1f)]
    private float ArmorReduction;

    public void ChangeArmorReduction(float amount)
    {
        ArmorReduction = amount;
    }

    public void P_TakeDamage(float damage)
    {
        if((damage - TotalArmor()) * (1 - ArmorReduction) <= 1 ) { CurrentHp -= 1; }
        else { CurrentHp -= (damage - TotalArmor()) * (1 - ArmorReduction); }
        if(CurrentHp <=0) { CurrentHp = 0; IsDead(); UpdateDB(); }
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
    [Range(0f,1f)]
    private float SpeedReduction;

    public void ChangeSpeedReduction(float amount)
    {
        SpeedReduction = amount;
    }

    private void InputSpeed()
    {
        //이동속도 입력
        UpdateSpeed();

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

    //--------------------------------------<능력치 레벨>--------------------------------------------

    private void UpdateStrLevel() { Data.Strength_Level += 1; }
    private void UpdateAgiLevel() { Data.Agility_Level += 1; }
    private void UpdateIntLevel() { Data.Intelligent_Level += 1; }

    //능력치 레벨업 기능
    private void UpdateStats()
    {
        if(Data.Str_Exp >= 100) { UpdateStrLevel(); Data.Str_Exp = 0; UpdateMaxHp(); }
        
        if(Data.Agi_Exp >= 100) { UpdateAgiLevel(); Data.Agi_Exp = 0; UpdateSpeed(); }
        
        if(Data.Int_Exp >= 100) { UpdateIntLevel(); Data.Int_Exp = 0; }
    }

    private void UpdateSpeed() => Speed = Data.P_Speed + (Data.Agility_Level * 0.1f);

    //--------------------------------------<애니메이션>---------------------------------------------
    //플레이어 애니메이터
    private Animator P_Ani;

    //마우스 및 플레이어 위치 변수
    private Vector3 Mouse_Position, P_Position;
    private float dx, dy;

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
        { P_Ani.SetBool("IsMove", true); }
        else
        { P_Ani.SetBool("IsMove", false); }
    }

    private void IsDead() 
    {
        if (CurrentHp <= 0) 
        {
            P_Ani.SetBool("IsDead", true);
            P.enabled = false;
            weaponCase.SetActive(false);
        }
    }


    //------------------------------------------<초기화>-------------------------------------------
    private void Awake()
    {
        //애니메이터 지정
        P_Ani = GetComponent<Animator>();
        P = GetComponent<Player>();
        weaponCase = GameObject.FindWithTag("Weapon Case");

        P.enabled = true;
        weaponCase.SetActive(true);

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
    }

    private void FixedUpdate()
    {
        //플레이어 움직임 변경
        this.transform.Translate(P_XSpeed * Time.deltaTime, P_YSpeed * Time.deltaTime, 0);
    }
}
