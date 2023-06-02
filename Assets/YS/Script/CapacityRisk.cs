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
        if (inventory._Items!= null)
        {
            capacity = inventory._Items.Length; // �뷮 �ޱ�
        }
        if (magneticField != null && capacity >= 1) { magneticField.decreaseSpeed = capacity / 20; } // �뷮��ŭ �ڱ��� ���� �ӵ��� ������
        // player.ChangeSpeedReduction(capacity / 10); // �뷮��ŭ �̵��ӵ� ���� �� ���⿡ �÷��̾� �ӵ��� 0���Ϸ� �� �������� �ϴ� ���� �߰��ؾ� ��
        // �� ���� ����??
    }
}