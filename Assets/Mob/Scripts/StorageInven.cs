using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInven : MonoBehaviour
{
    [SerializeField]
    private StorageData storData;

    public static StorageInven instance;  // �κ��丮 �ν��Ͻ�

    public List<Item> items = new();  // �κ��丮�� �ִ� ������ ����Ʈ

    public delegate void SlotCountChange(int val);  // ���� ���� ���� �� ȣ���� �븮��
    public SlotCountChange slotCountChange;  // ���� ���� ���� �� �̺�Ʈ

    public delegate void OnChangeItem();  // ������ ���� �븮��
    public OnChangeItem onChangeItem;  // ������ ���� �ν��Ͻ�

    private int slotCount;

    private void Awake()
    {
        if (instance != null)  // �κ��丮 �ν��Ͻ��� �����ϸ�
        {
            Destroy(gameObject);  // �ߺ� ���� ������ ���� ���� ���� ������Ʈ�� �ı�
            return;  // ����
        }
        instance = this;  // �ν��Ͻ��� �������� ������ ���� �ν��Ͻ��� �Ҵ�
    }

    public int SlotCount  // ���� ���� ������Ƽ
    {
        get => slotCount;  // ���� �� ��ȯ
        set  // ���� �� ����
        {
            slotCount = value;  // �Էµ� ������ ���� ���� ����
            slotCountChange.Invoke(slotCount);
            /*if (slotCountChange != null)  // ���� ���� ���� �̺�Ʈ�� ��ϵǾ� ������
                slotCountChange.Invoke(slotCount);  // ���� ���� ���� ȣ��*/
        }
    }

    private void Start()
    {
        SlotCount = 15; // ���� Ȱ�� ���� ����
        if (storData.items.Count > 0)
        {
            items = storData.items;
            if (onChangeItem != null)  // ������ ���� �̺�Ʈ�� ��ϵǾ� ������
                onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
        }
    }


    // ������ �߰� �Լ�
    public bool AddItem(Item _item)
    {
        if (items.Count < SlotCount)  // �κ��丮�� ������ ������ ���� ���� ������ ������
        {
            items.Add(_item);  // ������ ����Ʈ�� ������ �߰�
            if (onChangeItem != null)  // ������ ���� �̺�Ʈ�� ��ϵǾ� ������
                onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
            storData.items = items;
            return true;  // ������ �߰�
        }
        else
        {
            Debug.Log("�κ��丮 ��ȭ!");
            storData.items = items;
            return false;  // ������ �߰����� ����
        }
    }

    // ������ ���� �Լ�
    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);  // ������ ����Ʈ���� �ش� �ε����� ������ ����
        if (onChangeItem != null)  // ������ ���� �̺�Ʈ�� ��ϵǾ� ������
            onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
        storData.items = items;
    }
}