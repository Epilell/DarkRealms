using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 시간 및 스택 체크
/// </summary>
public class TimeAndStack
{
    //현재 쿨타임
    public float curTime = 0;
    //현재 스택
    public int curStack = 0;
    //임시 스택 증가
    public int temporalStack = 0;
    //사용 가능 여부
    public bool canUse = false;
    //  *시즈모드 전용*
    public bool isActive = false;
}

public class SkillManager : MonoBehaviour
{
    //Field
    #region
    [Header("Public Field")]
    public static SkillManager Instance;

    private Animator ani;
    private Rigidbody2D rb;

    //마우스 위치, 방향벡터
    private Vector3 mousePos, mouseVec;

    //플레이어 이동 방향 값
    private float mx, my;

    //Data
    [Header("Skill Data", order = 1), Space(5)]
    public DodgeData dodgeData; public MolotovData molotovData;
    public SiegeModeData siegemodeData; public EvdshotData evdshotData;

    //Time And Stack
    public TimeAndStack dodgeTS = new(); public TimeAndStack molotovTS = new();
    public TimeAndStack siegemodeTS = new(); public TimeAndStack evdshotTS = new();

    //스킬 업그레이드 리스트
    [Header("Skill Upgrade List", order = 2), Space(5)]
    public SkillUpgradeList SkillUpgradeData;

    //스킬 히트박스 리스트
    [Header("Hitbox List", order = 3), Space(5)]
    public Transform EvadeshotHitbox;
    public Vector2 EvadeshotHitboxSize;

    private Coroutine siegeModeCoroutine; // 시즈모드 수정용
    public bool isSkillCanUse = true; // 인벤, 옵션창 전용 추가

    #endregion

    //Get Method
    #region
    /// <summary>
    /// 마우스의 위치
    /// </summary>
    private void GetMousePos()
    {
        Vector3 calVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        calVec.z = 0;
        mousePos = calVec;
    }

    /// <summary>
    /// 플레이어와 마우스위치의 방향벡터
    /// </summary>
    private void GetMouseVec()
    {
        mouseVec = (mousePos - transform.position).normalized;
    }
    #endregion

    //Check Method
    #region

    private void CheckTime(TimeAndStack _ts, SkillData _data, String _skillName)
    {
        if (CheckUpgrade(_skillName, "Stack Up"))
        {
            _ts.temporalStack = 1;
        }
        if (_ts.isActive == false)
        {
            if (_ts.curStack < _data.MaxStack + _ts.temporalStack)
            {
                if (_ts.curTime < _data.CoolTime * (1 - Player.Instance.data.IntLevel * 0.02f))
                {
                    _ts.curTime += Time.deltaTime;
                }
                else
                {
                    _ts.curStack++;
                    _ts.curTime = 0;
                }
            }
            if (_ts.curStack > 0)
            {
                _ts.canUse = true;
            }
            else
            {
                _ts.canUse = false;
            }
        }
    }

    /// <summary>
    /// 해당 스킬에 특정 업그레이드를 했는지 확인
    /// </summary>
    /// <param name="_skillName">Dodge, Molotov, Siege Mode, Evade Shot</param>
    /// <param name="_upgradeName">업그레이드 이름</param>
    /// <returns></returns>
    public bool CheckUpgrade(string _skillName, string _upgradeName)
    {
        foreach(SkillList _target in SkillUpgradeData.ApplyUpgradeList)
        {
            if(_target.SkillName == _skillName)
            {
                SkillUpgrade upgrade = _target.UpgradeList.Find(u => u.Name == _upgradeName);
                if(upgrade != null)
                {
                    return upgrade.IsUpgrade;
                }
            }
        }
        return false;
    }

    #endregion

    //스킬 리스트
    #region

    //회피 ( Dodge ) , dodgeTS
    #region

    private float dodgeForce = 20f;
    private bool isDashing = false;

