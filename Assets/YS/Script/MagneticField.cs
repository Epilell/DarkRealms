using UnityEngine;

public class MagneticField : MonoBehaviour
{
    public Player player;
    public Transform escape; // 출구
    public GameObject filter;

    public float initialRadius = 600; // 초기 자기장 반지름: 나중에 private로 바꿀 거임
    public float decreaseSpeed = 1; // 자기장 감소 속도
    private float currentRadius; // 현재 자기장 반지름

    public float damage = 5; // 피해량
    private float damageTimer; // 타이머

    private bool isPlayerInsideField; // 플레이어가 자기장 내부에 있는지 여부

    private void Start()
    {
        filter.SetActive(false);
        currentRadius = initialRadius; // 반지름 초기화
        escape = GameObject.FindWithTag("Escape").transform;
        transform.position = escape.position; // 자기장 중심 위치를 출구 위치로 설정
        damageTimer = 1f; // 초기 피해 입히는 타이머 설정
    }

    private void Update()
    {
        currentRadius -= decreaseSpeed * Time.deltaTime; // 자기장 크기 감소

        if (currentRadius <= 0f)
        {
            currentRadius = 0f; // 반지름 0이하로 내려가지 않게 함
            isPlayerInsideField = false; // 반지름이 0이 되면 플레이어는 자기장 내부에 없다고 판단
        }
        else { isPlayerInsideField = Vector3.Distance(transform.position, player.transform.position) <= transform.localScale.x * 2.5; } // 플레이어가 자기장 내부에 있는지 확인

        transform.localScale = new Vector3(currentRadius, currentRadius, 1f); // 자기장 크기 조정: Scale값이 지름

        if (!isPlayerInsideField) // 플레이어가 자기장 밖이면
        {
            filter.SetActive(true);

            damageTimer -= Time.deltaTime; // 타이머 감소

            if (damageTimer <= 0f) // 1초 지나면
            {
                player.P_TakeDamage(damage); // 플레이어에게 피해를 입힘
                damageTimer = 1f; // 타이머 재설정
            }
        }
        else { filter.SetActive(false); }
    }
}