using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHit : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            anim.SetInteger("IsHit", 1);
            OnDamaged(collision.transform.position);
        }
    }
    void OnDamaged(Vector2 targetPos)
    {
        //gameObject.layer = 11;//layer를 Mob 히트로 바꿔서 무적 구현

        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        //rigid.AddForce(new Vector2(dirc, 1)*5, ForceMode.Impulse);
        Invoke("OffDamaged", 1);
    }
    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        anim.SetInteger("IsHit", 0);
    }
}
