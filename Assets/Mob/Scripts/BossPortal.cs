using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortal : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private GameObject bossRoom;
    //�浹�� ������Ʈ�� player�� ������ ��ġ�� �̵�
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            StartCoroutine(moveToBossRoom());
        }
    }
    private IEnumerator moveToBossRoom()
    {
        player.transform.position = bossRoom.transform.position;
        yield return new WaitForSeconds(0.1f);
    }
}
