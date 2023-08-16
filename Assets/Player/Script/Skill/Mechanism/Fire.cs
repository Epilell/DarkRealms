using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //Field
    #region

    private float Damage, Duration, Radius;

    #endregion

    //Method
    #region

    //ȭ�� �ɷ�ġ ����
    public void SetFireStats(float _dmg, float _dur, float _ran)
    {
        Damage = _dmg; Duration = _dur; Radius = _ran;
    }

    //�� ����Ʈ ����
    private void Extinguish()
    {
        Destroy(gameObject);
    }

    #endregion

    //unity event
    #region

    //ȭ�� CC�� ����
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob"))
        {
            collision.GetComponent<MobHP>().TakeCC("burn", Duration, Damage);
        }
    }

    private void Start()
    {
        this.GetComponent<Transform>().localScale *= Radius;
        Invoke("Extinguish", Duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion
}
