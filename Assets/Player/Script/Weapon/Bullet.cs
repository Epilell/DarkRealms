using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    private float damage;

    //�ɷ�ġ ��������
    public void SetStats(float _damage)
    {
        this.damage = _damage;
    }

    //------------------------------<����>-------------------------------------
    public void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Mob":
                collider.gameObject.GetComponent<MobHP>().TakeDamage(damage);
                DestroyBullet();
                break;
            case "BossMob":
                collider.gameObject.GetComponent<BossHP>().TakeDamage(damage);
                DestroyBullet();
                break;
            case "Map":
                DestroyBullet();
                break;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyBullet();
    }

    //�Ѿ� ����
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroyBullet", 2f);
    }

    private void Start()
    {
        rb.velocity = transform.up * 30;
    }
}
