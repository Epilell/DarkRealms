using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnTrigger : MonoBehaviour
{
    public GameObject Mob;

    bool spawn_true;

    private void Start()
    {
        spawn_true = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (spawn_true == false)
            {
                //Debug.Log("Nope");
                GameObject mob = Instantiate(Mob, transform.position, transform.rotation);
                spawn_true = true;
            }
        }
    }
}
