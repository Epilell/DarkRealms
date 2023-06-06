using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnTrigger : MonoBehaviour
{
    public GameObject Mob;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Nope");
            GameObject mob = Instantiate(Mob, transform.position, transform.rotation);
        }
    }
}
