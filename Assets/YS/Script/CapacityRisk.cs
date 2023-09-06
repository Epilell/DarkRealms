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

        magneticField.decreaseSpeed = 1.0f + (float)previousCapacity / inventory.Capacity * 4.0f; // ���� ���� �� �ڱ��� ��� �ӵ� ����
    }

    void Update()
    {
        currentCapacity = 0;

        if (inventory != null && inventory._Items != null) // ���� �뷮 �ޱ�
            for (int i = 0; i < inventory._Items.Length; i++) if (inventory._Items[i] != null && inventory._Items[i].Data != null) currentCapacity += 1;

        if (currentCapacity != previousCapacity) // ���� �뷮 �ٲ� ��
        {
            previousCapacity = currentCapacity; // ���� �뷮�� ���� �뷮���� ������Ʈ
            magneticField.decreaseSpeed = 1.0f + (float)currentCapacity / inventory.Capacity * 4.0f; // �ڱ��� ��� �ӵ� ����
            // FindObjectOfType<Player>().ChangeSpeedReduction((float)currentCapacity / inventory.Capacity * 50f); // �÷��̾� �̵� �ӵ� ����
        }

        // �� ���� ����?
    }
}