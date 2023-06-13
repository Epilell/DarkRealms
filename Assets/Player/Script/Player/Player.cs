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
    //Public Field
    #region

    //플레이어블 캐릭터 기본 데이터
    [Header("Player Data")]
    public P_Data data;

    #endregion

    //Private Field
    #region

    private Animator ani;
    private Player P;
    private GameObject weaponCase;

    #endregion

    //체력관련
    #region

    public float MaxHP, CurrentHp;
    [Range(0f,1f)]
    public float ArmorReduction;

    //게임 시작시 데이터 불러오는 용도
    private void UpdateSetting()
    {
        MaxHP = data.maxHP + (data.Strlevel * 10f);
        Speed = data.speed + (data.Agilevel * 0.1f);
    }

    /// <summary>
    /// 플레이어에게 들어오는 데미지를 amount%만큼 경감
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeArmorReduction(float amount)
    {
        ArmorReduction = amount/100;
    }

    /// <summary>
    /// 플레이어 체력을 damage만큼 제거
    /// </summary>
    /// <param name="damage"></param>
    public void P_TakeDamage(float damage)
    {
        if((damage - data.GetArmor()) * (1 - ArmorReduction) <= 1 ) { CurrentHp -= 1; }
        else { CurrentHp -= (damage - data.GetArmor()) * (1 - ArmorReduction); }
        if(CurrentHp <=0) { CurrentHp = 0; IsDead(); Debug.Log(ani.GetBool("IsDead"));
            StartCoroutine(FindObjectOfType<GameOver>().GameOverCoroutine());
        }
    }

    /// <summary>
    /// 플레이어 체력을 Amount만큼 회복
    /// </summary>
    /// <param name="amount"></param>
    public void P_Heal(float amount)
    {
        CurrentHp += amount;
        if(CurrentHp > MaxHP) { CurrentHp = MaxHP; }
    }
    #endregion

    //이동관련
    #region
    // 플레이어 이동시 대입할 변수
    private float Speed, P_XSpeed, P_YSpeed;
    [Range(0f,1f)]
    private float SpeedReduction;

    /// <summary>
    /// 플레이어 이동속도를 amount% 만큼 감소
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeSpeedReduction(float amount)
    {
        SpeedReduction = amount/100;
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
        if(data.Strexp >= 100) { data.Strlevel++; }
        
        if(data.Agiexp >= 100) { data.Agilevel++; }
        
        if(data.Intexp >= 100) { data.Intlevel++; }

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
        { ani.SetBool("IsMove", true); }
        else
        { ani.SetBool("IsMove", false); }
    }

    private void IsDead() 
    {
        if (ani.GetBool("IsDead") == false) 
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
        CurrentHp = MaxHP;
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
