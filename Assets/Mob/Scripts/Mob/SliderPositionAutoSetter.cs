using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.up;//*500f;
    private Transform targetTransform;
    private RectTransform rectTransform; // UI 위치 정보 제어
    private float correctionFloat=4.5f;

    public void Setup(Transform target,float correction=4.5f)
    {
        //Slider UI가 쫓아다닐 target 설정
        targetTransform = target;
        //RectTransform 컴포넌트 정보 얻어오기
        rectTransform = GetComponent<RectTransform>();
        correctionFloat = correction;
    }


    //LateUpdate = 모든 Update 함수 호출 후 마지막으로 호출
    private void LateUpdate()
    {
        //적이 파괴될 경우 slider Ui도 삭제
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        //오브젝트 위치 갱신 후 Slider UI도 위치를 동일하게 설치

        //오브젝트의 월드 좌표를 기준으로 화면에서 좌표 값 구현
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        //화면 내에서 좌표 + distance만큼 떨어진 위치를 Slider Ui 위치로 지정
        rectTransform.position = screenPosition - distance*correctionFloat;
        //Debug.Log(rectTransform.position);
    }
}
