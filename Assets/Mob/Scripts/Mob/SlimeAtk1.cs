using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAtk1 : MonoBehaviour
{
    public bool atkOn = false;
    public MobStat mobStat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (atkOn)
        {
            if (collision.tag == "Player")
            {
                GetComponent<Collider>().GetComponent<Player>().P_TakeDamage(mobStat.mobDamage);
            }
        }
    }
}
