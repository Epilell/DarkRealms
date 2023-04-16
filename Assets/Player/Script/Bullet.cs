using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float Speed;
    private float Damage;
    private float Distance;

    //능력치 가져오기
    public void SetStats(float speed, float damage, float distance)
    {
        this.Speed = speed;
        this.Damage = damage;
        this.Distance = distance;
    }

    private void Attack()
    {

    }

    //------------------------------<공격>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mob"))
        {

        }
        DestroyBullet();
    }

    //총알 삭제
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime) ;
    }
}
