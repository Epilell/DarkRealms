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

    void Update()
    {
        if (inventory._Items != null) { capacity = inventory._Items.Length; } // 용량 받기
        if (magneticField != null && capacity >= 1 && magneticField.decreaseSpeed <= 5) { magneticField.decreaseSpeed = capacity / 50; } // 용량만큼 자기장 감소 속도 증가
        /*if (p_data.speed >= 0.5) { player.ChangeSpeedReduction(capacity / 10); } // 용량만큼 이동속도 감소
        else p_data.speed = 0.5f;*/
        // 몹 스폰 증가??
    }
}