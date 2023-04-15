using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public delegate void OnSlotCountChange(int val);  // �븮�� ����
    public OnSlotCountChange onSlotCountChange;  // �ν��Ͻ�ȭ

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
        // ���� Ȱ�� ���� ����
        SlotCount = 5;
    }
}