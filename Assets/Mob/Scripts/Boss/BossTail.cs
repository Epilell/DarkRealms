using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTail : MonoBehaviour
{
    private float Damage;
    private bool canDamage = false;
    public void SetStats(float damage)
    {
        this.Damage = damage;
    }
    private void Awake()
    {
        Destroy(gameObject, 2.8f);
    }
    private void Start()
    {
        Invoke("EnableDamage", 0.7f);
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
        collider.isTrigger = true;
        rb.gravityScale = 0f;
        collider.radius = 1.5f;
        GetComponent<Animator>().SetTrigger("TailAtk");
    }
}
