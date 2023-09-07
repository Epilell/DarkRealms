using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target; // 추적할 대상
    private RectTransform rectTransform; // 객체의 RectTransform을 저장

    public bool cameraSizeUp; // 카메라 시점 변경 유무(보스 스폰 유무)

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (cameraSizeUp) // 카메라 시점이 변경됐으면
        {
            // target의 위치를 카메라 뷰포트 좌표계로 변환하여 screenPoint에 저장. 화면 내에서의 상대적인 위치: 왼쪽 하단(0, 0), 오른쪽 상단(1, 1)
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.position); 
            
            // 우측 아래로 좌표값 이동
            screenPoint.x += 10f / Screen.width;
            screenPoint.y -= 40f / Screen.height;

            // 위에서 계산한 screenPoint 값을 RectTransform의 anchorMin과 anchorMax에 모두 적용.
            // anchorMin과 anchorMax가 같은 값일 경우 해당 점을 기준으로 UI 위치 변경
            rectTransform.anchorMin = screenPoint;
            rectTransform.anchorMax = screenPoint;
        }
    }
}