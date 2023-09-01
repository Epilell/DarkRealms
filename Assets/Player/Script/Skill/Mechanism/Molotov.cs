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

    private Animator ani;

    private Color color = new(1f, 1f, 1f, 1f);

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
    /// �ΰ��� �ӽ� ���� ����
    /// </summary>
    /// <param name="_dmg">_dmg%��ŭ ������ ����(��)</param>
    /// <param name="_rad">_rad%��ŭ ���� ����(��)</param>
    public void AddTempStats(float _dmg, float _rad)
    {
        tDamage += _dmg; tRadius += _rad;
    }

    /// <summary>
    /// ȭ�� ���� ����
    /// </summary>
    /// <param name="_color">RGBA</param>
    public void SetColor(Color _color)
    {
        color = _color;
    }

    //ȭ���� ��ô �� ȭ�� ����
    private void MakeFire()
    {
        //FindObjectOfType<SoundManager>().PlaySound("Molotov");
        GameObject fire = Instantiate(data.firePrefab, transform.position, new Quaternion(0,0,0,0));
        fire.GetComponent<Renderer>().material.color = color;
        fire.GetComponent<Fire>().SetFireStats
            (data.Damage * (1 + tDamage / 100) <= 0 ? data.Damage : data.Damage * (1 + tDamage / 100),  // ������
            data.CCDuration,                                                                            // �����̻� ���ӽð�
            data.Radius * (1 + tRadius / 100) <= 0 ? data.Radius : data.Radius * (1 + tRadius / 100));   // ȭ�� ����                                                                                   // ȭ�� ����
        Destroy(gameObject);
    }

    private IEnumerator TransitionIntoFire()
    {
        ani.SetTrigger("IsExplode");
        yield return new WaitForSecondsRealtime(0.167f);
        MakeFire();
    }

    #endregion

    //Unity Event
    #region

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float distance = Vector3.Distance(targetPos, transform.position);
        completePercentage = (elapsedTime / (distance / meterPerSec));
        transform.position = Vector3.Lerp(transform.position, targetPos, completePercentage);
        if (transform.position == targetPos) { StartCoroutine(TransitionIntoFire()); }
    }

    #endregion

}
