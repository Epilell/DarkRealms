using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObstacle : MonoBehaviour
{
    public Transform exit; // 탈출구
    public List<GameObject> obstacles; // 벽 리스트

    private void Start()
    {
        GameObject nearesObstacle = null; // 가장 가까운 벽 저장
        float nearesDistance = float.MaxValue; // 가장 가까운 벽과의 거리 저장

        // 리스트 순회하며 탈출구와 가장 가까운 벽을 찾음
        foreach (GameObject obstacle in obstacles)
        {
            float distance = Vector3.Distance(obstacle.transform.position, exit.position); // 벽과 탈출구 사이의 거리 계산
            if (distance < nearesDistance)
            {
                nearesObstacle = obstacle; // 가장 가까운 벽로 변수를 업데이트
                nearesDistance = distance; // 가장 가까운 거리로 변수를 업데이트
            }
        }

        if (nearesObstacle != null) { nearesObstacle.SetActive(false); } // 가장 가까운 벽 비활성화
    }
}