using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target; // ������ ���
    private RectTransform rectTransform; // ��ü�� RectTransform�� ����
    private Vector2 originalAnchorMin, originalAnchorMax; // �ʱⰪ �����

    public bool cameraChangeUp, cameraChangeDown; // ī�޶� ���� ���� ����

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rectTransform = GetComponent<RectTransform>();

        // �ʱⰪ ����
        originalAnchorMin = rectTransform.anchorMin;
        originalAnchorMax = rectTransform.anchorMax;
    }

    private void Update()
    {
        if (cameraChangeUp) // ī�޶� ������ ���������
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

        if (cameraChangeDown) // ī�޶� ���� ���� �� ���� ������ ����
        {
            rectTransform.anchorMin = originalAnchorMin;
            rectTransform.anchorMax = originalAnchorMax;
        }
    }
}