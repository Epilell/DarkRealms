using UnityEngine;

public class MagneticField : MonoBehaviour
{
    public Player player;
    public Transform escape; // 출구

    public float initialRadius; // 초기 자기장 반지름
    public float decreaseSpeed; // 자기장 감소 속도
    private float currentRadius; // 현재 자기장 반지름

    public float damage = 1; // 피해량
    private float damageTimer; // 타이머
    private bool isPlayerInsideField; // 플레이어가 자기장 내부에 있는지 여부

    private void Start()
    {
        currentRadius = initialRadius; // 반지름 초기화
        transform.position = escape.position; // 자기장 중심 위치를 출구 위치로 설정
        damageTimer = 1f; // 초기 피해 입히는 타이머 설정
    }

    private void Update()
    {
        if (decreaseSpeed < 1) { decreaseSpeed = 1; }

        // Debug.Log("영역 감소 속도: " + decreaseSpeed);
        currentRadius -= decreaseSpeed * Time.deltaTime; // 자기장 크기 감소

        if (currentRadius <= 0f)
        {
            currentRadius = 0f; // 반지름 0이하로 내려가지 않게 함
            isPlayerInsideField = false; // 반지름이 0이 되면 플레이어는 자기장 내부에 없다고 판단
        }
        else { isPlayerInsideField = Vector3.Distance(transform.position, player.transform.position) <= currentRadius; } // 플레이어가 자기장 내부에 있는지 확인

        transform.localScale = new Vector3(currentRadius * 2, currentRadius * 2, 1f); // 자기장 크기 조정: Scale값이 지름이므로 2를 곱해줌

        if (!isPlayerInsideField) // 플레이어가 자기장 밖이면
        {
            damageTimer -= Time.deltaTime; // 타이머 감소

            if (damageTimer <= 0f) // 1초 지나면
            {
                player.P_TakeDamage(damage); // 플레이어에게 피해를 입힘
                damageTimer = 1f; // 타이머 재설정
            }
        }
        /*Debug.Log("원 반지름: " + currentRadius);
        Debug.Log("자기장 중심과의 거리: " + Vector3.Distance(transform.position, player.transform.position));*/
    }
}