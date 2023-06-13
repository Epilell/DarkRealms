using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal;
using UnityEngine;

/*�߰� ���� ---------------------------



----------------------------------------*/

public class Player : MonoBehaviour
{
    //Public Field
    #region

    //�÷��̾�� ĳ���� �⺻ ������
    [Header("Player Data")]
    public P_Data data;

    #endregion

    //Private Field
    #region

    private Animator ani;
    private Player P;
    private GameObject weaponCase;

    #endregion

    //ü�°���
    #region

    public float MaxHP, CurrentHp;
    [Range(0f,1f)]
    public float ArmorReduction;

    //���� ���۽� ������ �ҷ����� �뵵
    private void UpdateSetting()
    {
        MaxHP = data.maxHP + (data.Strlevel * 10f);
        Speed = data.speed + (data.Agilevel * 0.1f);
    }

    /// <summary>
    /// �÷��̾�� ������ �������� amount%��ŭ �氨
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeArmorReduction(float amount)
    {
        ArmorReduction = amount/100;
    }

    /// <summary>
    /// �÷��̾� ü���� damage��ŭ ����
    /// </summary>
    /// <param name="damage"></param>
    public void P_TakeDamage(float damage)
    {
        if((damage - data.GetArmor()) * (1 - ArmorReduction) <= 1 ) { CurrentHp -= 1; }
        else { CurrentHp -= (damage - data.GetArmor()) * (1 - ArmorReduction); }
        if(CurrentHp <=0) { CurrentHp = 0; IsDead(); Debug.Log(ani.GetBool("IsDead"));
            StartCoroutine(FindObjectOfType<GameOver>().GameOverCoroutine());
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
    #endregion

    //�̵�����
    #region
    // �÷��̾� �̵��� ������ ����
    private float Speed, P_XSpeed, P_YSpeed;
    [Range(0f,1f)]
    private float SpeedReduction;

    /// <summary>
    /// �÷��̾� �̵��ӵ��� amount% ��ŭ ����
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeSpeedReduction(float amount)
    {
        SpeedReduction = amount/100;
    }

    private void InputSpeed()
    {

        //�¿� ������ �ӵ� ����
        if (Input.GetKey(KeyCode.D) && P_XSpeed >= 0)
        {
            if(dx >= 0)
            { P_XSpeed = (Speed) * (1 - SpeedReduction); }
            else
            { P_XSpeed = (float)(Speed * 0.9) * (1 - SpeedReduction); }
        }
        else if (Input.GetKey(KeyCode.A) && P_XSpeed <= 0)
        {
            if(dx < 0)
            { P_XSpeed = -Speed * (1 - SpeedReduction); }
            else
            { P_XSpeed = -(float)(Speed * 0.9) * (1 - SpeedReduction); }
        }
        else
        { P_XSpeed = 0; }

        //���� ������ �ӵ� ����
        if (Input.GetKey(KeyCode.W) && P_YSpeed >= 0)
        {
            if(dy >= 0)
            { P_YSpeed = Speed * (1 - SpeedReduction); }
            else
            { P_YSpeed = (float)(Speed * 0.9) * (1 - SpeedReduction); }
        }
        else if (Input.GetKey(KeyCode.S) && P_YSpeed <= 0)
        {
            if(dy < 0)
            { P_YSpeed = -Speed * (1 - SpeedReduction); }
            else
            { P_YSpeed = -(float)(Speed * 0.9) * (1 - SpeedReduction); }
        }
        else
        { P_YSpeed = 0; }
    }
    #endregion

    //�ɷ�ġ ����
    #region

    //�ɷ�ġ ������ ���
    private void UpdateStats()
    {
        if(data.Strexp >= 100) { data.Strlevel++; }
        
        if(data.Agiexp >= 100) { data.Agilevel++; }
        
        if(data.Intexp >= 100) { data.Intlevel++; }

        UpdateSetting();
    }
    #endregion

    //�ִϸ��̼�
    #region
    //���콺 �� �÷��̾� ��ġ ����
    private Vector3 Mouse_Position, P_Position;
    [HideInInspector]
    public float dx, dy;

    //���⺤�� ��� �Լ�
    private void CalcVec()
    {
        //���콺 ��ġ�� �÷��̾� ��ġ �Է�
        Mouse_Position = Input.mousePosition;
        P_Position = this.transform.position;

        //���콺�� z���� ī�޶� ������ ��ġ
        Mouse_Position.z = P_Position.z - Camera.main.transform.position.z;

        //���� ���콺 ��ġ �Է�
        Vector3 target = Camera.main.ScreenToWorldPoint(Mouse_Position);

        //���콺 ���� ���
        dx = target.x - P_Position.x;
        dy = target.y - P_Position.y;

        //���콺��ġ�� ���� �¿� ����
        if (dx < 0f)
        { 
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
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
            P.enabled = false;
            weaponCase.SetActive(false);
        }
    }

    #endregion

    //Unity Event
    #region
    private void Awake()
    {
        //�ִϸ����� ����
        ani = GetComponent<Animator>();
        P = GetComponent<Player>();
        weaponCase = transform.Find("Weapon Case").gameObject;
        

        P.enabled = true;
        weaponCase.SetActive(true);

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
        //<�ִϸ��̼� ����>
        CalcVec();
        IsMove();

        //<�̵� ����>
        InputSpeed();

        //<�ɷ�ġ ����>
        UpdateStats();
    }

    private void FixedUpdate()
    {
        //�÷��̾� ������ ����
        this.transform.Translate(P_XSpeed * Time.deltaTime, P_YSpeed * Time.deltaTime, 0);
    }
    #endregion
}
