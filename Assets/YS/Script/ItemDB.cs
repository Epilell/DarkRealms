using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB instance;  // ItemDB Ŭ������ �̱��� �ν��Ͻ�
    public List<Item> itemDB;  // ������ ����Ʈ
    public GameObject fieldItemPrefab;  // ������ ������Ʈ ������
    Vector3[] pos;  // �����Ǵ� ��ġ�� �迭
    public int posNum; // pos ���� ��(== ����� �������� ��)

    private void Awake()
    {
        instance = this;  // �̱��� �ν��Ͻ��� �� ��ü�� ����
        pos = new Vector3[posNum];  // pos �ʱ�ȭ

        //
        PItem fieldItemPItem = fieldItemPrefab.GetComponent<PItem>();
        if (fieldItemPItem != null)
        {
            itemDB.AddRange(fieldItemPItem.items);
        }

        for (int i = 0; i < posNum; i++)  // pos�� posNum���� ������ ���� ���� �Է�
        {
            pos[i] = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-5.0f, 5.0f), 0.0f);
        }

    }

    private void Start()
    {
        // ������ n�� ������ ��ġ�� ����
        for (int i = 0; i < pos.Length; i++)
        {
            // ������ ������Ʈ ����
            GameObject gameObject = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            // FieldItem ������Ʈ�� �������� ������ �����ͺ��̽����� �������� ������ ���������� ����
            gameObject.GetComponent<PItem>().SetItem(itemDB[Random.Range(0, itemDB.Count)]);
        }
    }
}