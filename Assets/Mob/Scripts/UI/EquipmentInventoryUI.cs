using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rito.InventorySystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class EquipmentInventoryUI : MonoBehaviour
{

    //그래픽 변수
    private List<RaycastResult> _rrList;
    [SerializeField]
    private GraphicRaycaster _gr;
    private PointerEventData _ped;

    private Inventory _inventory;
    private EquipmentInventory _equipmentInventory;

    private ItemSlotUI _pointerOverSlot; // 현재 포인터가 위치한 곳의 슬롯
    private ItemSlotUI _beginDragSlot; // 현재 드래그를 시작한 슬롯

    [SerializeField] private GameObject Tooltip;
    [SerializeField] private TMP_Text _itemname;
    [SerializeField] private TMP_Text _itemTooltip;
    [SerializeField] private TMP_Text _itemStat;

    private int _leftClick = 0;
    private int _rightClick = 1;

    private Transform _beginDragIconTransform;
    private Vector3 _beginDragIconPoint;
    private Vector3 _beginDragCursorPoint;
    private int _beginDragSlotSiblingIndex;

    [Space]
    private bool _showHighlight = true;

    private void Update()
    {
        _ped.position = Input.mousePosition;
        OnPointerDown();
        OnPointerDrag();
        OnPointerUp();
        OnPointerEnterAndExit();
    }
    // Start is called before the first frame update
    void Start()
    {
        Tooltip.SetActive(false);
        _inventory = GameObject.FindWithTag("GameController").GetComponentInChildren<Inventory>();
        _equipmentInventory = GameObject.FindWithTag("GameController").GetComponentInChildren<EquipmentInventory>();
        GraphicInit();
    }

    private void GraphicInit()
    {
        _ped = new PointerEventData(EventSystem.current);
        _rrList = new List<RaycastResult>(10);
        /*
        TryGetComponent(out _gr);
        if (_gr == null)
            _gr = gameObject.AddComponent<GraphicRaycaster>();*/
    }

    private T RaycastAndGetFirstComponent<T>() where T : Component
    {
        //Debug.Log("퀵슬롯스크립트의 레이케스트");
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
        {
            //Debug.Log("레이캐스트가 아무 것도 감지하지 않았습니다.");
            return null;
        }

        //Debug.Log("레이캐스트가 명중한 오브젝트: " + _rrList[0].gameObject.name);
        return _rrList[0].gameObject.GetComponent<T>();
    }/*
    private T RaycastAndGetFirstComponent<T>() where T : Component
    {
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
            return null;

        return _rrList[0].gameObject.GetComponent<T>();
    }*/
    /*
    private void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ItemSlotUI slot = RaycastAndGetFirstComponent<ItemSlotUI>();
            if (slot != null)
            {
                if (_equipmentInventory.EqItems[slot.Index] != null)
                {
                    _inventory.Add(_equipmentInventory.UnEquip(slot.Index));
                }
            }
        }

    }*/
    private void OnPointerEnterAndExit()
    {
        // 이전 프레임의 슬롯
        var prevSlot = _pointerOverSlot;

        // 현재 프레임의 슬롯
        var curSlot = _pointerOverSlot = RaycastAndGetFirstComponent<ItemSlotUI>();

        if (prevSlot == null)
        {
            // Enter
            if (curSlot != null)
            {
                OnCurrentEnter();
            }
        }
        else
        {
            // Exit
            if (curSlot == null)
            {
                OnPrevExit();
            }

            // Change
            else if (prevSlot != curSlot)
            {
                OnPrevExit();
                OnCurrentEnter();
            }
        }

        // ===================== Local Methods ===============================
        /*
        void OnCurrentEnter()
        {
            Tooltip.SetActive(true);
            int _index = curSlot.Index;
            if (curSlot.HasItem)
            {
                ShowTooltips(_index);
            }
            if (_showHighlight)
                curSlot.Highlight(true);
        }*/
        void OnCurrentEnter()
        {
            if (!curSlot.isEquipmentSlot) return;
            Tooltip.SetActive(true);
            if (curSlot.HasItem)
            {
                int _index = curSlot.Index;
                if (_index >= 0 && _index < _equipmentInventory.EqItems.Length) // yourArray에는 적절한 배열이 들어가야 합니다.
                {
                    ShowTooltips(_index);
                }
                else
                {
                    // 유효하지 않은 인덱스에 대한 처리를 추가할 수 있습니다.
                    Debug.LogError("Invalid index: " + _index);
                }
            }
            if (_showHighlight)
                curSlot.Highlight(true);
        }
        void OnPrevExit()
        {
            DeleteTooltips();
            Tooltip.SetActive(false);
            prevSlot.Highlight(false);
        }
    }
    private void ShowTooltips(int index)
    {
        _itemname.text = _equipmentInventory.EqItems[index].Data.Name;
        _itemTooltip.text = _equipmentInventory.EqItems[index].Data.Tooltip;
        if(_equipmentInventory.EqItems[index].Data is WeaponItemData wdata)
        {
            _itemStat.text = " Damage = " + wdata.Damage.ToString();
            _itemStat.text += "\n RPM = " + wdata.Rpm.ToString();
            _itemStat.text += "\n Pellet = " + wdata.PelletNum.ToString();
        }
        else if (_equipmentInventory.EqItems[index].Data is ArmorItemData adata )
        {
            _itemStat.text = "Armor = " + adata.Defence.ToString();
            //_itemStat.text += adata.Defence.ToString();
        }
    }
    private void DeleteTooltips()
    {
        _itemname.text = "Empty";
        _itemTooltip.text = "";
        _itemStat.text = "";
    }

    //===================================================================
    //마우스 드래그드랍 아이템
    /// <summary> 슬롯에 클릭하는 경우 </summary>
    private void OnPointerDown()
    {
        // Left Click : Begin Drag
        if (Input.GetMouseButtonDown(_leftClick))
        {
            _beginDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();
            //장비슬롯이아니면 리턴
            if (_beginDragSlot != null)
            {
                if (!_beginDragSlot.isEquipmentSlot)
                {
                    return;
                }
            }

            // 아이템을 갖고 있는 슬롯만 해당
            if (_beginDragSlot != null && _beginDragSlot.HasItem && _beginDragSlot.IsAccessible)
            {

                // 위치 기억, 참조 등록
                _beginDragIconTransform = _beginDragSlot.IconRect.transform;
                _beginDragIconPoint = _beginDragIconTransform.position;
                _beginDragCursorPoint = Input.mousePosition;

                // 맨 위에 보이기
                _beginDragSlotSiblingIndex = _beginDragSlot.transform.GetSiblingIndex();
                _beginDragSlot.transform.SetAsLastSibling();

                // 해당 슬롯의 하이라이트 이미지를 아이콘보다 뒤에 위치시키기
                _beginDragSlot.SetHighlightOnTop(false);
            }
            else
            {
                _beginDragSlot = null;
            }
        }
    }
    /// <summary> 드래그하는 도중 </summary>
    private void OnPointerDrag()
    {
        if (_beginDragSlot == null) return;
        if (!_beginDragSlot.isQuickSlot) return;
        if (Input.GetMouseButton(_leftClick))
        {
            // 위치 이동
            _beginDragIconTransform.position =
                _beginDragIconPoint + (Input.mousePosition - _beginDragCursorPoint);
        }
    }
    /// <summary> 클릭을 뗄 경우 </summary>
    private void OnPointerUp()
    {
        if (Input.GetMouseButtonUp(_leftClick))
        {
            // End Drag
            if (_beginDragSlot != null)
            {
                //이큅슬롯일때만 활성화
                if (!_beginDragSlot.isEquipmentSlot) return;

                // 위치 복원
                _beginDragIconTransform.position = _beginDragIconPoint;

                // UI 순서 복원
                _beginDragSlot.transform.SetSiblingIndex(_beginDragSlotSiblingIndex);

                // 드래그 완료 처리
                EndDrag();

                // 해당 슬롯의 하이라이트 이미지를 아이콘보다 앞에 위치시키기
                _beginDragSlot.SetHighlightOnTop(true);

                // 참조 제거
                _beginDragSlot = null;
                _beginDragIconTransform = null;
            }
        }
    }

    private void EndDrag()
    {
        ItemSlotUI endDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();
        /*스왑은 필요없으니 제외
        if (endDragSlot != null && endDragSlot.isQuickSlot)
        {

            TrySwapItems(_beginDragSlot, endDragSlot);

            return;
        }*/
        //인벤토리에 놓은 경우
        if (endDragSlot != null && endDragSlot.IsAccessible && !endDragSlot.isEquipmentSlot && !endDragSlot.isQuickSlot)
        {
            Rito.InventorySystem.Item MovedItem = _equipmentInventory.EqItems[_beginDragSlot.Index]; //_quickSlotItems[_beginDragSlot.Index];
            int amount = 0;
            if (MovedItem is CountableItem citem)
            {
                amount = citem.Amount;
            }
            _inventory.Add(MovedItem.Data, amount);//인벤토리에 추가
            _equipmentInventory.RemoveItem(_beginDragSlot.Index);//eq인벤에서 아이템 삭제
        }
        // 슬롯이 아닌 다른 UI 위에 놓은 경우
        else
        {
            Debug.Log("Eq인벤에서 다른UI위에 올려서 아무것도 안함");
        }

    }


}
