using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Molotov : MonoBehaviour
{
    public MolotovData data;

    private Vector3 targetPos;
    private float meterPerSec = 0.1f;
    private float elapsedTime;
    private float completePercentage;

    //È­¿°º´ µµÂø ÁöÁ¡ ¼³Á¤
    public void SetCourse(Vector3 _target)
    {
        targetPos = _target;
    }

    //È­¿°º´ ÅõÃ´ ÈÄ È­¿° »ý¼º
    private void MakeFire()
    {
        FindObjectOfType<SoundManager>().PlaySound("Molotov");
        GameObject molotov = Instantiate(data.firePrefab, transform.position, transform.rotation);
        molotov.GetComponent<Fire>().SetStats(data.Damage, data.TickDamage, data.MaxTime, data.Radius);
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
