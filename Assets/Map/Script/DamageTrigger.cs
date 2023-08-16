using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public PlayerData data;
    private int timer = 0;

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Dmage count");
            timer += 1;

            if (timer % 40 == 0)
            {
                Debug.Log("3");
                //GameObject.Find("Test Player").GetComponent<Player>().CurrentHp = GameObject.Find("Test Player").GetComponent<Player>().CurrentHp - 3;
                if (other.tag == "Player")
                {
                    other.GetComponent<Player>().CurrentHp -= 3f;

                }
            }

        }
    }
}
