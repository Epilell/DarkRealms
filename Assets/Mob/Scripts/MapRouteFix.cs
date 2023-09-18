using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRouteFix : MonoBehaviour
{
    public List<GameObject> route;
    public PlayerSpawnSystem p_spawn;
    int routenum;
    // Start is called before the first frame update
    void Start()
    {
        p_spawn = GetComponent<PlayerSpawnSystem>();

        MakeWall();
    }
    private void MakeWall()
    {

        routenum = p_spawn.SpawnPoint;
        Debug.Log("MRF = " + routenum);
        GameObject walls = Instantiate(route[routenum], route[routenum].transform.position, Quaternion.identity);

        // 아래 두 줄은 자기장 위치를 탈출구로 함
        /*FindObjectOfType<MagneticField>().escape = walls.transform.Find("TestExit");
        FindObjectOfType<MagneticField>().transform.position = FindObjectOfType<MagneticField>().escape.position;*/

        // 아래는 포탈 위치 던져줌
        FindObjectOfType<SoundManager>().portal= walls.transform.Find("TestExit");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
