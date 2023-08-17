using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoblinShamanAtking : MobAttacking
{
    private Animator animator;
    //�÷��̾� �Ÿ����
    private Transform PlayerDirection;
    private float distanceToPlayer;
    public float minimumDistance=2f;
    //�������� ����ǥ��
    public Transform pos;
    public Vector2 boxSize = new Vector2(1, 1);
    //������
    public GameObject bullet;
    private void FixedUpdate()
    {
        // ���� ��ü�� �÷��̾� ������ �Ÿ� ���
        distanceToPlayer = Vector2.Distance(transform.position, PlayerDirection.position);


    }
    private void Start()
    {
        if (this.transform.GetChild(0) != null)
        {
            pos = this.transform.GetChild(0);
        }
        PlayerDirection = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected void OnDrawGizmos()
    {
        pos = this.transform.GetChild(0);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    new public void Attacking()
    {
        if (distanceToPlayer > minimumDistance)//�÷��̾ �ּ� ���ݹ��� �ۿ� ������ ����1���
        {
            //idle
        }
        else //�÷��̾ �ּ� ���ݹ��� �ȿ������� ����2 ���
        {
            StartCoroutine(Attacking2());
        }
    }

    private IEnumerator Attacking1()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("attack1", true);
        for(int i = 0; i < 5; i++)
        {
            GameObject MobBullet = Instantiate(bullet, this.transform.position+new Vector3(2,0,0), Quaternion.identity);
            MobBullet.GetComponent<MobRangeBullet>().SetStats(1f, damage, PlayerDirection.position, 0);
        }
        //
    }
    private IEnumerator Attacking2()
    {
        yield return new WaitForSeconds(0.5f);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            Debug.Log(collider.tag);
            if (collider.tag == "Player")
            {
                collider.GetComponent<Player>().P_TakeDamage(damage * 1.2f);
            }
        }
    }
}
