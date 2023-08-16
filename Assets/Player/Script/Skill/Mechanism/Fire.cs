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

    //화염 능력치 설정
    public void SetFireStats(float _dmg, float _dur, float _ran)
    {
        Damage = _dmg; Duration = _dur; Radius = _ran;
    }

    //불 이펙트 제거
    private void Extinguish()
    {
        Destroy(gameObject);
    }

    #endregion

    //unity event
    #region

    //화상 CC기 적용
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
