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

    /// <summary>
    /// 화염 능력치 설정
    /// </summary>
    /// <param name="_dmg">데미지</param>
    /// <param name="_dur">지속시간</param>
    /// <param name="_rad">범위</param>
    public void SetFireStats(float _dmg, float _dur, float _rad)
    {
        Damage = _dmg; Duration = _dur; Radius = _rad;
    }

    //화염 제거
    private void Extinguish()
    {
        Destroy(gameObject);
        FindObjectOfType<SoundManager>().StopSound("Molotov");
    }

    #endregion

    //unity event
    #region

    //화상 상태이상 적용
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
    #endregion
}
