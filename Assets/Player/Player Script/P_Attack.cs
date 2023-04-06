using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�߰����� ------------------



//---------------------------



public class P_Attack : MonoBehaviour
{

    //���콺 �� �÷��̾� ��ġ ����
    private Vector3 Mouse_Position;
    private Vector3 P_Position;

    //�÷��̾� ������Ʈ ����
    private GameObject Player;

    //�÷��̾� ���� ���� ������Ʈ ����
    private GameObject Weapon;

    //�÷��̾� �¿� ���� ����
    private bool Rev_Flag = false;

    //���⺤�� ��� �Լ�
    private void CalcWatchVec()
    {
        //���콺 ��ġ�� �÷��̾� ��ġ �Է�
        Mouse_Position = Input.mousePosition;
        P_Position = Player.transform.position;

        //���콺�� z���� ī�޶� ������ ��ġ
        Mouse_Position.z = P_Position.z - Camera.main.transform.position.z;

        //���� ���콺 ��ġ �Է�
        Vector3 target = Camera.main.ScreenToWorldPoint(Mouse_Position);

        //���콺 ���� ���
        float dx = target.x - P_Position.x;
        float dy = target.y - P_Position.y;

        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);

        //���콺��ġ�� ���� �¿� ����
        if(dx< 0)
        {
            Rev_Flag = true;
        }
        else
        {
            Rev_Flag= false;
        }
        Player.GetComponent<SpriteRenderer>().flipX = Rev_Flag;

    }
    
    //���۽� �ʱ�ȭ
    private void Start()
    {
        //�÷��̾�� ���� ã��
        Player = GameObject.FindWithTag("Player");
        Weapon = GameObject.FindWithTag("Weapon");
    }

    //�����Ӹ���
    private void Update()
    {
        //�������� ���� ���
        CalcWatchVec();
    }
}
