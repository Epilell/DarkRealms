using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{/*
    //[SerializeField]
    //private TowerSpawner towerSpawner; Inventory?
    //[SerializeField]
    //private TowerDataViewer towerDataViewer;Slot?

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    private Transform hitTransform = null;
    // Start is called before the first frame update
    private void Awake()
    {
        //MainCamera �±׸� ���� ������Ʈ Ž�� �� Camera ������Ʈ ��������
        //GameObject.FindGameObjectWithTag("MainCamera").GetComponetn<Camera>();�� ����
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            //ī�޶� ��ġ���� ȭ���� ���콺��ġ�� �����ϴ� ���� ����
            //ray.origin : ������ ������ġ, ray.direction : ������ �������
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //2D ����͸� ���� 3D������ ������Ʈ�� ���콺�� �����ϴ� ���
            //������ �ε����� ������Ʈ�� �����Ͽ� hit�� ����
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform = hit.transform;
                //������ �ε��� ������Ʈ�� �±װ� Slot�̸�
                if (hit.transform.CompareTag("Slot"))
                {
                    //������ ��ġ�� �� �������� �ű�
                    //towerSpawner.SpawnTower(hit.transform);
                }
                else if (hit.transform.CompareTag("Tower"))
                {
                    towerDataViewer.OnPanel(hit.transform);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (hitTransform == null || hitTransform.CompareTag("Tower") == false)
            {
                towerDataViewer.OffPanel();
            }

            hitTransform = null;
        }
    }*/
}
