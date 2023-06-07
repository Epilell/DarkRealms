using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSystem : MonoBehaviour
{
    public static PlayerSpawnSystem instance;

    public void Start()
    {
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
        Instantiate(playerObject, data.SpawnPoints[SetSpawnPoint(data)], playerObject.transform.rotation);
    }

}
