using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*�߰� ���� ---------------------------



----------------------------------------*/

public class W_Rifle : MonoBehaviour
{
    //���� �⺻ ������ ����
    public W_Data data;
    public P_Skill skill;

    //-----------------------------------<���� ���>--------------------------------------------------

    //������� ���ݹ��
    public int bulletMultiply = 3;
    private GameObject[] bullets;

    //�߻���ġ ����
    private GameObject Fire_Position;
    private float CurTime = 0;

    private void Attack()
    {

        Fire_Position = GameObject.FindWithTag("Fire Position");
        if (CurTime <= 0f)
        {
            if (Input.GetMouseButtonDown(0) && skill.siegeIsActive == false)
            {
                //�Ѿ� ����
                GameObject Bullet;
                Bullet = Instantiate(data.Bullet, Fire_Position.transform.position, Quaternion.Euler(0f, 0f, rotateDegree - 90f));

                //�Ѿ� ������ �Է�
                Bullet.GetComponent<Bullet>().SetStats(data.W_Speed,data.W_Damage,data.W_Distance);

                //�߻�ð� �ʱ�ȭ
                CurTime = data.W_AttackSpeed;
            }
            else if(Input.GetMouseButtonDown(0) && skill.siegeIsActive == true)
            {
                bullets = new GameObject[bulletMultiply];
                float deg = -10f;
                //�Ѿ� ����
                for(int i = 0; i < bulletMultiply; i++)
                {
                    bullets[i] = Instantiate(data.Bullet, Fire_Position.transform.position, Quaternion.Euler(0f, 0f, rotateDegree - 90f + deg));

                    //�Ѿ� ������ �Է�
                    bullets[i].GetComponent<Bullet>().SetStats(data.W_Speed, data.W_Damage, data.W_Distance);
                    deg += 10f;
                }
                //�߻�ð� �ʱ�ȭ
                CurTime = data.W_AttackSpeed;
            }
        }
        else
        {
            CurTime -= Time.deltaTime;
        }
    }

    private void SpecialAttack()
    {

    }

    //-----------------------------------<�ִϸ��̼�>--------------------------------------------------

    //ȸ������ ����
    private float rotateDegree;

    //���콺 �� �÷��̾� ��ġ ����
    private Vector3 Mouse_Position;
    private Vector3 W_Position;

    //�÷��̾� ���� ���� ������Ʈ ����
    private GameObject WeaponCase;

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

        WeaponCase.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);

        //���콺��ġ�� ���� �¿� ����
        if (dx < 0f)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
            WeaponCase.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            WeaponCase.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //-----------------------------------<�ʱ�ȭ>--------------------------------------------------


    private void Awake()
    {
        WeaponCase = GameObject.FindWithTag("Weapon Case");
        Fire_Position = GameObject.FindWithTag("Fire Position");
        skill = GameObject.FindWithTag("Player").GetComponent<P_Skill>();
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
