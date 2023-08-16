using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 업그레이드 리스트 요소
/// </summary>
[Serializable]
public struct Upgrade
{
    [SerializeField]private string name;
    public string Name => name;
    [SerializeField]private bool isUpgrade;
    public bool IsUpgrade => isUpgrade;
}

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
    private Collider2D col;

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

    [Header("Skill Upgrade List", order = 2), Space(5)]
    public List<Upgrade> dodgeUpgradeList = new(); public List<Upgrade> molotovUpgradeList = new();
    public List<Upgrade> siegemodeUpgradeList = new(); public List<Upgrade> evdshotUpgradeList = new();

    [Header("Object List", order = 3), Space(5)]
    [SerializeField] private GameObject EvadeShotgun;

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

    private void CheckTime(TimeAndStack _ts , SkillData _data, List<Upgrade> _list)
    {
        if (CheckUpgrade(_list, "Stack Up"))
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
    /// <param name="_list">해당 스킬 업그레이드 리스트</param>
    /// <param name="_upgradeName">업그레이드 이름</param>
    /// <returns></returns>
    public bool CheckUpgrade(List<Upgrade> _list, string _upgradeName)
    {
        for(int i = 0; i < _list.Count; i++)
        {
            if(_list[i].Name == _upgradeName && _list[i].IsUpgrade)
            {
                return true;
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
        if(CheckUpgrade(dodgeUpgradeList, "Trap"))
        {
            Instantiate(dodgeData.GetObj(),transform.position, transform.rotation);
        }
        //회피 거리 업그레이드
        if (CheckUpgrade(dodgeUpgradeList, "RangeUp"))
        {
            rb.AddForce(new Vector2(mx, my).normalized * (20f + dodgeForce), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(mx, my).normalized * 20f, ForceMode2D.Impulse);
        }
        
        ani.SetBool("IsDash", true);

        yield return new WaitForSeconds(1/6f);

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
    private void ThrowMolotov()
    {
        //초기화
        molotovTS.curTime = 0; molotovTS.canUse = false; molotovTS.curStack--;

        //화염병 생성
        GameObject Molotov;
        if (Vector3.Distance(mousePos, transform.position) > molotovData.Distance)
        {
            Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
            Molotov TM = Molotov.GetComponent<Molotov>();
            TM.data = molotovData;

            //데미지 증가 업그레이드
            if(CheckUpgrade(molotovUpgradeList, "Damage Up"))
            {
                TM.AddTempStats(10, 0);
                TM.SetRGB(47, 98, 255);
            }
            //범위 증가 업그레이드
            if(CheckUpgrade(molotovUpgradeList, "Radius Up"))
            {
                TM.AddTempStats(0, 10);
            }
            //투척 개수 증가 업그레이드
            if(CheckUpgrade(molotovUpgradeList, "More Projectile"))
            {
                
            }

            //경로 지정
            TM.SetCourse(transform.position + (mouseVec * molotovData.Distance));
        }
        else
        {
            Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
            Molotov TM = Molotov.GetComponent<Molotov>();
            TM.data = molotovData;

            //데미지 증가 업그레이드
            if (CheckUpgrade(molotovUpgradeList, "Damage Up"))
            {
                TM.AddTempStats(10, 0);
                TM.SetRGB(47, 98, 255);
            }
            //범위 증가 업그레이드
            if (CheckUpgrade(molotovUpgradeList, "Radius Up"))
            {
                TM.AddTempStats(0, 10);
            }
            //투척 개수 증가 업그레이드
            if (CheckUpgrade(molotovUpgradeList, "More Projectile"))
            {
                
            }

            //경로 지정
            TM.SetCourse(mousePos);
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

        //적용 목록

        //사용중 방어력 증가 업그레이드
        if (CheckUpgrade(siegemodeUpgradeList, "Armor Up"))
        {
            Player.Instance.ChangeDamageReduction(siegemodeData.DamageReduction + 20f > 100 ? 100 : siegemodeData.DamageReduction + 20f);
        }
        else
        {
            Player.Instance.ChangeDamageReduction(siegemodeData.DamageReduction);
        }

        //사용중 이동 가능 업그레이드
        if (CheckUpgrade(siegemodeUpgradeList, "Can Move"))
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
        StopCoroutine(ActivateSiegeMode());

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
    /// <param name="colliders">적용 대상</param>
    /// <param name="dmg">적용 데미지</param>
    private void ApplyEvdshotDamage(Collider2D[] colliders, float dmg)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            switch (colliders[i].tag)
            {
                case "Mob":
                    colliders[i].GetComponent<MobHP>().TakeDamage(dmg);
                    if(CheckUpgrade(evdshotUpgradeList, "Armor Break"))
                    {
                        colliders[i].GetComponent<MobHP>().TakeCC("reducedDefense", 2);
                    }
                    if(CheckUpgrade(evdshotUpgradeList, "Stun"))
                    {
                        colliders[i].GetComponent<MobHP>().TakeCC("stun", 1);
                    }
                    break;
                case "BossMob":
                    colliders[i].GetComponent<BossHP>().TakeDamage(dmg);
                    break;
            }
        }
    }

    /// <summary> 회피사격 실행 </summary>
    /// <returns></returns>
    private IEnumerator Evdshot()
    {
        evdshotTS.curTime = 0f; evdshotTS.canUse = false; evdshotTS.curStack--;
        yield return new WaitForSeconds(0.2f);

        //이펙트, 데미지
        // 좌측으로
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x >= Player.Instance.transform.position.x)
        {
            Instantiate(evdshotData.effect, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(1, 0, 0), 1);

            //회피사격 데미지 업그레이드
            if(CheckUpgrade(evdshotUpgradeList, "Damage Up"))
            {
                ApplyEvdshotDamage(col, evdshotData.Damage * 20f);
            }
            else
            {
                ApplyEvdshotDamage(col, evdshotData.Damage);
            }
            

            //회피사격 거리 업그레이드
            if(CheckUpgrade(evdshotUpgradeList, "Range Up"))
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
            Instantiate(evdshotData.effect, this.transform.position + new Vector3(-1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(-1, 0, 0), 1);

            //회피사격 데미지 업그레이드
            //적용
            if (CheckUpgrade(evdshotUpgradeList, "Damage Up"))
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
            if (CheckUpgrade(evdshotUpgradeList, "Range Up"))
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

        if(Instance == null)
        {
            Instance = this;
        }

        //회피
        CheckTime(dodgeTS, dodgeData, dodgeUpgradeList);
        if (siegemodeTS.isActive == false && dodgeTS.canUse == true && isDashing == false)
        {
            if(Input.GetKeyDown(KeyCode.Space) && (mx != 0 || my != 0))
            {
                StartCoroutine(Dodge());
                FindObjectOfType<SoundManager>().PlaySound("Dash");
            }
        }

        //화염병
        CheckTime(molotovTS, molotovData, molotovUpgradeList);
        if (molotovTS.canUse == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ThrowMolotov();
            }
        }

        //시즈모드
        CheckTime(siegemodeTS, siegemodeData, siegemodeUpgradeList);
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
                    StartCoroutine(ActivateSiegeMode());
                }
            }
        }

        //회피사격
        CheckTime(evdshotTS, evdshotData, evdshotUpgradeList);
        if (siegemodeTS.isActive == false && evdshotTS.canUse == true)
        {
            if(Input.GetMouseButtonDown(1))
            {
                StartCoroutine(Evdshot());
                FindObjectOfType<SoundManager>().PlaySound("EvadeShot");
            }
        }
        

        
    }
    #endregion
}
