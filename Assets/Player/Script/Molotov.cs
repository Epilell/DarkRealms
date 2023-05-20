using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    private float impactDmg, tickDmg, maxTime, radius;
    //�ʱ� ���� ���� ��� ����
    public Collider2D[] colliders;
    //Ȱ��ȭ ���ο�
    private bool IsActive = false;
    //Ÿ�̸�
    private float timer = 0f;

    //ȭ�� �ɷ�ġ ����
    public void SetStats(float _impactDmg, float _tickDmg, float _maxTime, float _radius)
    {
        impactDmg = _impactDmg; tickDmg = _tickDmg; maxTime = _maxTime; radius = _radius;
    }

    private void ApplyDamage(Collider2D[] collider, float _damage)
    {
        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].gameObject.CompareTag("Mob"))
            {
                collider[i].gameObject.GetComponent<MobHP>().TakeDamage(_damage);
            }
        }
    }

    private void DestoryEffect()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {

    }

    private void Start()
    {
        Invoke("DestoryEffect", maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive == false)
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            ApplyDamage(colliders, impactDmg);
            IsActive = true;
        }
        else
        {
            if (timer < 0.5f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                ApplyDamage(colliders, tickDmg);
                timer = 0f;
            }
        }
    }
}