    //회피
    private IEnumerator Dodge()
    {
        //초기화
        dodgeTS.curTime = 0; dodgeTS.canUse = false; dodgeTS.curStack--;
        isDashing = true;

        //회피중 무적
        //col.enabled = false;

        //회피 후 트랩설치
        if (CheckUpgrade("Dodge", "Trap"))
        {
            Instantiate(dodgeData.GetObj(), transform.position, transform.rotation);
        }
        //회피 거리 업그레이드
        if (CheckUpgrade("Dodge", "Range Up"))
        {
            rb.AddForce(new Vector2(mx, my).normalized * (20f + dodgeForce), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(mx, my).normalized * 20f, ForceMode2D.Impulse);
        }

        //애니메이션 재생
        ani.SetTrigger("IsDash");

        yield return new WaitForSeconds(1 / 6f);

        //회피후 초기화
        rb.velocity = Vector3.zero;
        isDashing = false;

        //회피후 무적 해제
        //col.enabled = true;
    }

    #endregion

    //화염병 ( Molotov ) , molotovTS
    #region

    //화염병 던지기
    private void ThrowMolotov(Vector3 _pos, Vector3 _vec)
    {
        //초기화
        molotovTS.curTime = 0; molotovTS.canUse = false; molotovTS.curStack--;

        //화염병 생성
        GameObject Molotov;

        //최대 투척 사거리 일때
        if (Vector3.Distance(_pos, transform.position) > molotovData.Distance)
        {
            //투척 개수 업그레이드 O
            if(CheckUpgrade("Molotov", "More Projectile"))
            {
                for (int i = 0; i < 3; i++)
                {
                    Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
                    Molotov TM = Molotov.GetComponent<Molotov>();
                    TM.data = molotovData;

                    //데미지 증가 업그레이드
                    if (CheckUpgrade("Molotov", "Damage Up"))
                    {
                        TM.AddTempStats(10, 0);
                        TM.SetColor(new(73/255f, 122/255f, 231/255f, 255/255f));
                    }

                    //범위 증가 업그레이드
                    if (CheckUpgrade("Molotov", "Radius Up"))
                    {
                        TM.AddTempStats(0, 10);
                    }

                    //경로 지정
                    TM.SetCourse(transform.position + ((_vec * molotovData.Distance) + _vec * (1 + 2 * i)));
                }
            }
            //투척 개수 업그레이드 X
            else
            {
                Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
                Molotov TM = Molotov.GetComponent<Molotov>();
                TM.data = molotovData;

                //데미지 증가 업그레이드
                if (CheckUpgrade("Molotov", "Damage Up"))
                {
                    TM.AddTempStats(10, 0);
                    TM.SetColor(new(73 / 255f, 122 / 255f, 231 / 255f, 255 / 255f));
                }

                //범위 증가 업그레이드
                if (CheckUpgrade("Molotov", "Radius Up"))
                {
                    TM.AddTempStats(0, 10);
                }

                //경로 지정
                TM.SetCourse(transform.position + (_vec * molotovData.Distance));
            }
            
        }
        //마우스 위치에 투척할 때
        else
        {
            //투척 개수 업그레이드 O
            if (CheckUpgrade("Molotov", "More Projectile"))
            {
                for(int i = 0; i < 3; i++)
                {
                    Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
                    Molotov TM = Molotov.GetComponent<Molotov>();
                    TM.data = molotovData;

                    //데미지 증가 업그레이드
                    if (CheckUpgrade("Molotov", "Damage Up"))
                    {
                        TM.AddTempStats(10, 0);
                        TM.SetColor(new(73 / 255f, 122 / 255f, 231 / 255f, 255 / 255f));
                    }

                    //범위 증가 업그레이드
                    if (CheckUpgrade("Molotov", "Radius Up"))
                    {
                        TM.AddTempStats(0, 10);
                    }

                    //경로 지정
                    TM.SetCourse(_pos + _vec * (1 + 2 * i));
                }
            }
            //투척 개수 업그레이드 X
            else
            {
                Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
                Molotov TM = Molotov.GetComponent<Molotov>();
                TM.data = molotovData;

                //데미지 증가 업그레이드
                if (CheckUpgrade("Molotov", "Damage Up"))
                {
                    TM.AddTempStats(10, 0);
                    TM.SetColor(new(73 / 255f, 122 / 255f, 231 / 255f, 255 / 255f));
                }

                //범위 증가 업그레이드
                if (CheckUpgrade("Molotov", "Radius Up"))
                {
                    TM.AddTempStats(0, 10);
                }

                //경로 지정
                TM.SetCourse(_pos);
            }
        }
    }

