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
        //MainCamera 태그를 갖는 오브젝트 탐색 후 Camera 컴포넌트 정보전달
        //GameObject.FindGameObjectWithTag("MainCamera").GetComponetn<Camera>();와 동일
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
            //카메라 위치에서 화면의 마우스위치를 관통하는 광선 생성
            //ray.origin : 광선의 시작위치, ray.direction : 광선의 진행방향
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //2D 모니터를 통해 3D월드의 오브젝트를 마우스로 선택하는 방법
            //광선에 부딪히는 오브젝트를 검출하여 hit에 저장
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform = hit.transform;
                //광선에 부딪힌 오브젝트의 태그가 Slot이면
                if (hit.transform.CompareTag("Slot"))
                {
                    //아이탬 위치를 그 슬롯으로 옮김
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
