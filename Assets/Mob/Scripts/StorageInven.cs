using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInven : Inventory
{
    public StorageData storage1;
    public StorageData storage2;
    public StorageData storage3;
    public StorageData storage4;
    [SerializeField]
    private int StorageNum = 1;
    private void Start()
    {
        switch (StorageNum)//â���ȣ�� ���� �ٸ� â�� �ֱ�
        {
            case 1:
                items = storage1.items;
                break;
            case 2:
                items = storage2.items;
                break;
            case 3:
                items = storage3.items;
                break;
            case 4:
                items = storage4.items;
                break;
        }
        SlotCount = 30;  // // ���� Ȱ�� ���� ����
        if (onChangeItem != null)  // ������ ���� �̺�Ʈ�� ��ϵǾ� ������
            onChangeItem.Invoke();  // ������ ���� �̺�Ʈ ȣ��
    }
}
