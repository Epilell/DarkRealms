using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPopDamage : MonoBehaviour
{
    private float Damage;
    private bool canDamage = false;
    public void SetStats(float damage)
    {
        this.Damage = damage;
    }
    private void Awake()
    {
        Destroy(gameObject, 3.625f);
    }
    private void Start()
    {
        EnableDamage2();
        Invoke("EnableDamage", 1.1f);
        Invoke("DisableDamage", 2.6f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDamage)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().P_TakeDamage(Damage);
            }
        }
    }

    private void EnableDamage()
    {
        canDamage = true;
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;
        rb.gravityScale = 0f;
        collider.offset=new Vector2(0,-6);
        collider.radius = 3f;
    }
    private void EnableDamage2()
    {
        canDamage = true;
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        rb.gravityScale = 0f;
        collider.offset = new Vector2(0, -1);
        collider.size = new Vector2(3, 6);
        Destroy(collider,1f);
        Destroy(rb, 1f);
    }
    private void DisableDamage()
    {
        canDamage = false;
    }
}
