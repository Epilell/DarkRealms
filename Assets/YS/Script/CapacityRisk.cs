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
        if (inventory._Items!= null)
        {
            capacity = inventory._Items.Length; // 용량 받기
        }
        if (magneticField != null && capacity >= 1) { magneticField.decreaseSpeed = capacity / 20; } // 용량만큼 자기장 감소 속도가 빨라짐
        // player.ChangeSpeedReduction(capacity / 10); // 용량만큼 이동속도 감소 ← 여기에 플레이어 속도가 0이하로 안 내려가게 하는 로직 추가해야 함
        // 몹 스폰 증가??
    }
}