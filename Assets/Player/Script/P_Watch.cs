using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�߰����� ------------------



//---------------------------

public class P_Watch : MonoBehaviour
{
    //���콺 �� �÷��̾� ��ġ ����
    private Vector3 Mouse_Position;
    private Vector3 P_Position;

    //�÷��̾� �߽� ����
    private GameObject Center;

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
        float dy = target.y - P_Position.y;

        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Center.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);

        //���콺��ġ�� ���� �¿� ����
        if(dx < 0f)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //-------------------------------------------------------------------------------------------

    //���۽� �ʱ�ȭ
    private void Start()
    {
        //�÷��̾� �߽� ã��
        Center = GameObject.FindWithTag("Center");
    }

    //�����Ӹ���
    private void Update()
    {
        //�������� ���� ���
        CalcVec();
    }
}
