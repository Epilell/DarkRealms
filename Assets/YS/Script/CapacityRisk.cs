using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

public class CapacityRisk : MonoBehaviour
{
    public Player player;
    public Inventory inventory;
    public MagneticField magneticField;
    int capacity; // �뷮

    void Update()
    {
<<<<<<< Updated upstream
        capacity = inventory._Items.Length; // �뷮 �ޱ�
        // player.ChangeSpeedReduction(capacity / 10); // �뷮��ŭ �̵��ӵ� ����
        if (magneticField != null) { magneticField.decreaseSpeed = capacity; } // �뷮��ŭ �ڱ��� ���� �ӵ��� ������
=======
        capacity = oldInventory.GetItemSlotCount(); // �뷮 �ޱ�
        if (capacity >= 1) { magneticField.decreaseSpeed = capacity; } // �뷮��ŭ �ڱ��� ��� �ӵ� ����
        // player.ChangeSpeedReduction(capacity / 10); // �뷮��ŭ �̵��ӵ� ����
        // �� ���� ����??
>>>>>>> Stashed changes
    }
}