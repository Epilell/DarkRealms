using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float mobAttackRange = 2f;
    //public MobMove mobMove;
    public Animator animator;

    private bool isPlayerInRange = false;

    private void Start()
    {
        // player를 찾아서 설정합니다.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // player와의 거리를 계산합니다.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // player가 일정 거리 안에 있으면 추적합니다.
        if (distanceToPlayer < detectionRange)
        {
            isPlayerInRange = true;
            transform.LookAt(player);
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        else
        {
            isPlayerInRange = false;
            //mobMove();
        }

        // player가 일정 거리 안에 있고 MobAttackRange 범위 안에 있으면 MobAttack스크립트를 호출합니다.
        if (isPlayerInRange && distanceToPlayer < mobAttackRange)
        {
            animator.SetTrigger("Attack");
        }
    }
}
