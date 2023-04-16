using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*�߰� ���� ---------------------------



----------------------------------------*/

public class W_Rifle : MonoBehaviour
{
    //���� �⺻ ������ ����
    public W_Data Data;

    //-----------------------------------<���� ���>--------------------------------------------------

    //�߻���ġ ����
    private GameObject Fire_Position;
    private float CurTime = 0;

    private void Attack()
    {
        if (CurTime <= 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //�Ѿ� ����
                GameObject Bullet;
                Bullet = Instantiate(Data.Bullet, Fire_Position.transform.position, Quaternion.Euler(0f, 0f, rotateDegree - 90f));

                //�Ѿ� ������ �Է�
                Bullet.GetComponent<Bullet>().SetStats(Data.W_Speed,Data.W_Damage,Data.W_Distance);

                //�߻�ð� �ʱ�ȭ
                CurTime = Data.W_AttackSpeed;
            }
        }
        else
        {
            CurTime -= Time.deltaTime;
        }
    }

    //-----------------------------------<�ִϸ��̼�>--------------------------------------------------

    //ȸ������ ����
    private float rotateDegree;

    //���콺 �� �÷��̾� ��ġ ����
    private Vector3 Mouse_Position;
    private Vector3 W_Position;

    //�÷��̾� ���� ���� ������Ʈ ����
    private GameObject Hand;

    //���⺤�� ��� �Լ�
    private void CalcVec()
    {
        //���콺 ��ġ�� �÷��̾� ��ġ �Է�
        Mouse_Position = Input.mousePosition;
        W_Position = this.transform.position;

        //���콺�� z���� ī�޶� ������ ��ġ
        Mouse_Position.z = W_Position.z - Camera.main.transform.position.z;

        //���� ���콺 ��ġ �Է�
        Vector3 target = Camera.main.ScreenToWorldPoint(Mouse_Position);

        //���콺 ���� ���
        float dx = target.x - W_Position.x;
        float dy = target.y - W_Position.y;

        rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Hand.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);

        //���콺��ġ�� ���� �¿� ����
        if (dx < 0f)
        {
            Hand.transform.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            Hand.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //-----------------------------------<�ʱ�ȭ>--------------------------------------------------


    private void Awake()
    {
        Hand = GameObject.FindWithTag("Hand");
        Fire_Position = GameObject.FindWithTag("Fire Position");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        CalcVec();
        Attack();
    }
}
