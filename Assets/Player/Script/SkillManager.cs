using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillManager : MonoBehaviour
{
    //public field
    #region

    public static SkillManager Instance;

    #endregion

    //private field
    #region

    private Player player;
    private Animator ani;
    private Rigidbody2D rb;

    private Vector3 mousePos, mouseVec;
    private float mx, my;

    #endregion

    //Data Field
    #region

    [Header("Skill Data")]
    [SerializeField]
    private DodgeData dodgedata;
    [SerializeField]
    private MolotovData molotovdata;
    [SerializeField]
    private SiegeModeData siegemodedata;
    [SerializeField]
    private EvdshotData evdshotdata;

    #endregion

    //Get Method
    #region
    private void GetMousePos()
    {
        Vector3 calVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        calVec.z = 0;
        mousePos = calVec;
    }

    private void GetMouseVec()
    {
        mouseVec = (mousePos - transform.position).normalized;
    }
    #endregion

    //회피 ( Dodge )
    #region

    //회피 가능여부 확인
    public bool canDodge;
    //시간 확인용
    private float dodgeCurtime = 0;

    //Public Method
    #region

    /// <summary> 회피스킬 남은 시간 가져오기 </summary>
    /// <returns></returns>
    public float GetDodgeTime()
    {
        if (dodgeCurtime > dodgedata.upgradeList[0].coolTime)
        {
            return 0;
        }
        else
        {
            return dodgedata.upgradeList[0].coolTime - dodgeCurtime;
        }
    }

    #endregion

    //Private Method
    #region

    //회피 시간 체크
    private void DodgeTimeCheck()
    {
        if (dodgeCurtime <= dodgedata.upgradeList[dodgedata.upgradeNum].coolTime)
        {
            dodgeCurtime += Time.deltaTime;
        }
        else
        {
            canDodge = true;
        }
    }

    private IEnumerator Dodge()
    {
        dodgeCurtime = 0; canDodge = false;
        rb.AddForce(new Vector2(mx,my).normalized * 20f, ForceMode2D.Impulse);
        ani.SetBool("IsDash", true);

        yield return new WaitForSeconds(1/6f);

        rb.velocity = Vector3.zero;
        
    }
    #endregion

    #endregion

    //화염병 ( Molotov )
    #region

    //화염병 투척 가능여부 확인
    public bool canThrow;
    //시간 확인용
    private float throwCurtime = 0;

    //Public Method
    #region

    /// <summary> 시즈모드 스킬 남은시간 가져오기 </summary>
    /// <returns></returns>
    public float GetMolotovTime()
    {
        if (throwCurtime >= molotovdata.upgradeList[0].coolTime)
        {
            return 0;
        }
        else
        {
            return molotovdata.upgradeList[0].coolTime - throwCurtime;
        }
    }

    #endregion

    //Private Method
    #region

    //화염병 스킬 쿨타임 체크
    private void ThrowTimeCheck()
    {
        if (throwCurtime < molotovdata.upgradeList[molotovdata.upgradeNum].coolTime)
        {
            throwCurtime += Time.deltaTime;
        }
        else
        {
            canThrow = true;
        }
    }

    //화염병 던지기
    private void ThrowMolotov()
    {
        GameObject Molotov;

        if (Vector3.Distance(mousePos, transform.position) > molotovdata.upgradeList[molotovdata.upgradeNum].throwDistance)
        {

            Molotov = Instantiate(molotovdata.Molotov, transform.position, transform.rotation);
            Molotov.GetComponent<ThrowMolotov>().
                SetCourse(transform.position + (mouseVec * molotovdata.upgradeList[molotovdata.upgradeNum].throwDistance));
            canThrow = false; throwCurtime = 0;
        }
        else
        {
            Molotov = Instantiate(molotovdata.Molotov, transform.position, transform.rotation);
            Molotov.GetComponent<ThrowMolotov>().SetCourse(mousePos);
            canThrow = false; throwCurtime = 0;
        }
    }
    #endregion

    #endregion

    //시즈모드
    #region

    //시즈모드 가능여부 확인
    public bool canSiege;
    //시즈모드 활성화여부 확인
    public bool siegeIsActive = false;
    //시간 확인용
    private float siegeCurtime = 0f;

    //Public Method
    #region

    /// <summary> 시즈모드스킬 남은 시간 가져오기 </summary>
    /// <returns></returns>
    public float GetSiegeTime()
    {
        if (siegeCurtime > siegemodedata.upgradeList[0].coolTime)
        {
            return 0;
        }
        else
        {
            return siegemodedata.upgradeList[0].coolTime - siegeCurtime;
        }
    }

    #endregion

    //Private Method
    #region
    private void SiegeTimeCheck()
    {
        if( siegeCurtime < siegemodedata.upgradeList[siegemodedata.upgradeNum].coolTime && siegeIsActive == false)
        {
            siegeCurtime += Time.deltaTime;
        }
        else if (siegeCurtime >= siegemodedata.upgradeList[siegemodedata.upgradeNum].coolTime)
        {
            canSiege = true;
        }
    }

    private void SiegeMode()
    {
        if (siegeIsActive)
        {
            player.ChangeArmorReduction(0f);
            player.ChangeSpeedReduction(0f);
            siegeIsActive = false;
            canSiege = false;
        }
        else
        {
            player.ChangeArmorReduction(70f);
            player.ChangeSpeedReduction(100f);
            siegeIsActive = true;
            siegeCurtime = 0f;
        }
    }
    #endregion

    #endregion

    //회피사격 ( EvadeShot )
    #region

    //회피사격 가능여부 확인
    public bool canEvdshot = false;
    //시간 확인용
    private float evdshotCurtime = 0f;

    [SerializeField]
    private GameObject evadeshotgun;

    //Public Method

    /// <summary> 회피사격스킬 남은 시간 가져오기 </summary>
    /// <returns></returns>
    public float GetEvdshotTime()
    {
        if( evdshotCurtime > evdshotdata.upgradeList[0].coolTime )
        {
            return 0;
        }
        else
        {
            return evdshotdata.upgradeList[0].coolTime - evdshotCurtime;
        }
    }

    //Private Method
    #region

    private void EvdshotTimeCheck()
    {
        if (evdshotCurtime <= evdshotdata.upgradeList[evdshotdata.upgradeNum].coolTime)
        {
            evdshotCurtime += Time.deltaTime;
        }
        else
        {
            canEvdshot = true;
        }
    }

    /// <summary> 대상에게 데미지 적용 (Collider2D[], float) </summary>
    /// <param name="colliders"></param>
    /// <param name="dmg"></param>
    private void ApplyEvdshotDamage(Collider2D[] colliders, float dmg)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            switch (colliders[i].tag)
            {
                case "Mob":
                    colliders[i].GetComponent<MobHP>().TakeDamage(dmg);
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
        evdshotCurtime = 0f; canEvdshot = false; 
        evadeshotgun.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        //이펙트, 데미지
        if (player.dx >= 0)
        {
            Instantiate(evdshotdata.effect, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(1, 0, 0), 1);
            ApplyEvdshotDamage(col, evdshotdata.upgradeList[evdshotdata.upgradeNum].damage);
            rb.AddForce(Vector2.left * 20f, ForceMode2D.Impulse); // 좌측으로
        }
        else if (player.dx < 0) 
        {
            Instantiate(evdshotdata.effect, this.transform.position + new Vector3(-1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(-1, 0, 0), 1);
            ApplyEvdshotDamage(col, evdshotdata.upgradeList[evdshotdata.upgradeNum].damage);
            rb.AddForce(Vector2.right * 20f, ForceMode2D.Impulse); // 우측으로
        }

        evadeshotgun.SetActive(false);
        //rb.AddForce(mouseVec * -20f, ForceMode2D.Impulse); ( 마우스 반대 방향으로 )
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
    }
    
    #endregion

    #endregion

    //Unity Event
    #region
    private void Awake()
    {
        player = this.GetComponent<Player>();
        ani = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
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

        DodgeTimeCheck();
        if (siegeIsActive == false && canDodge == true )
        {
            if(Input.GetKeyDown(KeyCode.Space) && (mx != 0 || my != 0))
            {
                StartCoroutine(Dodge());
            }
        }

        EvdshotTimeCheck();
        if (siegeIsActive == false && canEvdshot == true)
        {
            if(Input.GetMouseButtonDown(1))
            {
                StartCoroutine(Evdshot());
            }
        }
        ThrowTimeCheck();
        if (canThrow == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ThrowMolotov();
            }
        }

        SiegeTimeCheck();
        if(canSiege == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SiegeMode();
            }
        }
    }
    #endregion
}
