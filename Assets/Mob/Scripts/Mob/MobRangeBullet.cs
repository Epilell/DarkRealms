using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobRangeBullet : MonoBehaviour
{
    private float BulletSpeed;
    private float Damage;
    //private float Distance;
    Vector3 PlayerDirection;
    int BulletNum;
    float DestoryTime = 150f;
    private Animator animator;
    Rigidbody2D rb;

    private bool ishit = false;
    //능력치 가져오기
    /// <summary>
    /// 능력치 설정
    /// </summary>
    /// <param name="bulletSpeed">총알 속도</param>
    /// <param name="damage">데미지</param>
    /// <param name="P_direction">플레이어 위치</param>
    /// <param name="bulletNum">불릿의 번호</param>
    public void SetStats(float bulletSpeed, float damage, Vector3 P_direction, int bulletNum)
    {
        this.BulletSpeed = bulletSpeed;
        this.Damage = damage;
        this.PlayerDirection = P_direction;

        this.BulletNum = bulletNum;
        //this.Distance = distance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ishit == false)
            {
                ishit = true;
                collision.gameObject.GetComponent<Player>().P_TakeDamage(Damage);
            }
            StartCoroutine(DestroyBullet());
        }
        else if (collision.gameObject.CompareTag("Map"))
        {
            StartCoroutine(DestroyBullet());
        }
        else if (collision.gameObject.CompareTag("BossMob"))
        {
            StartCoroutine(DestroyBullet());
        }
    }

    private IEnumerator DestroyBullet()
    {
        //rb.velocity = Vector2.zero;
        this.transform.position = this.transform.position;
        //animator.SetBool("broken", true);
        //사운드삽입
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
    private void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (BulletNum == 0)
        {
            rb.AddForce(PlayerDirection.normalized * 7f, ForceMode2D.Impulse);
        }
        if (BulletNum != 0)
        {

            if (BulletNum == 1)
            {
                //transform.Translate(Vector3.up * BulletSpeed * Time.deltaTime);
                rb.AddForce(Vector3.up.normalized * 1f, ForceMode2D.Impulse);

            }
            else if (BulletNum == 2)
            {
                rb.AddForce(Vector3.down.normalized * 1f, ForceMode2D.Impulse);
            }
            else if (BulletNum == 3)
            {
                rb.AddForce(Vector3.left.normalized * 1f, ForceMode2D.Impulse);
            }
            else if (BulletNum == 4)
            {
                rb.AddForce(Vector3.right.normalized * 1f, ForceMode2D.Impulse);
            }
            else if (BulletNum == 9)
            {
                StartCoroutine(waitingshot());
            }
        }

    }
    private IEnumerator waitingshot()
    {
        yield return new WaitForSeconds(1f);
        rb.angularVelocity = 180;
        rb.AddForce(PlayerDirection.normalized * 1f, ForceMode2D.Impulse);
    }
    private void FixedUpdate()
    {
        DestoryTime -= 1;
        if (DestoryTime < 0)
        {
            StartCoroutine(DestroyBullet());
        }
        //transform.Translate(Vector3.up * BulletSpeed * Time.deltaTime);
        
        if (BulletNum != 0|| BulletNum != 1 || BulletNum != 2 || BulletNum != 3 || BulletNum != 4)
        {
            
            if (BulletNum == 5)
            {
                transform.Translate(Vector3.up.normalized * BulletSpeed * Time.deltaTime);
            }
            else if (BulletNum == 6)
            {
                transform.Translate(Vector3.down.normalized * BulletSpeed * Time.deltaTime);
            }
            else if (BulletNum == 7)
            {
                transform.Translate(Vector3.right.normalized * BulletSpeed * Time.deltaTime);
            }
            else if (BulletNum == 8)
            {
                transform.Translate(Vector3.left.normalized * BulletSpeed * Time.deltaTime);
            }
        }

    }
}
