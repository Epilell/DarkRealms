using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

    [SerializeField]
    private GameObject WeaponCase;

    //���콺 ��ġ, ���⺤��
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

    //��ų ���׷��̵� ����Ʈ
    [Header("Skill Upgrade List", order = 2), Space(5)]
    public SkillUpgradeList SkillUpgradeData;

    //��ų ��Ʈ�ڽ� ����Ʈ
    [Header("Hitbox List", order = 3), Space(5)]
    public Transform EvadeshotHitbox; //ȸ�� ��� ��Ʈ�ڽ�
    public Vector2 EvadeshotHitboxSize; //ȸ�� ��� ���� ũ��

    //��ų ����Ʈ ����Ʈ
    [Header("Effect List", order = 4), Space(5)]
    public Transform DodgeEffect; //�뽬 ����Ʈ ���� ��ġ
    public Transform DodgeTrapPosition; //�뽬 Ʈ�� ������ġ

    private Coroutine siegeModeCoroutine; // ������ ������
    public bool isSkillCanUse = true; // �κ�, �ɼ�â ���� �߰�

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

    /// <summary>
    /// �÷��̾� �¿� ���� Ȯ��
    /// </summary>
    /// <returns>������ T, ���� F</returns>
    private bool IsFlip()
    {
        if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x >= Player.Instance.transform.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

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
                if (_ts.curTime < _data.CoolTime * (1 - Player.Instance.data.Stats[2].Level * 0.02f))
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
    /// <param name="_skillName">Dodge, Molotov, Siege Mode, Evade Shot</param>
    /// <param name="_upgradeName">���׷��̵� �̸�</param>
    /// <returns></returns>
    public bool CheckUpgrade(string _skillName, string _upgradeName)
    {
        foreach(Skill _target in SkillUpgradeData.ApplyUpgradeList)
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
        dodgeTS.curTime = 0; dodgeTS.canUse = false; dodgeTS.curStack--; dodgeTS.isActive = true;
        isDashing = true; WeaponCase.SetActive(false);

        //ȸ�� �� Ʈ����ġ
        if (CheckUpgrade("Dodge", "Trap"))
        {
            Instantiate(dodgeData.GetBearTrap(), DodgeTrapPosition.position, transform.rotation);
        }
        //ȸ�� �Ÿ� ���׷��̵�
        if (CheckUpgrade("Dodge", "Range Up"))
        {
            rb.AddForce(new Vector2(mx, my).normalized * (20f + dodgeForce), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(mx, my).normalized * 20f, ForceMode2D.Impulse);
        }

        //�ִϸ��̼� ���
        ani.SetTrigger("IsDash");

        //����Ʈ ����
        //������
        if (IsFlip())
        {
            Instantiate(dodgeData.GetDodgeEffect(), DodgeEffect.position, DodgeEffect.rotation);
        }
        //����
        else
        {
            GameObject eft = Instantiate(dodgeData.GetDodgeEffect(), DodgeEffect.position, DodgeEffect.rotation);
            eft.GetComponent<SpriteRenderer>().flipX = true;
        }

        yield return new WaitForSeconds(1 / 6f);

        WeaponCase.SetActive(true);

        //ȸ���� �ʱ�ȭ
        rb.velocity = Vector3.zero; dodgeTS.isActive = false;
        isDashing = false;
    }

    #endregion

    //ȭ���� ( Molotov ) , molotovTS
    #region

    //ȭ���� ������
    private void ThrowMolotov(Vector3 _pos, Vector3 _vec)
    {
        //�ʱ�ȭ
        molotovTS.curTime = 0; molotovTS.canUse = false; molotovTS.curStack--;

        //ȭ���� ����
        GameObject Molotov;

        //�ִ� ��ô ��Ÿ� �϶�
        if (Vector3.Distance(_pos, transform.position) > molotovData.Distance)
        {
            //��ô ���� ���׷��̵� O
            if(CheckUpgrade("Molotov", "More Projectile"))
            {
                for (int i = 0; i < 3; i++)
                {
                    Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
                    Molotov TM = Molotov.GetComponent<Molotov>();
                    TM.data = molotovData;

                    //������ ���� ���׷��̵�
                    if (CheckUpgrade("Molotov", "Damage Up"))
                    {
                        TM.AddTempStats(10, 0);
                        TM.SetColor(new(73/255f, 122/255f, 231/255f, 255/255f));
                    }

                    //���� ���� ���׷��̵�
                    if (CheckUpgrade("Molotov", "Radius Up"))
                    {
                        TM.AddTempStats(0, 10);
                    }

                    //��� ����
                    TM.SetCourse(transform.position + ((_vec * molotovData.Distance) + _vec * (1 + 2 * i)));
                }
            }
            //��ô ���� ���׷��̵� X
            else
            {
                Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
                Molotov TM = Molotov.GetComponent<Molotov>();
                TM.data = molotovData;

                //������ ���� ���׷��̵�
                if (CheckUpgrade("Molotov", "Damage Up"))
                {
                    TM.AddTempStats(10, 0);
                    TM.SetColor(new(73 / 255f, 122 / 255f, 231 / 255f, 255 / 255f));
                }

                //���� ���� ���׷��̵�
                if (CheckUpgrade("Molotov", "Radius Up"))
                {
                    TM.AddTempStats(0, 10);
                }

                //��� ����
                TM.SetCourse(transform.position + (_vec * molotovData.Distance));
            }
            
        }
        //���콺 ��ġ�� ��ô�� ��
        else
        {
            //��ô ���� ���׷��̵� O
            if (CheckUpgrade("Molotov", "More Projectile"))
            {
                for(int i = 0; i < 3; i++)
                {
                    Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
                    Molotov TM = Molotov.GetComponent<Molotov>();
                    TM.data = molotovData;

                    //������ ���� ���׷��̵�
                    if (CheckUpgrade("Molotov", "Damage Up"))
                    {
                        TM.AddTempStats(10, 0);
                        TM.SetColor(new(73 / 255f, 122 / 255f, 231 / 255f, 255 / 255f));
                    }

                    //���� ���� ���׷��̵�
                    if (CheckUpgrade("Molotov", "Radius Up"))
                    {
                        TM.AddTempStats(0, 10);
                    }

                    //��� ����
                    TM.SetCourse(_pos + _vec * (1 + 2 * i));
                }
            }
            //��ô ���� ���׷��̵� X
            else
            {
                Molotov = Instantiate(molotovData.Molotov, transform.position, transform.rotation);
                Molotov TM = Molotov.GetComponent<Molotov>();
                TM.data = molotovData;

                //������ ���� ���׷��̵�
                if (CheckUpgrade("Molotov", "Damage Up"))
                {
                    TM.AddTempStats(10, 0);
                    TM.SetColor(new(73 / 255f, 122 / 255f, 231 / 255f, 255 / 255f));
                }

                //���� ���� ���׷��̵�
                if (CheckUpgrade("Molotov", "Radius Up"))
                {
                    TM.AddTempStats(0, 10);
                }

                //��� ����
                TM.SetCourse(_pos);
            }
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
        siegemodeTS.canUse = false; siegemodeTS.isActive = true; WeaponCase.SetActive(false);

        Player.Instance.ChangeSpeedReduction(100f);
        ani.SetTrigger("IsDualPrecision");
        ani.SetBool("IsSiegeMode", true);

        yield return new WaitForSecondsRealtime(0.7f);


        //�ʱ�ȭ
        WeaponCase.SetActive(true); siegemodeTS.canUse = true; siegemodeTS.curTime = 0f; siegemodeTS.curStack--;

        //��ų ��ٿ� UI
        FindObjectOfType<CoolDown>().siegeActive = true;
        StartCoroutine(FindObjectOfType<CoolDown>().SiegeCool(siegemodeDuration));

        //���� ���
        //����� ���� ���� ���׷��̵�
        if (CheckUpgrade("Siege Mode", "Armor Up"))
        {
            Player.Instance.ChangeDamageReduction(siegemodeData.DamageReduction + 20f > 100 ? 100 : siegemodeData.DamageReduction + 20f);
        }
        else
        {
            Player.Instance.ChangeDamageReduction(siegemodeData.DamageReduction);
        }

        //����� �̵� ���� ���׷��̵�
        if (CheckUpgrade("Siege Mode", "Can Move"))
        {
            Player.Instance.ChangeSpeedReduction(80f);
        }
        else
        {
            Player.Instance.ChangeSpeedReduction(100f);
        }

        yield return new WaitForSecondsRealtime(siegemodeDuration);

        //���� �ð� ����
        //��ų ��ٿ� UI
        FindObjectOfType<CoolDown>().siegeActive = false;

        //���� ���
        Player.Instance.ChangeDamageReduction(0f);
        Player.Instance.ChangeSpeedReduction(0f);

        //�ʱ�ȭ
        siegemodeTS.isActive = false;
        ani.SetBool("IsSiegeMode", false);

        yield return null;
    }

    //������ ���� ��Ȱ��ȭ
    private IEnumerator DeactivateSiegeMode()
    {
        if (siegeModeCoroutine != null)
        {
            StopCoroutine(siegeModeCoroutine);
            siegeModeCoroutine = null;
        }

        //��ų ��ٿ� UI
        FindObjectOfType<CoolDown>().siegeActive = false;

        //���� ���
        Player.Instance.ChangeDamageReduction(0f);
        Player.Instance.ChangeSpeedReduction(0f);

        //�ʱ�ȭ
        siegemodeTS.isActive = false;
        ani.SetBool("IsSiegeMode", false);

        yield return null;
    }

    #endregion

    //ȸ�ǻ�� ( EvadeShot ) , evdshotTS
    #region

    /// <summary> ��󿡰� ������ ���� (Collider2D[], float) </summary>
    /// <param name="col">���� ���</param>
    /// <param name="dmg">���� ������</param>
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

    /// <summary> ȸ�ǻ�� ���� </summary>
    /// <returns></returns>
    private IEnumerator Evdshot()
    {
        //�ʱ�ȭ
        evdshotTS.curTime = 0f; evdshotTS.canUse = false; evdshotTS.curStack--; WeaponCase.SetActive(false);

        //�ӵ� ����
        Player.Instance.ChangeSpeedReduction(100f);

        //�ִϸ��̼� ����
        ani.SetTrigger("IsEvadeshot");

        yield return new WaitForSecondsRealtime(0.5f);
        FindObjectOfType<SoundManager>().PlaySound("EvadeShot");

        //����Ʈ, ������
        // ������ ���
        if (IsFlip())
        {
            Instantiate(evdshotData.Effect, EvadeshotHitbox.position, EvadeshotHitbox.rotation);
            Collider2D[] col = Physics2D.OverlapBoxAll(EvadeshotHitbox.position, EvadeshotHitboxSize, 0);

            //ȸ�ǻ�� ������ ���׷��̵�
            //����
            if (CheckUpgrade("Evade Shot", "Damage Up"))
            {
                ApplyEvdshotDamage(col, evdshotData.Damage * 20f);
            }
            //������
            else
            {
                ApplyEvdshotDamage(col, evdshotData.Damage);
            }

            //ȸ�ǻ�� �̵��Ÿ� ���׷��̵�
            if (CheckUpgrade("Evade Shot", "Range Up"))
            {
                rb.AddForce(Vector2.left * 30f, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.left * 20f, ForceMode2D.Impulse);
            }
        }
        // ���� ���
        else
        {
            GameObject efc = Instantiate(evdshotData.Effect, EvadeshotHitbox.position, EvadeshotHitbox.rotation);
            efc.GetComponent<SpriteRenderer>().flipX = true;
            Collider2D[] col = Physics2D.OverlapBoxAll(EvadeshotHitbox.position, EvadeshotHitboxSize, 0);

            //ȸ�ǻ�� ������ ���׷��̵�
            //����
            if (CheckUpgrade("Evade Shot", "Damage Up"))
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
            if (CheckUpgrade("Evade Shot", "Range Up"))
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

        //�ӵ� �ʱ�ȭ
        Player.Instance.ChangeSpeedReduction(0f);

        //�ʱ�ȭ
        rb.velocity = Vector2.zero; WeaponCase.SetActive(true);
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

        //��ų  
        if (isSkillCanUse && !FindObjectOfType<GameOver>().isGameOver)
        {
            //ȸ��
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

            //ȭ����
            CheckTime(molotovTS, molotovData, "Molotov");
            if (molotovTS.canUse == true)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    ThrowMolotov(mousePos, mouseVec);
                    FindObjectOfType<CoolDown>().molotovActive = true;
                }
            }

            //������
            if(siegemodeTS.isActive == false)
            {
                CheckTime(siegemodeTS, siegemodeData, "Siege Mode");
            }
            if (siegemodeTS.canUse == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (siegemodeTS.isActive)
                    {
                        StartCoroutine(DeactivateSiegeMode());
                        FindObjectOfType<CoolDown>().siegeCoolDown = true;
                    }
                    else
                    {
                        siegeModeCoroutine = StartCoroutine(ActivateSiegeMode());
                    }
                }
            }

            //ȸ�ǻ��
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

    //��ų ��Ʈ�ڽ� �� ����Ʈ ����
    private void OnDrawGizmos()
    {
        //��Ʈ�ڽ�
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(EvadeshotHitbox.position, EvadeshotHitboxSize); //ȸ�� ���

        //����Ʈ
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(DodgeEffect.position, new Vector2(0.2f, 0.2f)); //ȸ�� ����Ʈ
        Gizmos.DrawWireSphere(DodgeTrapPosition.position, 0.1f); //Ʈ�� ���� ��ġ
        
    }
    #endregion
}