using System.Collections;
using UnityEngine;

public class UndyingEffect : MonoBehaviour
{
    public Player player; // 플레이어

    private float maxHealth; // 최대 체력
    private float currentHealth; // 현재 체력
    private bool undyingActive; // 불사 효과 활성화 여부

    private void Start() { player = GetComponent<PotionEffect>().player; }

    public void Undying(float duration) // 불사 효과 적용
    {
        if (!undyingActive)
        {
            undyingActive = true;

            maxHealth = player.MaxHP;
            currentHealth = player.CurrentHp;

            StartCoroutine(EndUndyingCoroutine(duration)); // 아이템 지속시간만큼 효과 부여
        }
    }

    // 현재 체력이 최대 체력의 20% 미만인 경우 체력 감소하지 않음 ← 조금 이상해서 나중에 수정
    private void Update() { if (undyingActive && currentHealth < maxHealth * 0.2f) { player.CurrentHp = currentHealth; } }

    private IEnumerator EndUndyingCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration); // 지정된 시간만큼 대기

        undyingActive = false; // 효과 종료
    }
}