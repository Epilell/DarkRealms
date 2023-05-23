using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveItem : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private InventoryUI _inventoryUI;
    [SerializeField]
    private Inventory _warehouse;
    [SerializeField]
    private InventoryUI _warehouseUI;
    private List<RaycastResult> _rrList;
    private GraphicRaycaster _pgr;
    private PointerEventData _ped;
    private ItemSlotUI _beginDragSlot;
    private void Update()
    {
        _rrList = _inventoryUI._RrList;
        _pgr = _inventoryUI._Pgr;
        _ped = _inventoryUI._Ped;
        _beginDragSlot = _inventoryUI._BeginDragSlot
    }

    private T RaycastAndGetComponent<T>() where T : Component
    {
        _rrList.Clear();

        _pgr.Raycast(_ped, _rrList);
        for (int i = 0; i < _rrList.Count; i++)
        {
            if (_rrList != null)
            {
                Debug.Log(_rrList[i]);
            }
            else
            {
                Debug.Log(_rrList.Count);
            }
        }
        if (_rrList.Count == 0)
        {
            Debug.Log(_rrList.Count);
            return null;
        }
        return _rrList[_rrList.Count - 1].gameObject.GetComponent<T>();
    }
    private void aaaaa()
    {
        InventoryUI _otherInventoryUI = RaycastAndGetComponent<InventoryUI>();
        Debug.Log(RaycastAndGetComponent<InventoryUI>());
        if (_otherInventoryUI != null)//레이케스트된것에 _otherInventory가 있을경우)
        {
            //다른인벤토리에 추가하고 this 인벤토리에서 삭제
            Debug.Log("옮기기 성공?");
            _otherInventoryUI._Inventory.Add(_inventory.GetItemData(_beginDragSlot.Index), _inventory.GetCurrentAmount(_beginDragSlot.Index));
            _inventoryUI.TryRemoveItem_P(_beginDragSlot.Index);
            Debug.Log("옮기기 성공!");
        }
        else
        {
            Debug.Log("옮기기 실패!");
        }
    }
}
