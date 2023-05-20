using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ThrowMolotov : MonoBehaviour
{
    public MolotovData data;

    private Vector3 targetPos;
    private float meterPerSec = 0.1f;
    private float elapsedTime;
    private float completePercentage;

    //ȭ���� ���� ���� ����
    public void SetCourse(Vector3 _target)
    {
        targetPos = _target;
    }

    //ȭ���� ��ô �� ȭ�� ����
    private void MakeFire()
    {
        GameObject molotov = Instantiate(data.firePrefab, transform.position, transform.rotation);
        molotov.GetComponent<Molotov>().
            SetStats(data.upgradeList[data.upgradeNum].impactDamage, data.upgradeList[data.upgradeNum].tickDamage,
            data.upgradeList[data.upgradeNum].maxTime, data.upgradeList[data.upgradeNum].radius);
        Destroy(gameObject);
    }


    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float distance = Vector3.Distance(targetPos, transform.position);
        completePercentage = (elapsedTime / (distance / meterPerSec));
        transform.position = Vector3.Lerp(transform.position, targetPos, completePercentage);
        if (transform.position == targetPos) { MakeFire(); }
    }
}
