using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.up;//*500f;
    private Transform targetTransform;
    private RectTransform rectTransform; // UI ��ġ ���� ����
    private float correctionFloat=4.5f;

    public void Setup(Transform target,float correction=4.5f)
    {
        //Slider UI�� �Ѿƴٴ� target ����
        targetTransform = target;
        //RectTransform ������Ʈ ���� ������
        rectTransform = GetComponent<RectTransform>();
        correctionFloat = correction;
    }


    //LateUpdate = ��� Update �Լ� ȣ�� �� ���������� ȣ��
    private void LateUpdate()
    {
        //���� �ı��� ��� slider Ui�� ����
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        //������Ʈ ��ġ ���� �� Slider UI�� ��ġ�� �����ϰ� ��ġ

        //������Ʈ�� ���� ��ǥ�� �������� ȭ�鿡�� ��ǥ �� ����
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        //ȭ�� ������ ��ǥ + distance��ŭ ������ ��ġ�� Slider Ui ��ġ�� ����
        rectTransform.position = screenPosition - distance*correctionFloat;
        //Debug.Log(rectTransform.position);
    }
}