    #endregion

    //시즈모드 ( SiegeMode ) , siegemodeTS
    #region

    private float siegemodeDuration = 5f;
    
    //시즈모드 활성화
    private IEnumerator ActivateSiegeMode()
    {
        //초기화
        siegemodeTS.isActive = true; siegemodeTS.curTime = 0f; siegemodeTS.curStack--;

        FindObjectOfType<CoolDown>().siegeActive = true;
        StartCoroutine(FindObjectOfType<CoolDown>().SiegeCool(siegemodeDuration));

        //적용 목록

        //사용중 방어력 증가 업그레이드
        if (CheckUpgrade("Siege Mode", "Armor Up"))
        {
            Player.Instance.ChangeDamageReduction(siegemodeData.DamageReduction + 20f > 100 ? 100 : siegemodeData.DamageReduction + 20f);
        }
        else
        {
            Player.Instance.ChangeDamageReduction(siegemodeData.DamageReduction);
        }

        //사용중 이동 가능 업그레이드
        if (CheckUpgrade("Siege Mode", "Can Move"))
        {
            Player.Instance.ChangeSpeedReduction(siegemodeData.SpeedReduction - 20 < 0 ? 0 : siegemodeData.SpeedReduction - 20);
        }
        else
        {
            Player.Instance.ChangeSpeedReduction(siegemodeData.SpeedReduction);
        }

        yield return new WaitForSecondsRealtime(siegemodeDuration);

        StartCoroutine(DeactivateSiegeMode());
        yield return null;
    }

    //시즈모드 비활성화
    private IEnumerator DeactivateSiegeMode()
    {
        if (siegeModeCoroutine != null)
        {
            StopCoroutine(siegeModeCoroutine);
            siegeModeCoroutine = null;
        }

        FindObjectOfType<CoolDown>().siegeActive = false;

        //적용 목록
        Player.Instance.ChangeDamageReduction(0f); Player.Instance.ChangeSpeedReduction(0f);

        //초기화
        siegemodeTS.isActive = false; siegemodeTS.canUse = false;
        
        yield return null;
    }

    #endregion

    //회피사격 ( EvadeShot ) , evdshotTS
    #region

    /// <summary> 대상에게 데미지 적용 (Collider2D[], float) </summary>
    /// <param name="col">적용 대상</param>
    /// <param name="dmg">적용 데미지</param>
    private void ApplyEvdshotDamage(Collider2D[] col, float dmg)
    {
        foreach (Collider2D target in col)
        {
            switch (target.tag)
            {
                case "Mob":
                    target.GetComponent<MobHP>().TakeDamage(dmg);
                    if (CheckUpgrade("Evade Shot", "Armor Break"))
                    {
                        target.GetComponent<MobHP>().TakeCC("reducedDefense", 2);
                    }
                    if (CheckUpgrade("Evade Shot", "Stun"))
                    {
                        target.GetComponent<MobHP>().TakeCC("stun", 1);
                    }
                    break;
                case "BossMob":
                    target.GetComponent<BossHP>().TakeDamage(dmg);
                    break;
            }
        }
    }

