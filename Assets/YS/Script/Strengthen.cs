using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Strengthen : MonoBehaviour
{
    public int currentLevel = 1; // ���� ��� ����
    public int maxLevel = 3; // �ִ� ��� ����
    public float successRateStep = 0.3f; // ��ȭ ���� Ȯ�� ������

    public Button strengthenBtn; // ��ȭ ��ư
    public StrengthenInventory inventory; // ��ȭ �κ��丮

    private void Start()
    {
        strengthenBtn.onClick.AddListener(EnhanceEquipment);
        inventory = FindObjectOfType<StrengthenInventory>(); // StrengthenInventory Ÿ���� �ν��Ͻ��� ã�Ƽ� �Ҵ�
    }

    private void EnhanceEquipment()
    {
        // ���� ���ο� �±װ� "Material"�� ��ȭ ��� �������� �ִ��� Ȯ��
        bool hasMaterials = CheckIfMaterialsExist();

        if (hasMaterials)
        {
            int materialCount = inventory.GetMaterialCount(); // �κ��丮���� ��ȭ ��� ������ ���� ��������
            float successRate = (materialCount * successRateStep) + 0.1f; // ��ȭ ���� Ȯ�� ���

            // ��ȭ ���� ���� ����
            bool isEnhancementSuccessful = Random.Range(0f, 1f) < successRate;

            if (isEnhancementSuccessful)
            {
                currentLevel++;
                Debug.Log("��� ��ȭ ����! ���� ����: " + currentLevel);
            }
            else
            {
                Debug.Log("��ȭ ����!");
            }

            // ���� ������ ��� ��ȭ ��� ������ ����
            inventory.RemoveMaterials(materialCount);
        }
        else
        {
            Debug.Log("��ȭ ��ᰡ �����մϴ�.");
        }
    }

    private bool CheckIfMaterialsExist()
    {
        // ��ȭ �κ��丮 ���� ��� ���� Ȯ��
        foreach (Transform slot in inventory.transform)
        {
            // ���� ���ο� �±װ� "Material"�� ��ü�� �ִ��� Ȯ��
            if (slot.CompareTag("Material"))
            {
                return true; // ��ȭ ��ᰡ ������
            }
        }

        return false; // ��ȭ ��ᰡ �������� ����
    }

    /*private void Start()
    {
        strengthenBtn.onClick.AddListener(EnhanceEquipment); // ��ȭ ��ư�� ��ȭ ��� �߰�
    }

    private void EnhanceEquipment()
    {
        int materialCount = inventory.GetMaterialCount(); // �κ��丮���� ��� ���� ��������
        float successRate = (materialCount * successRateStep) + 0.1f; // ��� ������ ���� ���� Ȯ�� ���

        // ��ȭ ���� ���� ����
        bool isEnhancementSuccessful = Random.Range(0f, 1f) < successRate;

        if (isEnhancementSuccessful)
        {
            currentLevel++;
            Debug.Log("��� ��ȭ ����! ���� ����: " + currentLevel);
        }
        else
        {
            Debug.Log("��� ��ȭ ����.");
        }

        // �κ��丮���� ��� ������ ����
        inventory.RemoveMaterials(materialCount);
    }*/
}