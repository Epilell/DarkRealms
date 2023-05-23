using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObstacle : MonoBehaviour
{
    public List<GameObject> obstacles; // 장애물 리스트
    public Transform exit; // 탈출구

    private void Start()
    {
        GameObject nearesObstacle = null; // 가장 가까운 장애물 저장
        float nearesDistance = float.MaxValue; // 가장 가까운 장애물과의 거리 저장

        // 모든 장애물을 순회하며 가장 가까운 장애물을 찾음
        foreach (GameObject obstacle in obstacles)
        {
            float distance = Vector3.Distance(obstacle.transform.position, exit.position); // 장애물과 탈출구 사이의 거리 계산
            if (distance < nearesDistance)
            {
                nearesObstacle = obstacle; // 가장 가까운 장애물로 변수를 업데이트
                nearesDistance = distance; // 가장 가까운 거리로 변수를 업데이트
            }
        }

        if (nearesObstacle != null) { nearesObstacle.SetActive(false); } // 가장 가까운 장애물 비활성화
    }
}