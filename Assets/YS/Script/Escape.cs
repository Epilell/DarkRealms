using UnityEngine;

public class Escape : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어랑 충돌하면 페이드 아웃
        {
            StartCoroutine(FindObjectOfType<FadeOut>().FadeFlow());
        }
    }
}