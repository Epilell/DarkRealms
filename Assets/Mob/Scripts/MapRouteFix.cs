using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRouteFix : MonoBehaviour
{
    public List<GameObject> route;
    public PlayerSpawnSystem p_spawn;
    int routenum = 1;
    // Start is called before the first frame update
    void Awake()
    {
        p_spawn = GetComponent<PlayerSpawnSystem>();

        MakeWall();
    }
    private void MakeWall()
    {

        routenum = p_spawn.SpawnPoint;
        Debug.Log("MRF = " + routenum);
        GameObject walls = Instantiate(route[routenum], route[routenum].transform.position, Quaternion.identity);
        
        // �Ʒ� �� ���� �ڱ��� ��ġ�� Ż�ⱸ�� �� - ys
        FindObjectOfType<MagneticField>().escape = walls.transform.Find("TestExit");
        FindObjectOfType<MagneticField>().transform.position = FindObjectOfType<MagneticField>().escape.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
