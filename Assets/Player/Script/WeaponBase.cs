using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData data;
    public SkillManager skill;
    public GameObject firePos;
    protected GameObject weapon, weaponImg;

    public float Rpm;

    protected float curTime = 0f, rotateDeg;

    public abstract void Attack();

    //-----------------------------------<애니메이션>--------------------------------------------------
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
            weapon.transform.localScale = new Vector3(1, -1, 1);
            weaponImg.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
        else
        {
            weapon.transform.localScale = new Vector3(1, 1, 1);
            weaponImg.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }

    //-----------------------------------<초기화>--------------------------------------------------

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
        Rpm = data.upgradeList[data.upgradeNum].rpm;
        CalcVec();
        if (curTime >= 60 / Rpm)
        {
            Attack();
        }
        else
        {
            curTime += Time.deltaTime;
        }
    }
}
