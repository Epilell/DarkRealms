using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBreath : MonoBehaviour
{
    private float Damage;
    private bool canDamage = false;
    public void SetStats(float damage)
    {
        this.Damage = damage;
    }
    private void Awake()
    {
        Destroy(gameObject, 5f);
    }
    private void Start()
    {
        Invoke("EnableDamage", 0.01f);
    }
    public void OnTriggerEnter2D(Collider2D collision)
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
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        rb.gravityScale = 0f;
        collider.size = new Vector2(1f, 15f);
        GetComponent<Animator>().SetBool("BreathAtk",true);
    }
}
