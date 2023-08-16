using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ���׷��̵� ����Ʈ ���
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
/// �ð� �� ���� üũ
/// </summary>
public class TimeAndStack
{
    //���� ��Ÿ��
    public float curTime = 0;
    //���� ����
    public int curStack = 0;
    //�ӽ� ���� ����
    public int temporalStack = 0;
    //��� ���� ����
    public bool canUse = false;
    //  *������ ����*
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

    //�÷��̾� �̵� ���� ��
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
    /// ���콺�� ��ġ
    /// </summary>
    private void GetMousePos()
    {
        Vector3 calVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        calVec.z = 0;
        mousePos = calVec;
    }

    /// <summary>
    /// �÷��̾�� ���콺��ġ�� ���⺤��
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
    /// �ش� ��ų�� Ư�� ���׷��̵带 �ߴ��� Ȯ��
    /// </summary>
    /// <param name="_list">�ش� ��ų ���׷��̵� ����Ʈ</param>
    /// <param name="_upgradeName">���׷��̵� �̸�</param>
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

    //��ų ����Ʈ
    #region

    //ȸ�� ( Dodge ) , dodgeTS
    #region

    private float dodgeForce = 20f;
    private bool isDashing = false;

    //ȸ��
    private IEnumerator Dodge()
    {
        //�ʱ�ȭ
        dodgeTS.curTime = 0; dodgeTS.canUse = false; dodgeTS.curStack--;
        isDashing = true; 

        //ȸ���� ����
        //col.enabled = false;

        //ȸ�� �� Ʈ����ġ
        if(CheckUpgrade(dodgeUpgradeList, "Trap"))
        {
            Instantiate(dodgeData.GetObj(),transform.position, transform.rotation);
        }
        //ȸ�� �Ÿ� ���׷��̵�
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

        //ȸ���� �ʱ�ȭ
        rb.velocity = Vector3.zero;
        isDashing = false;

        //ȸ���� ���� ����
        //col.enabled = true;
    }

    #endregion

    //ȭ���� ( Molotov ) , molotovTS
    #region

    //ȭ���� ������
    private void ThrowMolotov()
    {
        //�ʱ�ȭ
        molotovTS.curTime = 0; molotovTS.canUse = false; molotovTS.curStack--;

        //ȭ���� ����
        GameObject Molotov;
        if (Vector3.Distance(mousePos, transform.position) > molotovData.Distance)
        {
            Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
            Molotov TM = Molotov.GetComponent<Molotov>();
            TM.data = molotovData;

            //������ ���� ���׷��̵�
            if(CheckUpgrade(molotovUpgradeList, "Damage Up"))
            {
                TM.AddTempStats(10, 0);
                TM.SetRGB(47, 98, 255);
            }
            //���� ���� ���׷��̵�
            if(CheckUpgrade(molotovUpgradeList, "Radius Up"))
            {
                TM.AddTempStats(0, 10);
            }
            //��ô ���� ���� ���׷��̵�
            if(CheckUpgrade(molotovUpgradeList, "More Projectile"))
            {
                
            }

            //��� ����
            TM.SetCourse(transform.position + (mouseVec * molotovData.Distance));
        }
        else
        {
            Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
            Molotov TM = Molotov.GetComponent<Molotov>();
            TM.data = molotovData;

            //������ ���� ���׷��̵�
            if (CheckUpgrade(molotovUpgradeList, "Damage Up"))
            {
                TM.AddTempStats(10, 0);
                TM.SetRGB(47, 98, 255);
            }
            //���� ���� ���׷��̵�
            if (CheckUpgrade(molotovUpgradeList, "Radius Up"))
            {
                TM.AddTempStats(0, 10);
            }
            //��ô ���� ���� ���׷��̵�
            if (CheckUpgrade(molotovUpgradeList, "More Projectile"))
            {
                
            }

            //��� ����
            TM.SetCourse(mousePos);
        }
    }

    #endregion

    //������ ( SiegeMode ) , siegemodeTS
    #region

    private float siegemodeDuration = 5f;

