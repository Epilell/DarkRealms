using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPopDamage : MonoBehaviour
{
    private float Damage;
    public void SetStats(float damage)
    {
        this.Damage = damage;
    }
    private void Awake()
    {
        Destroy(gameObject, 4f);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().P_TakeDamage(Damage);
        }
    }
}
