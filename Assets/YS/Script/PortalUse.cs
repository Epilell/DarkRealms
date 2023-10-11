using UnityEngine;

public class PortalUse : MonoBehaviour
{
    [SerializeField] private GameObject portalPrefab; // ��Ż ������

    public void SpawnPortal()
    {
        // �÷��̾� ��ġ �ޱ�
        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;

        int random = Random.Range(0, 2);

        // ��Ż ����
        if (random == 0) { Instantiate(portalPrefab, playerPosition + new Vector3(2, 0, 0), Quaternion.identity); }
        else { Instantiate(portalPrefab, playerPosition + new Vector3(-2, 0, 0), Quaternion.identity); }

        /*
        // ���콺 Ŀ���� ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // �÷��̾�� ���콺 Ŀ�������� ���� ���
        Vector3 direction = (mousePosition - playerPosition).normalized;

        Vector3 spawnOffset = (direction.x < 0) ? new Vector3(-5, 0, 0) : new Vector3(5, 0, 0);

        // ��Ż�� ������ ��ġ ��� (�÷��̾� ��ġ���� �������� 5��ŭ ������ ��ġ)
        Vector3 spawnPosition = playerPosition + spawnOffset;

        Instantiate(portalPrefab, spawnPosition, Quaternion.identity);
        */
    }
}