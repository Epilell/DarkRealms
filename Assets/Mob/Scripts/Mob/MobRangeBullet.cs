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
    float DestoryTime = 250f;

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
            collision.gameObject.GetComponent<Player>().P_TakeDamage(Damage);
            DestroyBullet();
        }
        else if (collision.gameObject.CompareTag("Map"))
        {
            DestroyBullet();
        }
        else if (collision.gameObject.CompareTag("BossMob"))
        {
            DestroyBullet();
        }
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
    private void Start()
    {
        if (BulletNum != 0)
        {

            if (BulletNum == 1)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(Vector3.up * 2f, ForceMode2D.Impulse);
                //transform.Translate(Vector3.up * BulletSpeed * Time.deltaTime);
            }
            else if (BulletNum == 2)
            {
                //transform.Translate(Vector3.down * BulletSpeed * Time.deltaTime);
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(Vector3.down * 2f, ForceMode2D.Impulse);
            }
            else if (BulletNum == 3)
            {
                //transform.Translate(Vector3.right * BulletSpeed * Time.deltaTime);
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(Vector3.left * 2f, ForceMode2D.Impulse);
            }
            else if (BulletNum == 4)
            {
                //transform.Translate(Vector3.left * BulletSpeed * Time.deltaTime);
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(Vector3.right * 2f, ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        DestoryTime -= 1;
        if (DestoryTime < 0)
        {
            DestroyBullet();
        }
        //transform.Translate(direction * BulletSpeed * Time.deltaTime);
        /*
        if (BulletNum != 0)
        {
            
            if (BulletNum == 1)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(Vector3.up * 0.3f, ForceMode2D.Impulse);
                //transform.Translate(Vector3.up * BulletSpeed * Time.deltaTime);
            }
            else if (BulletNum == 2)
            {
                //transform.Translate(Vector3.down * BulletSpeed * Time.deltaTime);
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(Vector3.down * 0.3f, ForceMode2D.Impulse);
            }
            else if (BulletNum == 3)
            {
                //transform.Translate(Vector3.right * BulletSpeed * Time.deltaTime);
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(Vector3.left * 0.3f, ForceMode2D.Impulse);
            }
            else if (BulletNum == 4)
            {
                //transform.Translate(Vector3.left * BulletSpeed * Time.deltaTime);
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(Vector3.right * 0.3f, ForceMode2D.Impulse);
            }
        }
        else
        {
            //transform.Translate(PlayerDirection * -BulletSpeed * Time.deltaTime);
        }*/
    }
}
