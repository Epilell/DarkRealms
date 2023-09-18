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

        // �Ʒ� �� ���� �ڱ��� ��ġ�� Ż�ⱸ�� ��
        /*FindObjectOfType<MagneticField>().escape = walls.transform.Find("TestExit");
        FindObjectOfType<MagneticField>().transform.position = FindObjectOfType<MagneticField>().escape.position;*/

        // �Ʒ��� ��Ż ��ġ ������
        FindObjectOfType<SoundManager>().portal= walls.transform.Find("TestExit");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
