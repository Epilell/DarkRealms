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
    //�÷��̾�� ĳ���� �⺻ ������
    public P_Data Data;

    private Player P;
    private GameObject weaponCase;

    //--------------------------------------<ü��>---------------------------------------------------
    private float MaxHp, CurrentHp, Helmet, Armor;

    //���� ���۽� ������ �ҷ����� �뵵
    private void StartSetting()
    {
        MaxHp = Data.P_MaxHp + (Data.Strength_Level * 10f);
        CurrentHp = Data.P_CurrentHp;
        Helmet = Data.P_HelmetArmor;
        Armor = Data.P_BodyArmor;
    }

    //��������� �Ǵ� ����� ���
    private void UpdateDB()
    {
        Data.P_MaxHp = MaxHp;
        Data.P_CurrentHp = CurrentHp;
        Data.P_HelmetArmor = Helmet;
        Data.P_BodyArmor = Armor;
    }

    //�������� �ɷ�ġ �ݿ���
    private void UpdateMaxHp() { MaxHp = Data.P_MaxHp + (Data.Strength_Level * 10f); }

    //������ ������ �ݿ�
    private void UpdateCurrentHp() { Data.P_CurrentHp = CurrentHp; }

    //--------------------------------------<��� �ɷ�ġ>-------------------------------------------------
    private void UpdateArmor() 
    {
        Data.P_HelmetArmor = Helmet;
        Data.P_BodyArmor = Armor;
    }

    private float TotalArmor() { return Helmet + Armor; }

    //--------------------------------------<ü�� ����>-------------------------------------------------
    [Range(0f,1f)]
    private float ArmorReduction;

    public void ChangeArmorReduction(float amount)
    {
        ArmorReduction = amount;
    }

    public void P_TakeDamage(float damage)
    {
        if((damage - TotalArmor()) * (1 - ArmorReduction) <= 1 ) { CurrentHp -= 1; }
        else { CurrentHp -= (damage - TotalArmor()) * (1 - ArmorReduction); }
        if(CurrentHp <=0) { CurrentHp = 0; IsDead(); UpdateDB(); }
        UpdateCurrentHp();
    }

    public void P_Heal(float Amount)
    {
        CurrentHp += Amount;
        if(CurrentHp > MaxHp) { CurrentHp = MaxHp; }
        UpdateCurrentHp();
    }

    //--------------------------------------<�̵��ӵ�>-------------------------------------------------
    // �÷��̾� �̵��� ������ ����
    private float Speed, P_XSpeed, P_YSpeed;
    [Range(0f,1f)]
    private float SpeedReduction;

    public void ChangeSpeedReduction(float amount)
    {
        SpeedReduction = amount;
    }

    private void InputSpeed()
    {
        //�̵��ӵ� �Է�
        UpdateSpeed();

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

    //--------------------------------------<�ɷ�ġ ����>--------------------------------------------

    private void UpdateStrLevel() { Data.Strength_Level += 1; }
    private void UpdateAgiLevel() { Data.Agility_Level += 1; }
    private void UpdateIntLevel() { Data.Intelligent_Level += 1; }

    //�ɷ�ġ ������ ���
    private void UpdateStats()
    {
        if(Data.Str_Exp >= 100) { UpdateStrLevel(); Data.Str_Exp = 0; UpdateMaxHp(); }
        
        if(Data.Agi_Exp >= 100) { UpdateAgiLevel(); Data.Agi_Exp = 0; UpdateSpeed(); }
        
        if(Data.Int_Exp >= 100) { UpdateIntLevel(); Data.Int_Exp = 0; }
    }

    private void UpdateSpeed() => Speed = Data.P_Speed + (Data.Agility_Level * 0.1f);

    //--------------------------------------<�ִϸ��̼�>---------------------------------------------
    //�÷��̾� �ִϸ�����
    private Animator P_Ani;

    //���콺 �� �÷��̾� ��ġ ����
    private Vector3 Mouse_Position, P_Position;
    private float dx, dy;

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
            //this.transform.localScale = new Vector3(-1, 1, 1);
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        { 
            //this.transform.localScale = new Vector3(1, 1, 1);
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    //ĳ���Ͱ� �����̴���
    private void IsMove()
    {
        if (P_XSpeed != 0f || P_YSpeed != 0f)
        { P_Ani.SetBool("IsMove", true); }
        else
        { P_Ani.SetBool("IsMove", false); }
    }

    private void IsDead() 
    {
        if (CurrentHp <= 0) 
        {
            P_Ani.SetBool("IsDead", true);
            P.enabled = false;
            weaponCase.SetActive(false);
        }
    }


    //------------------------------------------<�ʱ�ȭ>-------------------------------------------
    private void Awake()
    {
        //�ִϸ����� ����
        P_Ani = GetComponent<Animator>();
        P = GetComponent<Player>();
        weaponCase = GameObject.FindWithTag("Weapon Case");

        P.enabled = true;
        weaponCase.SetActive(true);

        StartSetting();

        //�÷��̾� �߷� �� �� ȸ�� ����
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;

    }

    // Start is called before the first frame update
    private void Start()
    {
        
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
}
