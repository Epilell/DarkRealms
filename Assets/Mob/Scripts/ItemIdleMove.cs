using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdleMove : MonoBehaviour
{
    Transform _localtransform;
    [SerializeField]
    private Vector3 moverange=new Vector3(0,0.1f,0);
    [SerializeField]
    private float moveTime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        _localtransform = transform;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            _localtransform.localPosition += moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition += moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition += moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition += moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition += moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition += moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition += moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition += moverange;
            yield return new WaitForSeconds(moveTime);


            _localtransform.localPosition -= moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition -= moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition -= moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition -= moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition -= moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition -= moverange;
            yield return new WaitForSeconds(moveTime);
            _localtransform.localPosition -= moverange;
            yield return new WaitForSeconds(moveTime);
        }
    }
}
