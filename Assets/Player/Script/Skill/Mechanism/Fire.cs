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
    /// ȭ�� �ɷ�ġ ����
    /// </summary>
    /// <param name="_dmg">������</param>
    /// <param name="_dur">���ӽð�</param>
    /// <param name="_rad">����</param>
    public void SetFireStats(float _dmg, float _dur, float _rad)
    {
        Damage = _dmg; Duration = _dur; Radius = _rad;
    }

    //ȭ�� ����
    private void Extinguish()
    {
        Destroy(gameObject);
        FindObjectOfType<SoundManager>().StopSound("Molotov");
    }

    #endregion

    //unity event
    #region

    //ȭ�� �����̻� ����
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
