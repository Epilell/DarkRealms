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

    //�׷��� ����
    private List<RaycastResult> _rrList;
    [SerializeField]
    private GraphicRaycaster _gr;
    private PointerEventData _ped;

    private Inventory _inventory;
    private EquipmentInventory _equipmentInventory;

    private ItemSlotUI _pointerOverSlot; // ���� �����Ͱ� ��ġ�� ���� ����
    private ItemSlotUI _beginDragSlot; // ���� �巡�׸� ������ ����

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
        //Debug.Log("�����Խ�ũ��Ʈ�� �����ɽ�Ʈ");
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
        {
            //Debug.Log("����ĳ��Ʈ�� �ƹ� �͵� �������� �ʾҽ��ϴ�.");
            return null;
        }

        //Debug.Log("����ĳ��Ʈ�� ������ ������Ʈ: " + _rrList[0].gameObject.name);
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
        // ���� �������� ����
        var prevSlot = _pointerOverSlot;

        // ���� �������� ����
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
                if (_index >= 0 && _index < _equipmentInventory.EqItems.Length) // yourArray���� ������ �迭�� ���� �մϴ�.
                {
                    ShowTooltips(_index);
                }
                else
                {
                    // ��ȿ���� ���� �ε����� ���� ó���� �߰��� �� �ֽ��ϴ�.
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
    //���콺 �巡�׵�� ������
    /// <summary> ���Կ� Ŭ���ϴ� ��� </summary>
    private void OnPointerDown()
    {
        // Left Click : Begin Drag
        if (Input.GetMouseButtonDown(_leftClick))
        {
            _beginDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();
            //��񽽷��̾ƴϸ� ����
            if (_beginDragSlot != null)
            {
                if (!_beginDragSlot.isEquipmentSlot)
                {
                    return;
                }
            }

            // �������� ���� �ִ� ���Ը� �ش�
            if (_beginDragSlot != null && _beginDragSlot.HasItem && _beginDragSlot.IsAccessible)
            {

                // ��ġ ���, ���� ���
                _beginDragIconTransform = _beginDragSlot.IconRect.transform;
                _beginDragIconPoint = _beginDragIconTransform.position;
                _beginDragCursorPoint = Input.mousePosition;

                // �� ���� ���̱�
                _beginDragSlotSiblingIndex = _beginDragSlot.transform.GetSiblingIndex();
                _beginDragSlot.transform.SetAsLastSibling();

                // �ش� ������ ���̶���Ʈ �̹����� �����ܺ��� �ڿ� ��ġ��Ű��
                _beginDragSlot.SetHighlightOnTop(false);
            }
            else
            {
                _beginDragSlot = null;
            }
        }
    }
    /// <summary> �巡���ϴ� ���� </summary>
    private void OnPointerDrag()
    {
        if (_beginDragSlot == null) return;
        if (!_beginDragSlot.isQuickSlot) return;
        if (Input.GetMouseButton(_leftClick))
        {
            // ��ġ �̵�
            _beginDragIconTransform.position =
                _beginDragIconPoint + (Input.mousePosition - _beginDragCursorPoint);
        }
    }
    /// <summary> Ŭ���� �� ��� </summary>
    private void OnPointerUp()
    {
        if (Input.GetMouseButtonUp(_leftClick))
        {
            // End Drag
            if (_beginDragSlot != null)
            {
                //��Ţ�����϶��� Ȱ��ȭ
                if (!_beginDragSlot.isEquipmentSlot) return;

                // ��ġ ����
                _beginDragIconTransform.position = _beginDragIconPoint;

                // UI ���� ����
                _beginDragSlot.transform.SetSiblingIndex(_beginDragSlotSiblingIndex);

                // �巡�� �Ϸ� ó��
                EndDrag();

                // �ش� ������ ���̶���Ʈ �̹����� �����ܺ��� �տ� ��ġ��Ű��
                _beginDragSlot.SetHighlightOnTop(true);

                // ���� ����
                _beginDragSlot = null;
                _beginDragIconTransform = null;
            }
        }
    }

    private void EndDrag()
    {
        ItemSlotUI endDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();
        /*������ �ʿ������ ����
        if (endDragSlot != null && endDragSlot.isQuickSlot)
        {

            TrySwapItems(_beginDragSlot, endDragSlot);

            return;
        }*/
        //�κ��丮�� ���� ���
        if (endDragSlot != null && endDragSlot.IsAccessible && !endDragSlot.isEquipmentSlot && !endDragSlot.isQuickSlot)
        {
            Rito.InventorySystem.Item MovedItem = _equipmentInventory.EqItems[_beginDragSlot.Index]; //_quickSlotItems[_beginDragSlot.Index];
            int amount = 0;
            if (MovedItem is CountableItem citem)
            {
                amount = citem.Amount;
            }
            _inventory.Add(MovedItem.Data, amount);//�κ��丮�� �߰�
            _equipmentInventory.RemoveItem(_beginDragSlot.Index);//eq�κ����� ������ ����
        }
        // ������ �ƴ� �ٸ� UI ���� ���� ���
        else
        {
            Debug.Log("Eq�κ����� �ٸ�UI���� �÷��� �ƹ��͵� ����");
        }

    }


}
