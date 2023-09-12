using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCoverOpener : MonoBehaviour
{
    [SerializeField]
    private int covernum;
    private Map map;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Maps").GetComponent<Map>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            map.MapCoverOpen(covernum);
        }
    }
}
