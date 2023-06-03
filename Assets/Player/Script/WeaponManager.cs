using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    public GameObject curWeapon;

    public void ChangeWeapon(string WeaponName)
    {
        if (this.transform.childCount >= 1)
        {
            RemoveWeapon();
        }

        GameObject prefab = Resources.Load<GameObject>("Prefabs/"+WeaponName);
        curWeapon = prefab;
        GameObject Instance = Instantiate(prefab, this.transform);
        Instance.tag = "Weapon";
    }

    private void RemoveWeapon()
    {
        for (int i = this.transform.childCount - 1; i >=0; i--)
        {
            Transform child = this.transform.GetChild(i);

            if (child.CompareTag("Weapon"))
            {
                Destroy(child.gameObject);
            }
            else
            {
                RemoveWeapon();
            }
        }
    }

    //--------------------------------------<°­È­>--------------------------------------------

    public void WeaponUpgrade()
    {
        curWeapon.GetComponent<WeaponBase>().data.Upgrade();
        Debug.Log("Upgrade");
    }

    

    // Update is called once per frame
    private void Update()
    {
        switch (Input.inputString)
        {
            case "1":
                ChangeWeapon("rifle");
                break;
            case "2":
                ChangeWeapon("shotgun");
                break;
            case "3":
                ChangeWeapon("pistol");
                break;
            case "4":
                ChangeWeapon("dualPistol");
                break;
        }
    }
}
