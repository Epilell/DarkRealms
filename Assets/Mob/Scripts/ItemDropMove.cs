using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropMove : MonoBehaviour
{
    private float xpos;
    private float ypos;
    private Vector3 vec;
    // Start is called before the first frame update
    void Start()
    {
        xpos = Random.Range(-0.09f, 0.10f);
        ypos = Random.Range(0, 0.1f);
        vec.x = xpos;
        vec.y = ypos;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DropMoveStart()
    {
        StartCoroutine(DropMove());
    }
    private IEnumerator DropMove()
    {

        for(int i = 0; i < 8; i++)
        {
            transform.localPosition += vec;
            yield return new WaitForSeconds(0.05f);
        }
        vec.y -= ypos;
        vec.y -= ypos;
        for (int i = 0; i < 8; i++)
        {
            transform.localPosition += vec;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
