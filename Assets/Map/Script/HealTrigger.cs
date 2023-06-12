using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTrigger : MonoBehaviour
{
    public P_Data data;
    //private float MaxHP;
    private int timer = 0;

    public void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("healcount");
            timer += 1;

            if (timer % 100 == 0)
            {
                Debug.Log("Heal");

                if (GameObject.FindWithTag("Player").GetComponent<Player>().CurrentHp <= 95)
                {
                    Debug.Log("5");
                    GameObject.FindWithTag("Player").GetComponent<Player>().CurrentHp = GameObject.FindWithTag("Player").GetComponent<Player>().CurrentHp + 5;
                }
                else if (95 < GameObject.FindWithTag("Player").GetComponent<Player>().CurrentHp)
                {
                    GameObject.FindWithTag("Player").GetComponent<Player>().CurrentHp
                        = GameObject.FindWithTag("Player").GetComponent<Player>().CurrentHp + (100 - GameObject.FindWithTag("Player").GetComponent<Player>().CurrentHp);
                    Debug.Log("N");
                }
                else
                {
                    Debug.Log("already");
                }
            }
        }
    }
}
