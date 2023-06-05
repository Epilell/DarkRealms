using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class WeaponBase : MonoBehaviour
{
    //Public Field
    #region
    public WeaponItemData data;
    public SkillManager skill;
    public GameObject firePos;
    public GameObject weapon, weaponImg, bullet;

    public float curTime = 0f, rotateDeg;

    #endregion

    //Public Method
    #region
    public abstract void Attack();

    public void SetStatus(WeaponItemData _data)
    {
        data = _data;
    }

    #endregion

    //Animation
    #region
    private void CalcVec()
    {
        Vector3 mousePos, weaponPos;
        //마우스 위치와 플레이어 위치 입력
        mousePos = Input.mousePosition;
        weaponPos = this.transform.position;

        //마우스의 z값을 카메라 앞으로 위치
        mousePos.z = weaponPos.z - Camera.main.transform.position.z;

        //실제 마우스 위치 입력
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        //마우스 방향 계산
        float dx = target.x - weaponPos.x;
        float dy = target.y - weaponPos.y;

        rotateDeg = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotateDeg);

        //마우스위치에 따라 좌우 반전
        if (dx < 0f)
        {
            weapon.transform.localScale = new Vector3(-1, -1, 1);
            weaponImg.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
        else
        {
            weapon.transform.localScale = new Vector3(1, 1, 1);
            weaponImg.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }
    #endregion

    //unity event
    #region

    private void Awake()
    {
        weapon = this.gameObject;
        skill = GameObject.FindWithTag("Player").GetComponent<SkillManager>();
    }

    private void Start()
    {
        weaponImg = transform.Find("weaponImg").gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        CalcVec();
        if (curTime >= 60 / data.Rpm)
        {
            if (Input.GetMouseButton(0))
            {
                Attack();
            }
        }
        else
        {
            curTime += Time.deltaTime;
        }
    }
    #endregion
}
