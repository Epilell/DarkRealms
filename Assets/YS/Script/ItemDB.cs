using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB instance;
    public List<Item> itemDB = new List<Item>();
    public GameObject fieldItemPrefab;  // ������
    public Vector3[] pos;  // ���� ��ġ

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // ������ 5��
        for (int i = 0; i < 7; i++)
        {
            GameObject gameObject = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            gameObject.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 3)]);
        }
    }
}