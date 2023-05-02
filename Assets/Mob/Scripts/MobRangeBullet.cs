using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobRangeBullet : MonoBehaviour
{
    private float BulletSpeed;
    private float Damage;
    //private float Distance;
    Transform PlayerDirection;
    int BulletNum;

    //능력치 가져오기
    public void SetStats(float bulletSpeed, float damage, Transform P_direction, int bulletNum)
    {
        this.BulletSpeed = bulletSpeed;
        this.Damage = damage;
        this.PlayerDirection = P_direction;
        this.BulletNum = bulletNum;
        //this.Distance = distance;
    }
    public void OnCollisionEnter2D(Collision2D collision)
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
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        //transform.Translate(direction * BulletSpeed * Time.deltaTime);
        if (PlayerDirection == null)
        {
            if (BulletNum == 1)
            {
                transform.Translate(Vector3.up * BulletSpeed * Time.deltaTime);
            }
            else if (BulletNum == 2)
            {
                transform.Translate(Vector3.down * BulletSpeed * Time.deltaTime);
            }
            else if (BulletNum == 3)
            {
                transform.Translate(Vector3.right * BulletSpeed * Time.deltaTime);
            }
            else if (BulletNum == 4)
            {
                transform.Translate(Vector3.left * BulletSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(PlayerDirection.position * BulletSpeed * Time.deltaTime);
        }
    }
}
