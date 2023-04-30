using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class W_Manager : MonoBehaviour
{
    public GameObject self;

    public void ChangeWeapon(string WeaponName)
    {
        if (self.transform.childCount >= 1)
        {
            RemoveWeapon();
        }

        GameObject prefab = Resources.Load<GameObject>("Prefabs/"+WeaponName);
        GameObject Instance = Instantiate(prefab, self.transform);
        Instance.tag = "Weapon";
    }

    private void RemoveWeapon()
    {
        for (int i = self.transform.childCount - 1; i >=0; i--)
        {
            Transform child = self.transform.GetChild(i);

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

    private void Awake()
    {
        self = this.gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon("Rifle");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon("Shotgun");
        }
    }
}
