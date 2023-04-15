using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public delegate void OnSlotCountChange(int val);  // 대리자 정의
    public OnSlotCountChange onSlotCountChange;  // 인스턴스화

    private int slotCount;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public int SlotCount
    {
        get => slotCount;
        set
        {
            slotCount = value;
            onSlotCountChange.Invoke(slotCount);
        }
    }

    void Start()
    {
        // 최초 활성 슬롯 개수
        SlotCount = 5;
    }
}