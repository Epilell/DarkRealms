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
        switch (StorageNum)//창고번호에 따라 다른 창고에 넣기
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
        SlotCount = 30;  // // 최초 활성 슬롯 개수
        if (onChangeItem != null)  // 아이템 변경 이벤트가 등록되어 있으면
            onChangeItem.Invoke();  // 아이템 변경 이벤트 호출
    }
}
