using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    private float damage;

    private float destroyTime;

    //능력치 가져오기
    public void SetStats(float _damage, float _time)
    {
        this.damage = _damage; this.destroyTime = _time;
    }

    //------------------------------<공격>-------------------------------------
    public void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Mob":
                collider.gameObject.GetComponent<MobHP>().TakeDamage(damage);
                FindObjectOfType<BloodEffect>().Blood(damage);
                DestroyBullet();
                break;
            case "BossMob":
                collider.gameObject.GetComponent<BossHP>().TakeDamage(damage);
                FindObjectOfType<BloodEffect>().Blood(damage / 2);
                DestroyBullet();
                break;
            case "Map":
                DestroyBullet();
                break;
        }
    }

    //충돌 시 총알 제거
    public void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBullet();
    }

    //총알 제거
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke("DestroyBullet", destroyTime);
        rb.velocity = transform.up * 30;
    }
}
