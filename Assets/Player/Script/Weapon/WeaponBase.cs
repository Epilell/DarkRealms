using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class WeaponBase : MonoBehaviour
{
    //Public Field
    #region
    public WeaponItemData data;

    public GameObject firePos;
    public GameObject weapon, weaponImg, bullet;

    protected float curTime = 0f, rotateDeg;

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

    }

    private void Start()
    {

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
                string containText = Regex.Match(data.Name, "rifle|shotgun|pistol", RegexOptions.IgnoreCase).Value.ToLower(); // 정규 표현식으로 문자열 찾기
                string findWord = containText.Substring(0, 1).ToUpper() + containText[1..]; // 첫 번째만 대문자, 나머지는 그대로 소문자
                FindObjectOfType<SoundManager>().PlaySound(findWord); // 총소리 재생
            }
        }
        else
        {
            curTime += Time.deltaTime;
        }
    }
    #endregion
}
