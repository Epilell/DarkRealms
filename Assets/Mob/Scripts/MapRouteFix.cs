using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRouteFix : MonoBehaviour
{
    public List<GameObject> route;
    public PlayerSpawnSystem p_spawn;
    int routenum = 1;
    // Start is called before the first frame update
    void Start()
    {
        p_spawn = GetComponent<PlayerSpawnSystem>();

        StartCoroutine("MakeWall", 2f);
    }
    private IEnumerator MakeWall()
    {
        yield return new WaitForSeconds(1f);

        routenum = p_spawn.SpawnPoint;
        Debug.Log("MRF = " + routenum);
        GameObject walls = Instantiate(route[routenum], route[routenum].transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
