using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Point", menuName = "Map Data/Map Spawn Point Data")]
public class PlayerSpawnData: ScriptableObject
{
    private string mapname;
    public string Mapname => mapname;

    public List<Vector2> SpawnPoints = new List<Vector2>();
}

public class PlayerSpawnSystem : MonoBehaviour
{
    public static PlayerSpawnSystem instance;

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
