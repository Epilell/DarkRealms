using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSystem : MonoBehaviour
{
    public static PlayerSpawnSystem instance;
    public int SpawnPoint;

    [SerializeField]
    private List<GameObject> SpawnBases = new List<GameObject>();
    private void SpawnPointSet()
    {
        for(int i =0; i < SpawnBases.Count; i++)
        {
            data.SpawnPoints[i] = SpawnBases[i].transform.position;
        }
    }


    public void Start()
    {
        SpawnPointSet();
        SpawnPlayer();
    }

    [SerializeField]
    private PlayerSpawnData data;

    [SerializeField]
    private GameObject playerObject;

    /// <summary>
    /// 랜덤 생성위치 리턴
    /// </summary>
    /// <param name="_data"></param>
    private int SetSpawnPoint(PlayerSpawnData _data)
    {
        return Random.Range(0, _data.SpawnPoints.Count);
    }

    /// <summary>
    /// 플레이어 생성
    /// </summary>
    public void SpawnPlayer()
    {
        SpawnPoint = SetSpawnPoint(data);
        Debug.Log("PSS = " + SpawnPoint);
        Instantiate(playerObject, data.SpawnPoints[SpawnPoint], playerObject.transform.rotation);
    }

}
