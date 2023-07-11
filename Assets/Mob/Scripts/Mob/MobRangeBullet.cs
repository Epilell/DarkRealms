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

    //�ɷ�ġ ��������
    /// <summary>
    /// �ɷ�ġ ����
    /// </summary>
    /// <param name="bulletSpeed">�Ѿ� �ӵ�</param>
    /// <param name="damage">������</param>
    /// <param name="P_direction">�÷��̾� ��ġ</param>
    /// <param name="bulletNum">�Ҹ��� ��ȣ</param>
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

    private void FixedUpdate()
    {
        DestoryTime -= 1;
        if (DestoryTime < 0)
        {
            DestroyBullet();
        }
        //transform.Translate(direction * BulletSpeed * Time.deltaTime);
        if (BulletNum != 0)
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
            transform.Translate(PlayerDirection * BulletSpeed * Time.deltaTime);
        }
    }
}
