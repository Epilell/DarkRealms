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

        magneticField.decreaseSpeed = 1.0f + (float)previousCapacity / inventory.Capacity * 4.0f; // 게임 시작 시 자기장 축소 속도 변경
    }

    void Update()
    {
        currentCapacity = 0;

        if (inventory != null && inventory._Items != null) // 현재 용량 받기
            for (int i = 0; i < inventory._Items.Length; i++) if (inventory._Items[i] != null && inventory._Items[i].Data != null) currentCapacity += 1;

        if (currentCapacity != previousCapacity) // 슬롯 용량 바뀔 때
        {
            previousCapacity = currentCapacity; // 현재 용량을 이전 용량으로 업데이트
            magneticField.decreaseSpeed = 1.0f + (float)currentCapacity / inventory.Capacity * 4.0f; // 자기장 축소 속도 변경
            // FindObjectOfType<Player>().ChangeSpeedReduction((float)currentCapacity / inventory.Capacity * 50f); // 플레이어 이동 속도 변경
        }

        // 몹 스폰 증가?
    }
}