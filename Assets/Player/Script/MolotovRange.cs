using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovRange : MonoBehaviour
{
    private float damage;
    private float timer;

    public void setDamage(float _damage)
    {
        damage = _damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {
            collision.gameObject.GetComponent<MobHP>().TakeDamage(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob") && timer >= 0.5f)
        {
            collision.gameObject.GetComponent<MobHP>().TakeDamage(damage);
            timer = 0f;
        }
    }

    private void DestoryEffect()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        Invoke("DestoryEffect", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0.5f)
        {
            timer += Time.deltaTime;
        }
    }
}
