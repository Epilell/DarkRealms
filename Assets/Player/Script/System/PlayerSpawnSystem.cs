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
    /// ���� ������ġ ����
    /// </summary>
    /// <param name="_data"></param>
    private int SetSpawnPoint(PlayerSpawnData _data)
    {
        return Random.Range(0, _data.SpawnPoints.Count);
    }

    /// <summary>
    /// �÷��̾� ����
    /// </summary>
    public void SpawnPlayer()
    {
        Instantiate(playerObject, data.SpawnPoints[SetSpawnPoint(data)], playerObject.transform.rotation);
    }

}
