using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthenInventory : MonoBehaviour
{
    private Dictionary<string, int> materials = new Dictionary<string, int>(); // 재료 아이템 저장을 위한 사전(Dictionary)

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

        // 인벤토리에서 재료 아이템 삭제
        foreach (string materialToRemove in materialsToRemove)
        {
            materials.Remove(materialToRemove);
        }
    }
}