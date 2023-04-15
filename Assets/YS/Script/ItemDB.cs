using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Item> itemDB = new List<Item>();
}