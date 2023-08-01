using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMobSpawner : MonoBehaviour
{
    //몹은 플레이어 위치 주위로 낙하
    //결계로 주위 막힘
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

        }
    }
}
