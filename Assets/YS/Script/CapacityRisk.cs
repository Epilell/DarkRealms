using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

public class CapacityRisk : MonoBehaviour
{
    public Player player;
    public Inventory inventory;
    public MagneticField magneticField;
    int capacity; // 용량

    void Update()
    {
        capacity = inventory._Items.Length; // 용량 받기
        // player.ChangeSpeedReduction(capacity / 10); // 용량만큼 이동속도 감소
        if (magneticField != null) { magneticField.decreaseSpeed = capacity; } // 용량만큼 자기장 감소 속도가 빨라짐
    }
}