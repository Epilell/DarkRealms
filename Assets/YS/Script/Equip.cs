using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public P_Data p;

    public void ApplyEquipmentEffect(string itemName, float effectPoint)
    {
        switch (itemName)
        {
            case "helmet":
                p.P_HelmetArmor += effectPoint;
                break;
            case "armor":
                p.P_BodyArmor += effectPoint;
                Debug.Log("후: " + p.P_BodyArmor);
                break;
            case "knee":
                p.P_LegArmor += effectPoint;
                break;
            case "shoes":
                p.P_Speed += effectPoint;
                break;
            case "rifle":
                p.P_MaxHp += effectPoint;
                break;
            case "shotgun":
                Debug.Log("샷건");
                break;
            default:
                return;
        }
    }

    public void RemoveEquipmentEffect(string itemName, float effectPoint)
    {
        switch (itemName)
        {
            case "helmet":
                p.P_HelmetArmor -= effectPoint;
                break;
            case "armor":
                p.P_BodyArmor -= effectPoint;
                break;
            case "knee":
                p.P_LegArmor -= effectPoint;
                break;
            case "shoes":
                p.P_Speed -= effectPoint;
                break;
            case "rifle":
                p.P_MaxHp -= effectPoint;
                break;
            case "shotgun":
                Debug.Log("샷건 교체 또는 해제");
                break;
            default:
                return;
        }
    }
}