using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB instance;
    public List<Item> itemDB = new List<Item>();
    public GameObject fieldItemPrefab;  // 아이템
    public Vector3[] pos;  // 생성 위치

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // 아이템 5개
        for (int i = 0; i < 7; i++)
        {
            GameObject gameObject = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            gameObject.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 3)]);
        }
    }
}