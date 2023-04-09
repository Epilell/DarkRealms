using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//일정 주기마다 radius 범위에 스폰 
public class MobExSpawnMnager : MonoBehaviour
{
    public GameObject mobExPrefab;
    public float spawnRadius;
    public float spawnInterval;

    void Start()
    {
        StartCoroutine(SpawnMobEx());
    }

    private IEnumerator SpawnMobEx()
    {
        while (true)
        {
            Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;
            spawnPosition.z = 0f;
            Instantiate(mobExPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Update()
    {
        
    }
}
