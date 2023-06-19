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


    [Space]
    private bool _showHighlight = true;

    private void Update()
    {
        _ped.position = Input.mousePosition;
        OnPointerDown();
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
    private T RaycastAndGetFirstComponent<T>() where T : Component
    {
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
            return null;

        return _rrList[0].gameObject.GetComponent<T>();
    }
    private void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ItemSlotUI slot = RaycastAndGetFirstComponent<ItemSlotUI>();
            if (slot != null)
            {
                _inventory.Add(_equipmentInventory.UnEquip(slot.Index));
            }
        }

    }
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
        }
        void OnPrevExit()
        {
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
            _itemStat.text = " ������ = "+ wdata.Damage.ToString();
            _itemStat.text += "\n ����ӵ� = " + wdata.Rpm.ToString();
            _itemStat.text += "\n �縴 �� = " + wdata.PelletNum.ToString();
        }
        else if (_equipmentInventory.EqItems[index].Data is ArmorItemData adata )
        {
            _itemStat.text = "���� = "+ adata.Defence.ToString();
            //_itemStat.text += adata.Defence.ToString();
        }
    }
    private void GraphicInit()
    {
        TryGetComponent(out _gr);
        if (_gr == null)
            _gr = gameObject.AddComponent<GraphicRaycaster>();
        // Graphic Raycaster
        _ped = new PointerEventData(EventSystem.current);
        _rrList = new List<RaycastResult>(10);
    }
}
