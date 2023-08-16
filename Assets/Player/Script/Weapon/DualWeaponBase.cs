using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DualWeaponBase : MonoBehaviour
{
    //Field
    #region

    public WeaponItemData data;
    public GameObject firePos1, firePos2;
    public GameObject weapon1, weapon2, weaponImg1, weaponImg2, bullet;

    protected int order = 1;
    protected float curTime = 0f, rotateDeg;

    //0.1초마다 RPM 증가량
    [SerializeField]
    private readonly float IncRpm = 10f;
    private  float temporalRpm = 0f;
    private float sec = 0f;

    #endregion

    //Method
    #region
    
    public void SetStatus(WeaponItemData _data)
    {
        data = _data;
    }

    #endregion

    //Abstract Method
    #region

    public abstract void Attack();

    #endregion

    //Animation
    #region
    private void CalcVec()
    {
        Vector3 mousePos, weaponPos;
        //마우스 위치와 플레이어 위치 입력
        mousePos = Input.mousePosition;
        weaponPos = Player.Instance.transform.position;

        //마우스의 z값을 카메라 앞으로 위치
        mousePos.z = weaponPos.z - Camera.main.transform.position.z;

        //실제 마우스 위치 입력
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        //마우스 방향 계산
        float dx = target.x - weaponPos.x;
        float dy = target.y - weaponPos.y;

        rotateDeg = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        weapon1.transform.rotation = Quaternion.Euler(0f, 0f, rotateDeg);
        weapon2.transform.rotation = Quaternion.Euler(0f, 0f, rotateDeg);

        //마우스위치에 따라 좌우 반전
        if (dx < 0f)
        {
            weapon1.transform.localScale = new Vector3(-1, -1, 1);
            weapon2.transform.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            weapon1.transform.localScale = new Vector3(1, 1, 1);
            weapon2.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    #endregion

    //unity event
    #region

    // Update is called once per frame
    private void Update()
    {
        CalcVec();
        if (SkillManager.Instance.CheckUpgrade(SkillManager.Instance.siegemodeUpgradeList, "Gradual Attack Speed Up"))
        {
            if (sec < 0.5f)
            {
                sec += Time.deltaTime;
            }
            else
            {
                temporalRpm += IncRpm;
                sec = 0f;
            }
        }

        if (curTime >= 60 / (data.Rpm + temporalRpm))
        {
            if (Input.GetMouseButton(0))
            {
                Attack();
                //FindObjectOfType<SoundManager>().PlaySound("Dual");
            }
        }
        else
        {
            curTime += Time.deltaTime;
        }
    }

    #endregion
}
