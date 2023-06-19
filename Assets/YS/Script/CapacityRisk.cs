using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

public class CapacityRisk : MonoBehaviour
{
    public Player player;
    public P_Data p_data;
    public Inventory inventory;
    public MagneticField magneticField;

    int capacity; // 용량

    private void Start() { player = GameObject.FindWithTag("Player").GetComponent<Player>(); }

    void Update()
    {
        if (inventory._Items != null) { capacity = inventory._Items.Length; } // 용량 받기

        if (magneticField != null && capacity >= 1) // 용량만큼 자기장 축소 빨라짐
        {
            float decreaseSpeed = (float)capacity / 45;

            if (decreaseSpeed <= 1.0) { magneticField.decreaseSpeed = 1.0f; }
            else if (decreaseSpeed <= 5.0) { magneticField.decreaseSpeed = decreaseSpeed; }
            else { magneticField.decreaseSpeed = 5.0f; }
        }


        // 용량만큼 이동속도 감소 추가 예정
        // 몹 스폰 증가??
    }
}