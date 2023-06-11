using Rito.InventorySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DualWeaponBase : MonoBehaviour
{
    //Public Field
    #region

    public WeaponItemData data;
    public GameObject firePos1, firePos2;
    public GameObject weapon, weaponImg1, weaponImg2, bullet;

    #endregion

    //Protected Field
    #region

    protected int order = 1;
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
        //���콺 ��ġ�� �÷��̾� ��ġ �Է�
        mousePos = Input.mousePosition;
        weaponPos = this.transform.position;

        //���콺�� z���� ī�޶� ������ ��ġ
        mousePos.z = weaponPos.z - Camera.main.transform.position.z;

        //���� ���콺 ��ġ �Է�
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        //���콺 ���� ���
        float dx = target.x - weaponPos.x;
        float dy = target.y - weaponPos.y;

        rotateDeg = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotateDeg);

        //���콺��ġ�� ���� �¿� ����
        if (dx < 0f)
        {
            
            weapon.transform.localScale = new Vector3(-1, -1, 1);
            weaponImg1.GetComponent<SpriteRenderer>().sortingOrder = -1;
            weaponImg2.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
        else
        {
            weapon.transform.localScale = new Vector3(1, 1, 1);
            weaponImg1.GetComponent<SpriteRenderer>().sortingOrder = 1;
            weaponImg2.GetComponent<SpriteRenderer>().sortingOrder = 1;
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
                FindObjectOfType<SoundManager>().PlaySound("Dual");
            }
        }
        else
        {
            curTime += Time.deltaTime;
        }
    }
    #endregion
}
