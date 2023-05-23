using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObstacle : MonoBehaviour
{
    public List<GameObject> obstacles; // ��ֹ� ����Ʈ
    public Transform exit; // Ż�ⱸ

    private void Start()
    {
        GameObject nearesObstacle = null; // ���� ����� ��ֹ� ����
        float nearesDistance = float.MaxValue; // ���� ����� ��ֹ����� �Ÿ� ����

        // ��� ��ֹ��� ��ȸ�ϸ� ���� ����� ��ֹ��� ã��
        foreach (GameObject obstacle in obstacles)
        {
            float distance = Vector3.Distance(obstacle.transform.position, exit.position); // ��ֹ��� Ż�ⱸ ������ �Ÿ� ���
            if (distance < nearesDistance)
            {
                nearesObstacle = obstacle; // ���� ����� ��ֹ��� ������ ������Ʈ
                nearesDistance = distance; // ���� ����� �Ÿ��� ������ ������Ʈ
            }
        }

        if (nearesObstacle != null) { nearesObstacle.SetActive(false); } // ���� ����� ��ֹ� ��Ȱ��ȭ
    }
}