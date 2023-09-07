using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target; // ������ ���
    private RectTransform rectTransform; // ��ü�� RectTransform�� ����

    public bool cameraSizeUp; // ī�޶� ���� ���� ����(���� ���� ����)

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (cameraSizeUp) // ī�޶� ������ ���������
        {
            // target�� ��ġ�� ī�޶� ����Ʈ ��ǥ��� ��ȯ�Ͽ� screenPoint�� ����. ȭ�� �������� ������� ��ġ: ���� �ϴ�(0, 0), ������ ���(1, 1)
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.position); 
            
            // ���� �Ʒ��� ��ǥ�� �̵�
            screenPoint.x += 10f / Screen.width;
            screenPoint.y -= 40f / Screen.height;

            // ������ ����� screenPoint ���� RectTransform�� anchorMin�� anchorMax�� ��� ����.
            // anchorMin�� anchorMax�� ���� ���� ��� �ش� ���� �������� UI ��ġ ����
            rectTransform.anchorMin = screenPoint;
            rectTransform.anchorMax = screenPoint;
        }
    }
}