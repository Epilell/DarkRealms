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

    public DodgeData dodgedata;
    public MolotovData molotovdata;
    public SiegeModeData siegemodedata;
    public EvdshotData evdshotdata;

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

    //회피
    #region
    
    private float dodgeTimeCheck = 0;   //시간 체크용
    public bool canDodge;              //회피 가능여부 체크

    //회피 시간 체크
    private void DodgeTimeCheck()
    {
        if (dodgeTimeCheck <= dodgedata.upgradeList[dodgedata.upgradeNum].coolTime)
        {
            dodgeTimeCheck += Time.deltaTime;
        }
        else
        {
            canDodge = true;
        }
    }

    private IEnumerator Dodge()
    {
        dodgeTimeCheck = 0; canDodge = false;
        rb.AddForce(new Vector2(mx,my).normalized * 20f, ForceMode2D.Impulse);
        ani.SetBool("IsDash", true);

        yield return new WaitForSeconds(1/6f);

        rb.velocity = Vector3.zero;
        
    }
    #endregion

    //화염병
    #region
    
    public bool canThrow;
    private float throwCurtime = 0;

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

    //시즈모드
    #region
    
    public bool canSiege;
    public bool siegeIsActive = false;
    private float siegeCurtime = 0f;
    
    
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
            player.ChangeArmorReduction(0.7f);
            player.ChangeSpeedReduction(1f);
            siegeIsActive = true;
            siegeCurtime = 0f;
        }
    }
    #endregion

    //회피사격
    #region

    public bool canEvdshot = false;
    private float evdshotCurtime = 0f;

    [SerializeField]
    private GameObject shotgun;

    private void ApplyDamage(Collider2D[] colliders, float dmg)
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

    private IEnumerator Evdshot()
    {
        evdshotCurtime = 0f; canEvdshot = false; 
        shotgun.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        //이펙트, 데미지
        if (player.dx >= 0)
        {
            Instantiate(evdshotdata.effect, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(1, 0, 0), 1);
            ApplyDamage(col, evdshotdata.upgradeList[evdshotdata.upgradeNum].damage);
            rb.AddForce(Vector2.left * 20f, ForceMode2D.Impulse);
        }
        else if (player.dx < 0) 
        {
            Instantiate(evdshotdata.effect, this.transform.position + new Vector3(-1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(-1, 0, 0), 1);
            ApplyDamage(col, evdshotdata.upgradeList[evdshotdata.upgradeNum].damage);
            rb.AddForce(Vector2.right * 20f, ForceMode2D.Impulse);
        }

        shotgun.SetActive(false);
        //rb.AddForce(mouseVec * -20f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
    }

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
    #endregion

    //설정
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
