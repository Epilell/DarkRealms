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

    private Animator ani;

    private Color color = new(1f, 1f, 1f, 1f);

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
    /// 인게임 임시 스탯 적용
    /// </summary>
    /// <param name="_dmg">_dmg%만큼 데미지 증가(합)</param>
    /// <param name="_rad">_rad%만큼 범위 증가(합)</param>
    public void AddTempStats(float _dmg, float _rad)
    {
        tDamage += _dmg; tRadius += _rad;
    }

    /// <summary>
    /// 화염 색상 변경
    /// </summary>
    /// <param name="_color">RGBA</param>
    public void SetColor(Color _color)
    {
        color = _color;
    }

    //화염병 투척 후 화염 생성
    private void MakeFire()
    {
        //FindObjectOfType<SoundManager>().PlaySound("Molotov");
        GameObject fire = Instantiate(data.firePrefab, transform.position, new Quaternion(0,0,0,0));
        fire.GetComponent<Renderer>().material.color = color;
        fire.GetComponent<Fire>().SetFireStats
            (data.Damage * (1 + tDamage / 100) <= 0 ? data.Damage : data.Damage * (1 + tDamage / 100),  // 데미지
            data.CCDuration,                                                                            // 상태이상 지속시간
            data.Radius * (1 + tRadius / 100) <= 0 ? data.Radius : data.Radius * (1 + tRadius / 100));   // 화염 범위                                                                                   // 화염 색상
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
