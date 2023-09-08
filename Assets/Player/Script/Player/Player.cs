using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

/*�߰� ���� ---------------------------



----------------------------------------*/

/// <summary>
/// �÷��̾� ����
/// </summary>
public enum PlayerState
{
    Normal, 
    Danger, //���� ü���� 30% ����
    Dead    //���� ü���� ����
}

public class Player : MonoBehaviour
{
    //Field
    #region

    //�÷��̾�� ĳ���� �⺻ ������
    [Header("Player Data")]
    public PlayerData data;
    public static Player Instance;

    private Animator ani;
    private GameObject weaponCase;

    #endregion

    //ü��
    #region

    public float MaxHP, CurrentHp;
    private PlayerState CurrentPlayerState;

    [Range(0f,100f)]
    public float ArmorReductionBySkill;

    //���� ���۽� ������ �ҷ����� �뵵
    private void UpdateSetting()
    {
        MaxHP = data.maxHP + (data.StrLevel * 10f);
        Speed = data.speed * (1 + (data.AgiLevel * 0.02f));
    }

    /// <summary>
    /// �÷��̾�� ������ �������� amount%��ŭ �氨
    /// </summary>
    /// <param name="amount">�氨��</param>
    public void ChangeDamageReduction(float amount)
    {
        ArmorReductionBySkill = amount;
    }

    /// <summary>
    /// �÷��̾� ü���� damage��ŭ ����
    /// </summary>
    /// <param name="damage">�޴� ������</param>
    public void P_TakeDamage(float damage)
    {
        if (!SkillManager.Instance.dodgeTS.isActive && CurrentPlayerState != PlayerState.Dead)
        {
            //�޴� �������� 1���ϸ� 1��
            if (((damage - data.GetArmor()) * (1 - ArmorReductionBySkill/100)) * (1 - data.ArmorMasteryLevel * 0.02f) <= 1) { CurrentHp -= 1; }
            //�ƴ� ��� �״��
            else { CurrentHp -= ((damage - data.GetArmor()) * (1 - ArmorReductionBySkill/100)) * (1 - data.ArmorMasteryLevel * 0.02f); }

            //�ǰ� ����Ʈ
            UIHitEffect.Instance.IsDamaged();
        }
    }

    /// <summary>
    /// �÷��̾� ü���� Amount��ŭ ȸ��
    /// </summary>
    /// <param name="amount"></param>
    public void P_Heal(float amount)
    {
        CurrentHp += amount;
        if(CurrentHp > MaxHP) { CurrentHp = MaxHP; }
    }

    /// <summary>
    /// �÷��̾��� ���¸� ��ȯ
    /// </summary>
    /// <returns>0 : Normal, 1 : Danger, 2 : Dead</returns>
    public int GetPlayerstate()
    {
        return (int)CurrentPlayerState;
    }

    private void CheckPlayerState()
    {
        //�����
        if (CurrentHp <= 0)
        {
            CurrentHp = 0; IsDead();
            CurrentPlayerState = PlayerState.Dead;  //�÷��̾� ���� ����
            StartCoroutine(FindObjectOfType<GameOver>().GameOverCoroutine());
        }
        //ü���� 30% �����϶�
        else if (CurrentHp < MaxHP * 0.3f)
        {
            CurrentPlayerState = PlayerState.Danger;
        }
        //ü���� 30% �̻��϶�
        else
        {
            CurrentPlayerState = PlayerState.Normal;
        }
    }

    #endregion

    //�̵�
    #region
    // �÷��̾� �̵��� ������ ����
    private float Speed, P_XSpeed, P_YSpeed;
    [Range(0f,1f)]
    private float SpeedReduction;

    /// <summary>
    /// �÷��̾� �̵��ӵ��� amount% ��ŭ ����
    /// </summary>
    /// <param name="amount">���ҷ�</param>
    public void ChangeSpeedReduction(float amount)
    {
        SpeedReduction = amount;
    }

    private void InputSpeed()
    {

        //�¿� ������ �ӵ� ����
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

        //���� ������ �ӵ� ����
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

    //�ִϸ��̼�
    #region

    private void CalcVec()
    {
        Vector3 mousePos, playerPos;
        //���콺 ��ġ�� �÷��̾� ��ġ �Է�
        mousePos = Input.mousePosition;
        playerPos = this.transform.position;

        //���콺�� z���� ī�޶� ������ ��ġ
        mousePos.z = playerPos.z - Camera.main.transform.position.z;

        //���� ���콺 ��ġ �Է�
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        float dx = target.x - playerPos.x;

        //���콺��ġ�� ���� �¿� ����
        //1. ���콺�� ������ ������
        if (dx < 0f)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        //2.���콺�� ������ ������
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //ĳ���Ͱ� �����̴���
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
        //���� ����
        Instance = this;

        //�ִϸ����� ����
        ani = GetComponent<Animator>();

        //���� ����
        weaponCase = transform.Find("Weapon Case").gameObject;
        weaponCase.SetActive(true);

        Instance.enabled = true;

        UpdateSetting();

        //�÷��̾� �߷� �� �� ȸ�� ����
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
        //�ִϸ��̼�
        CalcVec();
        IsMove();

        //�̵�
        InputSpeed();

        //ü��
        CheckPlayerState();
    }

    private void FixedUpdate()
    {
        //�÷��̾� ������ ����
        this.transform.Translate(P_XSpeed * Time.deltaTime, P_YSpeed * Time.deltaTime, 0);
    }
    #endregion
}
