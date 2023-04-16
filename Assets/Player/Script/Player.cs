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

    //--------------------------------------<ü��>---------------------------------------------------
    private float MaxHp, CurrentHp;

    //���� ���۽� ������ �ҷ����� �뵵
    private void StartSetting()
    {
        MaxHp = Data.P_MaxHp + (Data.Strength_Level * 10f);
        CurrentHp = Data.P_CurrentHp;
    }

    //��������� �Ǵ� ����� ���
    private void UpdateDB()
    {
        Data.P_MaxHp = MaxHp;
        Data.P_CurrentHp = CurrentHp;
    }

    //�������� �ɷ�ġ �ݿ���
    private void UpdateMaxHp() { MaxHp = Data.P_MaxHp + (Data.Strength_Level * 10f); }

    //������ ������ �ݿ�
    private void UpdateCurrentHp() { Data.P_CurrentHp = CurrentHp; }

    public void P_TakeDamage(float damage)
    {
        CurrentHp -= damage;
        if(CurrentHp <=0) { CurrentHp = 0; }
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

    private void UpdateSpeed() => Speed = Data.P_Speed + (Data.Agility_Level * 0.1f);

    private void UpSpeed()
    {
        Data.P_Speed += 1;
    }

    private void InputSpeed()
    {
        //�̵��ӵ� �Է�
        UpdateSpeed();

        //���� ������ �ӵ� ����
        if (Input.GetKey(KeyCode.W) && P_YSpeed >= 0)
        {
            P_YSpeed = Speed;
        }
        else if (Input.GetKey(KeyCode.S) && P_YSpeed <= 0)
        {
            P_YSpeed = -Speed;
        }
        else
        {
            P_YSpeed = 0;
        }

        //�¿� ������ �ӵ� ����
        if (Input.GetKey(KeyCode.D) && P_XSpeed >= 0)
        {
            P_XSpeed = Speed;
        }
        else if (Input.GetKey(KeyCode.A) && P_XSpeed <= 0)
        {
            P_XSpeed = -Speed;
        }
        else
        {
            P_XSpeed = 0;
        }
    }

    //--------------------------------------<�ɷ�ġ ����>--------------------------------------------

    private void UpdateStrLevel() { Data.Strength_Level += 1; }
    private void UpdateAgiLevel() { Data.Agility_Level += 1; }
    private void UpdateIntLevel() { Data.Intelligent_Level += 1; }

    //�ɷ�ġ ������ ���
    private void UpdateStats()
    {
        if(Data.Str_Exp >= 100) { UpdateStrLevel(); Data.Str_Exp = 0; }
        UpdateMaxHp();
        if(Data.Agi_Exp >= 100) { UpdateAgiLevel(); Data.Agi_Exp = 0; }
        UpdateSpeed();
        if(Data.Int_Exp >= 100) { UpdateIntLevel(); Data.Int_Exp = 0; }
    }

    //--------------------------------------<�ִϸ��̼�>---------------------------------------------
    //�÷��̾� �ִϸ�����
    private Animator P_Ani;

    //���콺 �� �÷��̾� ��ġ ����
    private Vector3 Mouse_Position, P_Position;

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
        float dx = target.x - P_Position.x;

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
        {
            P_Ani.SetBool("IsMove", true);
        }
        else
        {
            P_Ani.SetBool("IsMove", false);
        }
    }

    //------------------------------------------<�ʱ�ȭ>-------------------------------------------
    private void Awake()
    {
        //�ִϸ����� ����
        P_Ani = GetComponent<Animator>();

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

        if (Input.GetKey(KeyCode.V))
        {
            UpSpeed();
        }
    }

    private void FixedUpdate()
    {
        //�÷��̾� ������ ����
        this.transform.Translate(P_XSpeed * Time.deltaTime, P_YSpeed * Time.deltaTime, 0);
    }
}
