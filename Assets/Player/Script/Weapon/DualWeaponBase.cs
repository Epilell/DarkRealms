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

    //0.1�ʸ��� RPM ������
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
        //���콺 ��ġ�� �÷��̾� ��ġ �Է�
        mousePos = Input.mousePosition;
        weaponPos = Player.Instance.transform.position;

        //���콺�� z���� ī�޶� ������ ��ġ
        mousePos.z = weaponPos.z - Camera.main.transform.position.z;

        //���� ���콺 ��ġ �Է�
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        //���콺 ���� ���
        float dx = target.x - weaponPos.x;
        float dy = target.y - weaponPos.y;

        rotateDeg = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        weapon1.transform.rotation = Quaternion.Euler(0f, 0f, rotateDeg);
        weapon2.transform.rotation = Quaternion.Euler(0f, 0f, rotateDeg);

        //���콺��ġ�� ���� �¿� ����
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
