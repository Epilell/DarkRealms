using UnityEngine;

public class Escape : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // �÷��̾�� �浹�ϸ� ���̵� �ƿ�
        {
            StartCoroutine(FindObjectOfType<FadeOut>().FadeFlow());
        }
    }
}