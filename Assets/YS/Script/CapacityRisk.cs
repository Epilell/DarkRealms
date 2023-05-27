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
<<<<<<< HEAD
        capacity = inventory._Items.Length; // 용량 받기
        player.ChangeSpeedReduction(capacity / 10); // 용량만큼 이동속도 감소 <- 이거 맞는지 물어보기
        magneticField.decreaseSpeed = capacity; // 용량만큼 자기장 감소 속도가 빨라짐
=======
        capacity = oldInventory.GetItemSlotCount(); // 용량 받기
        magneticField.decreaseSpeed = capacity; // 용량만큼 자기장 축소 속도 증가
        player.ChangeSpeedReduction(capacity / 10); // 용량만큼 이동속도 감소
>>>>>>> 766aef09e2515b157675c1d7165477ddf8af3544
        // 몹 스폰 증가??
    }
}