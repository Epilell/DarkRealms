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

    //ȸ�� ( Dodge )
    #region

    //ȸ�� ���ɿ��� Ȯ��
    public bool canDodge;
    //�ð� Ȯ�ο�
    private float dodgeCurtime = 0;

    //Public Method
    #region

    /// <summary> ȸ�ǽ�ų ���� �ð� �������� </summary>
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

    //ȸ�� �ð� üũ
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

    //ȭ���� ( Molotov )
    #region

    //ȭ���� ��ô ���ɿ��� Ȯ��
    public bool canThrow;
    //�ð� Ȯ�ο�
    private float throwCurtime = 0;

    //Public Method
    #region

    /// <summary> ������ ��ų �����ð� �������� </summary>
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

    //ȭ���� ��ų ��Ÿ�� üũ
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

    //ȭ���� ������
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

    //������
    #region

    //������ ���ɿ��� Ȯ��
    public bool canSiege;
    //������ Ȱ��ȭ���� Ȯ��
    public bool siegeIsActive = false;
    //�ð� Ȯ�ο�
    private float siegeCurtime = 0f;

    //Public Method
    #region

    /// <summary> �����彺ų ���� �ð� �������� </summary>
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

    //ȸ�ǻ�� ( EvadeShot )
    #region

    //ȸ�ǻ�� ���ɿ��� Ȯ��
    public bool canEvdshot = false;
    //�ð� Ȯ�ο�
    private float evdshotCurtime = 0f;

    [SerializeField]
    private GameObject evadeshotgun;

    //Public Method

    /// <summary> ȸ�ǻ�ݽ�ų ���� �ð� �������� </summary>
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
        evdshotCurtime = 0f; canEvdshot = false; 
        evadeshotgun.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        //����Ʈ, ������
        if (player.dx >= 0)
        {
            Instantiate(evdshotdata.effect, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(1, 0, 0), 1);
            ApplyEvdshotDamage(col, evdshotdata.upgradeList[evdshotdata.upgradeNum].damage);
            rb.AddForce(Vector2.left * 20f, ForceMode2D.Impulse); // ��������
        }
        else if (player.dx < 0) 
        {
            Instantiate(evdshotdata.effect, this.transform.position + new Vector3(-1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(-1, 0, 0), 1);
            ApplyEvdshotDamage(col, evdshotdata.upgradeList[evdshotdata.upgradeNum].damage);
            rb.AddForce(Vector2.right * 20f, ForceMode2D.Impulse); // ��������
        }

        evadeshotgun.SetActive(false);
        //rb.AddForce(mouseVec * -20f, ForceMode2D.Impulse); ( ���콺 �ݴ� �������� )
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
