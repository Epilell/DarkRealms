using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB instance;  // ItemDB Ŭ������ �̱��� �ν��Ͻ�
    public List<Item> itemDB = new List<Item>();  // ������ ����Ʈ
    public GameObject fieldItemPrefab;  // ������ ������Ʈ ������
    public Vector3[] pos;  // �����Ǵ� ��ġ�� �迭

    private void Awake()
    {
        instance = this;  // // �̱��� �ν��Ͻ��� �� ��ü�� ����
    }

    private void Start()
    {
        // ������ 5�� ������ ��ġ�� ����
        for (int i = 0; i < 5; i++)
        {
            // ������ ������Ʈ ����
            GameObject gameObject = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            // FieldItem ������Ʈ�� �������� ������ �����ͺ��̽����� �������� ������ ���������� ����
            gameObject.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 3)]);
        }
    }
}