    /// <summary> 회피사격 실행 </summary>
    /// <returns></returns>
    private IEnumerator Evdshot()
    {
        evdshotTS.curTime = 0f; evdshotTS.canUse = false; evdshotTS.curStack--;

        ani.SetTrigger("IsEvadeshot");

        yield return new WaitForSecondsRealtime(0.5f);
        FindObjectOfType<SoundManager>().PlaySound("EvadeShot");

        //이펙트, 데미지
        // 좌측으로
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x >= Player.Instance.transform.position.x)
        {
            Instantiate(evdshotData.Effect, EvadeshotHitbox.position , EvadeshotHitbox.rotation);
            Collider2D[] col = Physics2D.OverlapBoxAll(EvadeshotHitbox.position, EvadeshotHitboxSize, 0);

            //회피사격 데미지 업그레이드
            //적용
            if (CheckUpgrade("Evade Shot", "Damage Up"))
            {
                ApplyEvdshotDamage(col, evdshotData.Damage * 20f);
            }
            //미적용
            else
            {
                ApplyEvdshotDamage(col, evdshotData.Damage);
            }


            //회피사격 이동거리 업그레이드
            if (CheckUpgrade("Evade Shot", "Range Up"))
            {
                rb.AddForce(Vector2.left * 30f, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.left * 20f, ForceMode2D.Impulse);
            }
        }
        // 우측으로
        else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < Player.Instance.transform.position.x)
        {
            GameObject efc = Instantiate(evdshotData.Effect, EvadeshotHitbox.position, EvadeshotHitbox.rotation);
            efc.GetComponent<SpriteRenderer>().flipX = true;
            Collider2D[] col = Physics2D.OverlapBoxAll(EvadeshotHitbox.position, EvadeshotHitboxSize, 0);

            //회피사격 데미지 업그레이드
            //적용
            if (CheckUpgrade("Evade Shot", "Damage Up"))
            {
                ApplyEvdshotDamage(col, evdshotData.Damage * 20f);
            }
            //미적용
            else
            {
                ApplyEvdshotDamage(col, evdshotData.Damage);
            }

            //회피사격 이동 거리 업그레이드
            //적용
            if (CheckUpgrade("Evade Shot", "Range Up"))
            {
                rb.AddForce(Vector2.right * 30f, ForceMode2D.Impulse);
            }
            //미적용
            else
            {
                rb.AddForce(Vector2.right * 20f, ForceMode2D.Impulse);
            }
        }

        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
    }

    #endregion

    #endregion

    //Unity Event
    #region
    private void Awake()
    {
        ani = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        Instance = this;
    }

    private void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");

        GetMousePos();
        GetMouseVec();

        if (Instance == null)
        {
            Instance = this;
        }

        if (isSkillCanUse&& !FindObjectOfType<GameOver>().isGameOver)
        {
            //회피
            CheckTime(dodgeTS, dodgeData, "Dodge");
            if (siegemodeTS.isActive == false && dodgeTS.canUse == true && isDashing == false)
            {
                if (Input.GetKeyDown(KeyCode.Space) && (mx != 0 || my != 0))
                {
                    StartCoroutine(Dodge());
                    FindObjectOfType<CoolDown>().dodgeActive = true;
                    FindObjectOfType<SoundManager>().PlaySound("Dash");
                }
            }

            //화염병
            CheckTime(molotovTS, molotovData, "Molotov");
            if (molotovTS.canUse == true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ThrowMolotov(mousePos, mouseVec);
                    FindObjectOfType<CoolDown>().molotovActive = true;
                }
            }

            //시즈모드
            CheckTime(siegemodeTS, siegemodeData, "Siege Mode");
            if (siegemodeTS.canUse == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (siegemodeTS.isActive)
                    {
                        StartCoroutine(DeactivateSiegeMode());
                    }
                    else
                    {
                        siegeModeCoroutine = StartCoroutine(ActivateSiegeMode());
                        FindObjectOfType<CoolDown>().siegeCoolDown = true;
                    }
                }
            }

            //회피사격
            CheckTime(evdshotTS, evdshotData, "Evade Shot");
            if (siegemodeTS.isActive == false && evdshotTS.canUse == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    StartCoroutine(Evdshot());
                    FindObjectOfType<CoolDown>().evdshotActive = true;
                }
            }
        }
        else { }
    }

    //스킬 히트박스 영역 생성
    private void OnDrawGizmos()
    {
        //회피사격 히트박스
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(EvadeshotHitbox.position, EvadeshotHitboxSize);
    }
    #endregion
}