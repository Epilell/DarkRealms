using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobInventory : MonoBehaviour
{
    public GameObject inventory;  // 인벤토리 변수
    private bool isInven = false;

    void Update()
    {
        // ESC키를 누르면 인벤토리를 닫음
        if (isInven == true && Input.GetKeyDown(KeyCode.Escape))
        {
            inventory.SetActive(false);
            isInven = false;
        }
    }

    void OnMouseDown()
    {
        // 마우스로 클릭했는데 태그가 DeadMob 이면 인벤토리 열기
        if (isInven == false && gameObject.tag == "DeadMob")
        {
            inventory.SetActive(true);
            isInven = true;
        }
    }
}