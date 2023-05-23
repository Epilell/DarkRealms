using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour
{
    public Player p;

    public void ApplyEquipmentEffect(string itemName, float effectPoint)
    {
        switch (itemName)
        {
            case "helmet":
                break;
            case "armor":
                break;
            case "knee":
                break;
            case "shoes":
                break;
            case "rifle":
                break;
            case "shotgun":
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
                break;
            case "armor":
                break;
            case "knee":
                break;
            case "shoes":
                break;
            case "rifle":
                break;
            case "shotgun":
                break;
            default:
                return;
        }
    }
}