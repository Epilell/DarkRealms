using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoblinShamanAtking : MobAttacking
{
    private Animator animator;
    //플레이어 거리계산
    private Transform PlayerDirection;
    private float distanceToPlayer;
    public float minimumDistance=2f;
    //근접공격 범위표시
    public Transform pos;
    public Vector2 boxSize = new Vector2(1, 1);
    //프리팹
    public GameObject bullet;
    private void FixedUpdate()
    {
        // 현재 객체와 플레이어 사이의 거리 계산
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
        if (distanceToPlayer > minimumDistance)//플레이어가 최소 공격범위 밖에 있으면 패턴1사용
        {
            //idle
        }
        else //플레이어가 최소 공격범위 안에있으면 패턴2 사용
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
