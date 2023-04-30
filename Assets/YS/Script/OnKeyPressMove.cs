using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnKeyPressMove : MonoBehaviour  // 강의자료 스크립트에서 가져옴
{
    public float speed = 2; // 속도：Inspector에 지정

    float vx = 0;
    float vy = 0;
    bool leftFlag = false;

    void Update()
    { // 계속 시행한다
        vx = 0;
        vy = 0;
        if (Input.GetKey("d"))// 만약 오른쪽 키가 눌리면
        {
            vx = speed; // 오른쪽으로 나아가는 이동량을 넣는다
            leftFlag = false;
        }
        if (Input.GetKey("a"))// 만약 왼쪽 키가 눌리면
        {
            vx = -speed; // 왼쪽으로 나아가는 이동량을 넣는다
            leftFlag = true;
        }
        if (Input.GetKey("w"))// 만약 위 키가 눌리면
        {
            vy = speed; // 위로 나아가는 이동량을 넣는다
        }
        if (Input.GetKey("s"))// 만약 아래 키가 눌리면
        {
            vy = -speed; // 아래로 나아가는 이동량을 넣는다
        }
    }
    void FixedUpdate()// 계속 시행한다(일정 시간마다)
    {
        // 이동한다
        this.transform.Translate(vx / 50, vy / 50, 0);
        // 왼쪽 오른쪽 방향을 바꾼다
        this.GetComponent<SpriteRenderer>().flipX = leftFlag;
    }
}