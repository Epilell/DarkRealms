using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    public float interactionDistance = 2.0f; // 플레이어와 상호작용 가능한 거리
    private Animator animator; // 애니메이터 컴포넌트
    [Header("돌릴 포인트")]
    public GameObject ArrowPoint;//목표를 향하는 화살표
    [Header("SpriteRenderer쓸 화살표")]
    public GameObject arrow;
    private SpriteRenderer srA;

    private GameObject escapeTarget;//탈출구 포탈 게임오브젝트
    private GameObject bossTarget;//보스룸 포탈 게임오브젝트
    private Vector3 direction;//target좌표
    public bool isEscape;

    private GameObject player; // 플레이어 오브젝트

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        escapeTarget = GameObject.FindGameObjectWithTag("Escape");
        bossTarget = GameObject.FindGameObjectWithTag("BossPortal");
        if (isEscape)
        {
            direction = escapeTarget.transform.position - transform.position;
        }
        else
        {
            direction = bossTarget.transform.position - transform.position;
        }
        //화살표 방향설정
        srA = arrow.GetComponent<SpriteRenderer>();
        StartCoroutine(colorChanger());
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        ArrowPoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90f));
    }

    private void Update()
    {
        // 플레이어와의 거리 계산
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // F 키를 누르면 애니메이션 실행
        if (distanceToPlayer <= interactionDistance && Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("On", true); // "Interact"라는 트리거 이름 사용 
            StartCoroutine(colorChanger(1f));
        }
        if (distanceToPlayer > interactionDistance)
        {
            animator.SetBool("On", false);
            StartCoroutine(colorChanger());
        }
    }
    private IEnumerator colorChanger(float alpha = 0)
    {
        if (alpha == 0)
        {
            yield return new WaitForSeconds(0.01f);
            Color c = srA.color;
            c.a = alpha;
            srA.color = c;
        }
        else
        {
            yield return new WaitForSeconds(1.8f);
            Color c = srA.color;
            c.a = alpha;
            srA.color = c;
        }

    }
}
