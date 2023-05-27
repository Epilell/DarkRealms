using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapacityRisk : MonoBehaviour
{
    public Player player;
    public OldInventory oldInventory;
    public MagneticField magneticField;
    int capacity; // �뷮

    void Update()
    {
        capacity = oldInventory.GetItemSlotCount(); // �뷮 �ޱ�
        magneticField.decreaseSpeed = capacity; // �뷮��ŭ �ڱ��� ��� �ӵ� ����
        player.ChangeSpeedReduction(capacity / 10); // �뷮��ŭ �̵��ӵ� ����
        // �� ���� ����??
    }
}