using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillManager : MonoBehaviour
{
    //Public Field
    #region
    [Header("Public Field")]
    public static SkillManager Instance;

    #endregion

    //Private Field
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
    public DodgeData dodgedata;
    [SerializeField]
    public MolotovData molotovdata;
    [SerializeField]
    public SiegeModeData siegemodedata;
    [SerializeField]
    public EvdshotData evdshotdata;

    #endregion

    //Private Method
    #region

    /// <summary>
    /// ��ų �����͵��� �ʱⰪ ����
    /// </summary>
    private void Init()
    {
        dodgedata.Init();
        molotovdata.Init();
        siegemodedata.AdditionalInit();
        evdshotdata.Init();
    }

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

    //ȸ�� ( Dodge )
    #region

    //Private Method

    //ȸ��
    private IEnumerator Dodge()
    {
        dodgedata.CurTime = 0; dodgedata.CanUse = false;
        rb.AddForce(new Vector2(mx,my).normalized * 20f, ForceMode2D.Impulse);
        ani.SetBool("IsDash", true);

        yield return new WaitForSeconds(1/6f);

        rb.velocity = Vector3.zero;
        
    }

    #endregion

    //ȭ���� ( Molotov )
    #region
    //Private Method

    //ȭ���� ������
    private void ThrowMolotov()
    {
        molotovdata.CanUse = false; molotovdata.CurTime = 0;
        GameObject Molotov;
        if (Vector3.Distance(mousePos, transform.position) > molotovdata.ThrowDistance)
        {

            Molotov = Instantiate(molotovdata.Molotov, transform.position, transform.rotation);
            Molotov TM = Molotov.GetComponent<Molotov>();
            TM.data = molotovdata;
            TM.SetCourse(transform.position + (mouseVec * molotovdata.ThrowDistance));
        }
        else
        {
            Molotov = Instantiate(molotovdata.Molotov, transform.position, transform.rotation);
            Molotov TM = Molotov.GetComponent<Molotov>();
            TM.data = molotovdata;
            TM.SetCourse(mousePos);
        }
    }

    #endregion

    //������ ( SiegeMode )
    #region
    //Private Method
    private void SiegeMode()
    {
        if (siegemodedata.IsActive)
        {
            player.ChangeArmorReduction(0f);
            player.ChangeSpeedReduction(0f);
            siegemodedata.IsActive = false;
            siegemodedata.CanUse = false;
        }
        else
        {
            player.ChangeArmorReduction(siegemodedata.Reductionrate);
            player.ChangeSpeedReduction(siegemodedata.SpeedReduction);
            siegemodedata.IsActive = true;
            siegemodedata.CurTime = 0f;
        }
    }

    #endregion

    //ȸ�ǻ�� ( EvadeShot )
    #region
    [SerializeField]
    private GameObject evadeshotgun;

    /// <summary> ��󿡰� ������ ���� (Collider2D[], float) </summary>
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

    /// <summary> ȸ�ǻ�� ���� </summary>
    /// <returns></returns>
    private IEnumerator Evdshot()
    {
        evdshotdata.CurTime = 0f; evdshotdata.CanUse = false; 
        evadeshotgun.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        //����Ʈ, ������
        if (player.dx >= 0)
        {
            Instantiate(evdshotdata.effect, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(1, 0, 0), 1);
            ApplyEvdshotDamage(col, evdshotdata.Damage);
            rb.AddForce(Vector2.left * 20f, ForceMode2D.Impulse); // ��������
        }
        else if (player.dx < 0) 
        {
            Instantiate(evdshotdata.effect, this.transform.position + new Vector3(-1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(-1, 0, 0), 1);
            ApplyEvdshotDamage(col, evdshotdata.Damage);
            rb.AddForce(Vector2.right * 20f, ForceMode2D.Impulse); // ��������
        }

        evadeshotgun.SetActive(false);
        //rb.AddForce(mouseVec * -20f, ForceMode2D.Impulse); ( ���콺 �ݴ� �������� )
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
    }
    
    #endregion

    //Unity Event
    #region
    private void Awake()
    {
        player = this.GetComponent<Player>();
        ani = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        Instance = this;
    }

    private void Start()
    {
        Init();
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

        dodgedata.TimeCheck();
        if (siegemodedata.IsActive == false && dodgedata.CanUse == true )
        {
            if(Input.GetKeyDown(KeyCode.Space) && (mx != 0 || my != 0))
            {
                StartCoroutine(Dodge());
            }
        }

        molotovdata.TimeCheck();
        if (molotovdata.CanUse == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ThrowMolotov();
            }
        }

        siegemodedata.AdditionalTimeCheck();
        if (siegemodedata.CanUse == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SiegeMode();
            }
        }

        evdshotdata.TimeCheck();
        if (siegemodedata.IsActive == false && evdshotdata.CanUse == true)
        {
            if(Input.GetMouseButtonDown(1))
            {
                StartCoroutine(Evdshot());
            }
        }
        

        
    }
    #endregion
}
