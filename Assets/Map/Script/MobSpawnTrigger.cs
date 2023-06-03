using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnTrigger : MonoBehaviour
{
    public GameObject Skeleton;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Nope");
            GameObject mob = Instantiate(Skeleton, transform.position, transform.rotation);
        }
    }
}
