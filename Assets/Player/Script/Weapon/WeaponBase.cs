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
    public GameObject weaponImg, bullet;

    protected float curTime = 0f, rotateDeg;
    #endregion

    //Private Field
    #region

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
        weaponPos = Player.instance.transform.position;

        //���콺�� z���� ī�޶� ������ ��ġ
        mousePos.z = weaponPos.z - Camera.main.transform.position.z;

        //���� ���콺 ��ġ �Է�
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        //ȸ���� ���� ����
        float dx = target.x - weaponPos.x;
        float dy = target.y - weaponPos.y;
        rotateDeg = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0f, 0f, rotateDeg);

        //���콺��ġ�� ���� �¿� ����
        //1. ���콺�� ������ ������
        if (dx < 0f)
        {
            this.transform.localScale = new Vector3(-1, -1, 1);
        }
        //2.���콺�� ������ ������
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    #endregion

    //unity event
    #region

    // Update is called once per frame
    private void Update()
    {
        CalcVec();
        if (curTime >= 60 / data.Rpm)
        {
            if (Input.GetMouseButton(0))
            {
                Attack();
                string containText = Regex.Match(data.Name, "rifle|shotgun|pistol", RegexOptions.IgnoreCase).Value.ToLower(); // ���� ǥ�������� ���ڿ� ã��
                string findWord = containText.Substring(0, 1).ToUpper() + containText[1..]; // ù ��°�� �빮��, �������� �״�� �ҹ���
                FindObjectOfType<SoundManager>().PlaySound(findWord); // �ѼҸ� ���
            }
        }
        else
        {
            curTime += Time.deltaTime;
        }
    }
    #endregion
}
