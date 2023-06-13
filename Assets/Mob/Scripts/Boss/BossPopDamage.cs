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
        Invoke("EnableDamage", 0.125f);
        Invoke("DisableDamage", 2.6f);
    }
    public void OnCollisionEnter2D(Collision2D collision)
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
        //collider.isTrigger = true;
        rb.gravityScale = 0f;
        collider.offset=new Vector2(0,-3);
        collider.radius = 3f;
        //GetComponent<Animator>().SetTrigger("BossPop");
    }
    private void DisableDamage()
    {
        canDamage = false;
    }
}
