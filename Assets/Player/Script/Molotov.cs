using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Molotov : MonoBehaviour
{
    private Vector3 targetPos;
    private float meterPerSec = 0.1f;
    private float elapsedTime;
    private float completePercentage;

    public void SetCourse(Vector3 _target)
    {
        targetPos = _target;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float distance = Vector3.Distance(targetPos, transform.position);
        completePercentage = (elapsedTime / (distance / meterPerSec));
        transform.position = Vector3.Lerp(transform.position, targetPos, completePercentage);
        if (transform.position == targetPos) { Destroy(gameObject); }
    }
}
