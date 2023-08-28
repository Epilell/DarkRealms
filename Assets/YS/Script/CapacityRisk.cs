using UnityEngine;
using Rito.InventorySystem;

public class CapacityRisk : MonoBehaviour
{
    public Player player;
    public PlayerData p_data;
    public Inventory inventory;
    public MagneticField magneticField;

    private int previousCapacity = 0, currentCapacity = 0; // 이전 프레임에서의 인벤토리 용량 기록

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        
        if (inventory != null && inventory._Items != null) // 게임 시작할 때 초기 용량 받기
            for (int i = 0; i < inventory._Items.Length; i++) if (inventory._Items[i] != null && inventory._Items[i].Data != null) previousCapacity += 1;

        ChangeMagneticSpeed(previousCapacity);
    }

    void Update()
    {
        currentCapacity = 0;

        if (inventory != null && inventory._Items != null) // 현재 용량 받기
            for (int i = 0; i < inventory._Items.Length; i++) if (inventory._Items[i] != null && inventory._Items[i].Data != null) currentCapacity += 1;

        if (currentCapacity != previousCapacity) // 슬롯 용량 바뀔 때
        {
            previousCapacity = currentCapacity; // 현재 용량을 이전 용량으로 업데이트
            ChangeMagneticSpeed(currentCapacity);
        }

        // 용량만큼 이동속도 감소
        // 몹 스폰 증가
    }

    private void ChangeMagneticSpeed(float capacity)
    {
        if (magneticField != null)
        {
            float changeSpeed = (float)capacity / inventory.Capacity; // 현재 아이템 슬롯 / 전체 아이템 슬롯 ← 이 부분은 나중에 amount로 계산해도 됨

            if (changeSpeed == 1.0f) magneticField.decreaseSpeed = 5.0f;  // 인벤토리 꽉 찬 상태
            else if (changeSpeed == 0.0f) magneticField.decreaseSpeed = 1.0f;  // 인벤토리 비움 상태
            else magneticField.decreaseSpeed = 1.0f + changeSpeed * 4.0f;  // 인벤토리 용량에 따라 변화
        }
    }
}