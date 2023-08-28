using UnityEngine;
using Rito.InventorySystem;

public class CapacityRisk : MonoBehaviour
{
    public Player player;
    public PlayerData p_data;
    public Inventory inventory;
    public MagneticField magneticField;

    private int previousCapacity = 0, currentCapacity = 0; // ���� �����ӿ����� �κ��丮 �뷮 ���

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        
        if (inventory != null && inventory._Items != null) // ���� ������ �� �ʱ� �뷮 �ޱ�
            for (int i = 0; i < inventory._Items.Length; i++) if (inventory._Items[i] != null && inventory._Items[i].Data != null) previousCapacity += 1;

        ChangeMagneticSpeed(previousCapacity);
    }

    void Update()
    {
        currentCapacity = 0;

        if (inventory != null && inventory._Items != null) // ���� �뷮 �ޱ�
            for (int i = 0; i < inventory._Items.Length; i++) if (inventory._Items[i] != null && inventory._Items[i].Data != null) currentCapacity += 1;

        if (currentCapacity != previousCapacity) // ���� �뷮 �ٲ� ��
        {
            previousCapacity = currentCapacity; // ���� �뷮�� ���� �뷮���� ������Ʈ
            ChangeMagneticSpeed(currentCapacity);
        }

        // �뷮��ŭ �̵��ӵ� ����
        // �� ���� ����
    }

    private void ChangeMagneticSpeed(float capacity)
    {
        if (magneticField != null)
        {
            float changeSpeed = (float)capacity / inventory.Capacity; // ���� ������ ���� / ��ü ������ ���� �� �� �κ��� ���߿� amount�� ����ص� ��

            if (changeSpeed == 1.0f) magneticField.decreaseSpeed = 5.0f;  // �κ��丮 �� �� ����
            else if (changeSpeed == 0.0f) magneticField.decreaseSpeed = 1.0f;  // �κ��丮 ��� ����
            else magneticField.decreaseSpeed = 1.0f + changeSpeed * 4.0f;  // �κ��丮 �뷮�� ���� ��ȭ
        }
    }
}