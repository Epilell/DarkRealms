using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Spawn Point", menuName = "Map Data/Map Spawn Point Data")]
public class PlayerSpawnData : ScriptableObject
{
    private string mapname;
    public string Mapname => mapname;
    public List<Vector2> SpawnPoints = new List<Vector2>();
}
