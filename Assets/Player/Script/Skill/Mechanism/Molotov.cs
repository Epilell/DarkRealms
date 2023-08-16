using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Molotov : MonoBehaviour
{
    //Field
    #region

    //ȭ���� ������
    public MolotovData data;

    //ȭ�� ����
    private float r = 255, g = 255, b = 255;

    //ȭ���� �̵� ����
    private Vector3 targetPos;
    private float meterPerSec = 0.1f;
    private float elapsedTime = 0f;
    private float completePercentage = 0f;

    //�ӽ� ���׷��̵� ����
    private float tDamage = 0f, tRadius = 0f;

    #endregion

    //Method
    #region

    /// <summary>
    /// ȭ���� ���� ���� ����
    /// </summary>
    /// <param name="_target">���� ���� ��ǥ</param>
    public void SetCourse(Vector3 _target)
    {
        targetPos = _target;
    }

    /// <summary>
    /// ȭ���� ������ ����
    /// </summary>
    /// <param name="_r">R</param>
    /// <param name="_g">G</param>
    /// <param name="_b">B</param>
    public void SetRGB(float _r, float _g, float _b)
    {
        r = _r; g = _g; b = _b;
    }

    /// <summary>
    /// �ΰ��� �ӽ� ���� ����
    /// </summary>
    /// <param name="_dmg">_dmg%��ŭ ������ ����(��)</param>
    /// <param name="_rad">_rad%��ŭ ���� ����(��)</param>
    public void AddTempStats(float _dmg, float _rad)
    {
        tDamage += _dmg; tRadius += _rad;
    }

    //ȭ���� ��ô �� ȭ�� ����
    private void MakeFire()
    {
        //FindObjectOfType<SoundManager>().PlaySound("Molotov");
        GameObject fire = Instantiate(data.firePrefab, transform.position, transform.rotation);
        fire.GetComponent<SpriteRenderer>().material.color = new Color(r, g, b);
        fire.GetComponent<Fire>().SetFireStats
            (data.Damage * (1 + tDamage / 100) <= 0 ? data.Damage : data.Damage * (1 + tDamage / 100), 
            data.CCDuration, 
            data.Radius * (1 + tRadius/100) <= 0 ? data.Radius : data.Radius * (1 + tRadius / 100));
        Destroy(gameObject);
    }

    #endregion

    //Unity Event
    #region

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float distance = Vector3.Distance(targetPos, transform.position);
        completePercentage = (elapsedTime / (distance / meterPerSec));
        transform.position = Vector3.Lerp(transform.position, targetPos, completePercentage);
        if (transform.position == targetPos) { MakeFire(); }
    }

    #endregion

}
