using UnityEngine;

public class PortalUse : MonoBehaviour
{
    [SerializeField] private GameObject portalPrefab; // 포탈 프리팹

    public void SpawnPortal()
    {
        // 플레이어 위치 받기
        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;

        int random = Random.Range(0, 2);

        // 포탈 생성
        if (random == 0) { Instantiate(portalPrefab, playerPosition + new Vector3(2, 0, 0), Quaternion.identity); }
        else { Instantiate(portalPrefab, playerPosition + new Vector3(-2, 0, 0), Quaternion.identity); }

        /*
        // 마우스 커서의 스크린 좌표를 월드 좌표로 변환
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 플레이어에서 마우스 커서까지의 방향 계산
        Vector3 direction = (mousePosition - playerPosition).normalized;

        Vector3 spawnOffset = (direction.x < 0) ? new Vector3(-5, 0, 0) : new Vector3(5, 0, 0);

        // 포탈을 생성할 위치 계산 (플레이어 위치에서 방향으로 5만큼 떨어진 위치)
        Vector3 spawnPosition = playerPosition + spawnOffset;

        Instantiate(portalPrefab, spawnPosition, Quaternion.identity);
        */
    }
}