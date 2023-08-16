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

    //화염병 데이터
    public MolotovData data;

    //화염 색상
    private float r = 255, g = 255, b = 255;

    //화염병 이동 변수
    private Vector3 targetPos;
    private float meterPerSec = 0.1f;
    private float elapsedTime = 0f;
    private float completePercentage = 0f;

    //임시 업그레이드 변수
    private float tDamage = 0f, tRadius = 0f;

    #endregion

    //Method
    #region

    /// <summary>
    /// 화염병 도착 지점 설정
    /// </summary>
    /// <param name="_target">도착 지점 좌표</param>
    public void SetCourse(Vector3 _target)
    {
        targetPos = _target;
    }

    /// <summary>
    /// 화염의 색상을 변경
    /// </summary>
    /// <param name="_r">R</param>
    /// <param name="_g">G</param>
    /// <param name="_b">B</param>
    public void SetRGB(float _r, float _g, float _b)
    {
        r = _r; g = _g; b = _b;
    }

    /// <summary>
    /// 인게임 임시 스탯 적용
    /// </summary>
    /// <param name="_dmg">_dmg%만큼 데미지 증가(합)</param>
    /// <param name="_rad">_rad%만큼 범위 증가(합)</param>
    public void AddTempStats(float _dmg, float _rad)
    {
        tDamage += _dmg; tRadius += _rad;
    }

    //화염병 투척 후 화염 생성
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
