using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityRisk : MonoBehaviour
{
    public Player player;
    public OldInventory oldInventory;
    public MagneticField magneticField;
    int capacity; // 용량

    void Update()
    {
        capacity = oldInventory.GetItemSlotCount(); // 용량 받기
        player.ChangeSpeedReduction(capacity / 10); // 용량만큼 이동속도 감소 <- 이거 맞는지 물어보기
        magneticField.decreaseSpeed = capacity; // 용량만큼 자기장 감소 속도가 빨라짐
        // 몹 스폰 증가??
    }
}