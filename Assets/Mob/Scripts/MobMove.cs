using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMove : MonoBehaviour
{
    Rigidbody2D rigid;
    private int xSpeed = 0;
    private int ySpeed = 0;
    public int MaxSpeed = 1;
    Animator anim;
    SpriteRenderer spriteRenderer;
    //int randomMoveTime;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("Think", 3f,3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(xSpeed, ySpeed);
    }
    //위치를 랜덤으로 이동, 재귀적으로 호출
    void Think()
    {
        xSpeed = Random.Range(-MaxSpeed, MaxSpeed+1);
        ySpeed = Random.Range(-MaxSpeed, MaxSpeed+1);
        int speed = 1;
        if (ySpeed == 0 && xSpeed == 0)
        {
                speed = 0;
        }
        anim.SetInteger("WalkSpeed",speed);
        //뒤집기
        if (speed != 0&& xSpeed > 0)
        {
                spriteRenderer.flipX = true;
        }
        //랜덤타임 뒤 재귀

        //randomMoveTime = Random.Range(1, 3);
        //Invoke("Think()", 3);
    }
}
