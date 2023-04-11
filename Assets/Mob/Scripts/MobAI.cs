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
        // player�� ã�Ƽ� �����մϴ�.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // player���� �Ÿ��� ����մϴ�.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // player�� ���� �Ÿ� �ȿ� ������ �����մϴ�.
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

        // player�� ���� �Ÿ� �ȿ� �ְ� MobAttackRange ���� �ȿ� ������ MobAttack��ũ��Ʈ�� ȣ���մϴ�.
        if (isPlayerInRange && distanceToPlayer < mobAttackRange)
        {
            animator.SetTrigger("Attack");
        }
    }
}
