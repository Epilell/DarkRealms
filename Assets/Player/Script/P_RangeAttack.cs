using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_RangeAttack : MonoBehaviour
{
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

    //----------------------------------------------------------------------

    //�Ѿ� ����
    public GameObject Bullet;
    private GameObject Fire_Position;

    [Range(0.5f,5f)]
    public float Attack_Speed = 2f;
    private float CurTime;

    private void Attack()
    {
        if(CurTime < 0f)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Instantiate(Bullet, Fire_Position.transform.position, Quaternion.Euler(0f, 0f, rotateDegree - 90f));
                CurTime = Attack_Speed;
            }
        }
        else
        {
            CurTime -= Time.deltaTime;
        }
    }
    private void Awake()
    {
        Hand = GameObject.FindWithTag("Hand");
        Fire_Position = GameObject.FindWithTag("Fire Position");
        CurTime = Attack_Speed;
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