    //������ Ȱ��ȭ
    private IEnumerator ActivateSiegeMode()
    {
        //�ʱ�ȭ
        siegemodeTS.isActive = true; siegemodeTS.curTime = 0f; siegemodeTS.curStack--;

        //���� ���

        //����� ���� ���� ���׷��̵�
        if (CheckUpgrade(siegemodeUpgradeList, "Armor Up"))
        {
            Player.Instance.ChangeDamageReduction(siegemodeData.DamageReduction + 20f > 100 ? 100 : siegemodeData.DamageReduction + 20f);
        }
        else
        {
            Player.Instance.ChangeDamageReduction(siegemodeData.DamageReduction);
        }

        //����� �̵� ���� ���׷��̵�
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

    //������ ��Ȱ��ȭ
    private IEnumerator DeactivateSiegeMode()
    {
        StopCoroutine(ActivateSiegeMode());

        //���� ���
        Player.Instance.ChangeDamageReduction(0f); Player.Instance.ChangeSpeedReduction(0f);

        //�ʱ�ȭ
        siegemodeTS.isActive = false; siegemodeTS.canUse = false;

        yield return null;
    }

    #endregion

    //ȸ�ǻ�� ( EvadeShot ) , evdshotTS
    #region

    /// <summary> ��󿡰� ������ ���� (Collider2D[], float) </summary>
    /// <param name="colliders">���� ���</param>
    /// <param name="dmg">���� ������</param>
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

    /// <summary> ȸ�ǻ�� ���� </summary>
    /// <returns></returns>
    private IEnumerator Evdshot()
    {
        evdshotTS.curTime = 0f; evdshotTS.canUse = false; evdshotTS.curStack--;
        yield return new WaitForSeconds(0.2f);

        //����Ʈ, ������
        // ��������
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x >= Player.Instance.transform.position.x)
        {
            Instantiate(evdshotData.effect, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(1, 0, 0), 1);

            //ȸ�ǻ�� ������ ���׷��̵�
            if(CheckUpgrade(evdshotUpgradeList, "Damage Up"))
            {
                ApplyEvdshotDamage(col, evdshotData.Damage * 20f);
            }
            else
            {
                ApplyEvdshotDamage(col, evdshotData.Damage);
            }
            

            //ȸ�ǻ�� �Ÿ� ���׷��̵�
            if(CheckUpgrade(evdshotUpgradeList, "Range Up"))
            {
                rb.AddForce(Vector2.left * 30f, ForceMode2D.Impulse); 
            }
            else
            {
                rb.AddForce(Vector2.left * 20f, ForceMode2D.Impulse);
            }
        }
        // ��������
        else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < Player.Instance.transform.position.x) 
        {
            Instantiate(evdshotData.effect, this.transform.position + new Vector3(-1, 0, 0), this.transform.rotation);
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position + new Vector3(-1, 0, 0), 1);

            //ȸ�ǻ�� ������ ���׷��̵�
            //����
            if (CheckUpgrade(evdshotUpgradeList, "Damage Up"))
            {
                ApplyEvdshotDamage(col, evdshotData.Damage * 20f);
            }
            //������
            else
            {
                ApplyEvdshotDamage(col, evdshotData.Damage);
            }

            //ȸ�ǻ�� �̵� �Ÿ� ���׷��̵�
            //����
            if (CheckUpgrade(evdshotUpgradeList, "Range Up"))
            {
                rb.AddForce(Vector2.right * 30f, ForceMode2D.Impulse);
            }
            //������
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

        //ȸ��
        CheckTime(dodgeTS, dodgeData, dodgeUpgradeList);
        if (siegemodeTS.isActive == false && dodgeTS.canUse == true && isDashing == false)
        {
            if(Input.GetKeyDown(KeyCode.Space) && (mx != 0 || my != 0))
            {
                StartCoroutine(Dodge());
                FindObjectOfType<SoundManager>().PlaySound("Dash");
            }
        }

        //ȭ����
        CheckTime(molotovTS, molotovData, molotovUpgradeList);
        if (molotovTS.canUse == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ThrowMolotov();
            }
        }

        //������
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

        //ȸ�ǻ��
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
