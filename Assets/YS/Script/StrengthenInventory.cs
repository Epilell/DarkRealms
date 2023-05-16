using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthenInventory : MonoBehaviour
{
    private Dictionary<string, int> materials = new Dictionary<string, int>(); // ��� ������ ������ ���� ����(Dictionary)

    public void AddMaterial(string materialName)
    {
        if (materials.ContainsKey(materialName))
        {
            materials[materialName]++;
        }
        else
        {
            materials.Add(materialName, 1);
        }
    }

    public int GetMaterialCount()
    {
        int count = 0;
        foreach (int materialCount in materials.Values)
        {
            count += materialCount;
        }
        return count;
    }

    public void RemoveMaterials(int materialCount)
    {
        List<string> materialsToRemove = new List<string>();
        foreach (var pair in materials)
        {
            string materialName = pair.Key;
            int count = pair.Value;

            if (count <= materialCount)
            {
                materialsToRemove.Add(materialName);
                materialCount -= count;
            }
            else
            {
                materials[materialName] -= materialCount;
                break;
            }
        }

        // �κ��丮���� ��� ������ ����
        foreach (string materialToRemove in materialsToRemove)
        {
            materials.Remove(materialToRemove);
        }
    }
}