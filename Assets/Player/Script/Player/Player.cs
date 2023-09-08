using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

/*추가 사항 ---------------------------



----------------------------------------*/

/// <summary>
/// 플레이어 상태
/// </summary>
public enum PlayerState
{
    Normal, 
    Danger, //남은 체력이 30% 이하
    Dead    //남은 체력이 없음
}

public class Player : MonoBehaviour
{
    //Field
    #region

    //플레이어블 캐릭터 기본 데이터
    [Header("Player Data")]
    public PlayerData data;
    public static Player Instance;

    private Animator ani;
    private GameObject weaponCase;

    #endregion

    //체력
    #region

    public float MaxHP, CurrentHp;
    private PlayerState CurrentPlayerState;

    [Range(0f,100f)]
    public float ArmorReductionBySkill;

    //게임 시작시 데이터 불러오는 용도
    private void UpdateSetting()
    {
        MaxHP = data.maxHP + (data.StrLevel * 10f);
        Speed = data.speed * (1 + (data.AgiLevel * 0.02f));
    }

    /// <summary>
    /// 플레이어에게 들어오는 데미지를 amount%만큼 경감
    /// </summary>
    /// <param name="amount">경감량</param>
    public void ChangeDamageReduction(float amount)
    {
        ArmorReductionBySkill = amount;
    }

    /// <summary>
    /// 플레이어 체력을 damage만큼 제거
    /// </summary>
    /// <param name="damage">받는 데미지</param>
    public void P_TakeDamage(float damage)
    {
        if (!SkillManager.Instance.dodgeTS.isActive && CurrentPlayerState != PlayerState.Dead)
        {
            //받는 데미지가 1이하면 1로
            if (((damage - data.GetArmor()) * (1 - ArmorReductionBySkill/100)) * (1 - data.ArmorMasteryLevel * 0.02f) <= 1) { CurrentHp -= 1; }
            //아닌 경우 그대로
            else { CurrentHp -= ((damage - data.GetArmor()) * (1 - ArmorReductionBySkill/100)) * (1 - data.ArmorMasteryLevel * 0.02f); }

            //피격 이펙트
            UIHitEffect.Instance.IsDamaged();
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

    /// <summary>
    /// 플레이어의 상태를 반환
    /// </summary>
    /// <returns>0 : Normal, 1 : Danger, 2 : Dead</returns>
    public int GetPlayerstate()
    {
        return (int)CurrentPlayerState;
    }

    private void CheckPlayerState()
    {
        //사망시
        if (CurrentHp <= 0)
        {
            CurrentHp = 0; IsDead();
            CurrentPlayerState = PlayerState.Dead;  //플레이어 상태 변경
            StartCoroutine(FindObjectOfType<GameOver>().GameOverCoroutine());
        }
        //체력이 30% 이하일때
        else if (CurrentHp < MaxHP * 0.3f)
        {
            CurrentPlayerState = PlayerState.Danger;
        }
        //체력이 30% 이상일때
        else
        {
            CurrentPlayerState = PlayerState.Normal;
        }
    }

    #endregion

    //이동
    #region
    // 플레이어 이동시 대입할 변수
    private float Speed, P_XSpeed, P_YSpeed;
    [Range(0f,1f)]
    private float SpeedReduction;

    /// <summary>
    /// 플레이어 이동속도를 amount% 만큼 감소
    /// </summary>
    /// <param name="amount">감소량</param>
    public void ChangeSpeedReduction(float amount)
    {
        SpeedReduction = amount;
    }

    private void InputSpeed()
    {

        //좌우 움직임 속도 대입
        if (Input.GetKey(KeyCode.D) && P_XSpeed >= 0)
        {
            P_XSpeed = (Speed) * ((100 - SpeedReduction) / 100);
        }
        else if (Input.GetKey(KeyCode.A) && P_XSpeed <= 0)
        {
            P_XSpeed = -Speed * ((100 - SpeedReduction) / 100);
        }
        else
        { P_XSpeed = 0; }

        //상하 움직임 속도 대입
        if (Input.GetKey(KeyCode.W) && P_YSpeed >= 0)
        {
            P_YSpeed = Speed * ((100 - SpeedReduction) / 100);
        }
        else if (Input.GetKey(KeyCode.S) && P_YSpeed <= 0)
        {
            P_YSpeed = -Speed * ((100 - SpeedReduction) / 100);
        }
        else
        { P_YSpeed = 0; }
    }
    #endregion

    //애니메이션
    #region

    private void CalcVec()
    {
        Vector3 mousePos, playerPos;
        //마우스 위치와 플레이어 위치 입력
        mousePos = Input.mousePosition;
        playerPos = this.transform.position;

        //마우스의 z값을 카메라 앞으로 위치
        mousePos.z = playerPos.z - Camera.main.transform.position.z;

        //실제 마우스 위치 입력
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        float dx = target.x - playerPos.x;

        //마우스위치에 따라 좌우 반전
        //1. 마우스가 좌측에 있을때
        if (dx < 0f)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        //2.마우스가 우측에 있을때
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
            Instance.enabled = false;
            weaponCase.SetActive(false);
        }
    }

    #endregion

    //Unity Event
    #region
    private void Awake()
    {
        //셀프 지정
        Instance = this;

        //애니메이터 지정
        ani = GetComponent<Animator>();

        //무기 지정
        weaponCase = transform.Find("Weapon Case").gameObject;
        weaponCase.SetActive(true);

        Instance.enabled = true;

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
        //애니메이션
        CalcVec();
        IsMove();

        //이동
        InputSpeed();

        //체력
        CheckPlayerState();
    }

    private void FixedUpdate()
    {
        //플레이어 움직임 변경
        this.transform.Translate(P_XSpeed * Time.deltaTime, P_YSpeed * Time.deltaTime, 0);
    }
    #endregion
}
