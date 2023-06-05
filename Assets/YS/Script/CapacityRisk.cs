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

    int capacity; // �뷮

    void Update()
    {
        if (inventory._Items != null) { capacity = inventory._Items.Length; } // �뷮 �ޱ�
        if (magneticField != null && capacity >= 1 && magneticField.decreaseSpeed <= 5) { magneticField.decreaseSpeed = capacity / 50; } // �뷮��ŭ �ڱ��� ���� �ӵ� ����
        /*if (p_data.speed >= 0.5) { player.ChangeSpeedReduction(capacity / 10); } // �뷮��ŭ �̵��ӵ� ����
        else p_data.speed = 0.5f;*/
        // �� ���� ����??
    }
}