using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�߰� ���� -----------



//----------------------

public class P_Move : MonoBehaviour
{
    // �÷��̾� �̵��ӵ� ����
    [SerializeField] private float P_Speed = 5;

    // �÷��̾� �̵��ӵ� ���� ����
    private float Max_P_Speed = 10;
    private float Min_P_Speed = 2;

    // �÷��̾� �̵��� ������ ����
    private float P_XSpeed = 0;
    private float P_YSpeed = 0;

    

    // �ӵ� ���� (�����ۿ��� �����Ͽ� �ӵ�����)
    public void Inc_Speed(float num)
    {
        P_Speed += num;
        if( P_Speed > Max_P_Speed)
        {
            P_Speed = Max_P_Speed;
        }
    }

    // �ӵ� ����
    public void Dec_Speed(float num)
    {
        P_Speed -= num;
        if (P_Speed < Min_P_Speed)
        {
            P_Speed = Min_P_Speed;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //�÷��̾� �߷� �� �� ȸ�� ����
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        //���� ������ �ӵ� ����
        if (Input.GetKey(KeyCode.W) && P_YSpeed >= 0)
        {
            P_YSpeed = P_Speed;
        }
        else if (Input.GetKey(KeyCode.S) && P_YSpeed <= 0)
        {
            P_YSpeed = -P_Speed;
        }
        else
        {
            P_YSpeed = 0;
        }

        //�¿� ������ �ӵ� ����
        if (Input.GetKey(KeyCode.D) && P_XSpeed >= 0)
        {
            P_XSpeed = P_Speed;
        }
        else if (Input.GetKey(KeyCode.A) && P_XSpeed <= 0)
        {
            P_XSpeed = -P_Speed;
        }
        else
        {
            P_XSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        //�÷��̾� ������ ����
        this.transform.Translate(P_XSpeed * Time.deltaTime, P_YSpeed * Time.deltaTime, 0);
    }
}
