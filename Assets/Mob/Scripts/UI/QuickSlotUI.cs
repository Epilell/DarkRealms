using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rito.InventorySystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class QuickSlotUI : MonoBehaviour
{
    public static QuickSlotUI instance;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private ItemSlotUI[] quickslot;
    private GameManager gm;

    private Rito.InventorySystem.Item[] _quickSlotItems;
    private int _leftClick = 0;
    private int _rightClick = 1;
    [SerializeField]
    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;

    private ItemSlotUI _pointerOverSlot;
    [SerializeField] private bool _showHighlight = true;

    private ItemSlotUI _beginDragSlot;
    private Transform _beginDragIconTransform;
    private Vector3 _beginDragIconPoint;
    private Vector3 _beginDragCursorPoint;
    private int _beginDragSlotSiblingIndex;
    private Rito.InventorySystem.Item _itemChager;

    private void Awake()
    {
        _quickSlotItems = new Rito.InventorySystem.Item[4];//�����ʱ�ȭ
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();//���� �Ŵ��� ã��

        if (instance != null)  // �κ��丮 �ν��Ͻ��� �����ϸ�
        {
            Destroy(gameObject);  // �ߺ� ���� ������ ���� ���� ���� ������Ʈ�� �ı�
            return;  // ����
        }
        instance = this;  // �ν��Ͻ��� �������� ������ ���� �ν��Ͻ��� �Ҵ�
    }

    // Start is called before the first frame update
    void Start()
    {
        QuickSlotInit();
    }
    private void Update()
    {
        _ped.position = Input.mousePosition;
        HandleQuickSlotInput();
        UnequipQuickSlotItem();
        //OnPointerEnterAndExit();
        OnPointerDown();
        OnPointerDrag();
        OnPointerUp();
    }
    //����Ÿ ����
    #region .
    private void QuickSlotInit()
    {
        _ped = new PointerEventData(EventSystem.current);
        _rrList = new List<RaycastResult>(10);

        Transform QuickSlotHolder = this.transform;
        quickslot = QuickSlotHolder.GetComponentsInChildren<ItemSlotUI>();//slots����
        if (quickslot != null)
        {
            for (int i = 0; i < quickslot.Length; i++)
            {
                quickslot[i].SetSlotIndex(i);
                Updateslot(i);
            }
        }
        inventory = GameObject.FindWithTag("GameController").GetComponentInChildren<Inventory>();
    }

    private void Updateslot(int index)
    {
        Rito.InventorySystem.Item? item;
        if (_quickSlotItems[index] != null)
        {
            item = _quickSlotItems[index];
        }
        else
        {
            item = null;
        }
        // 1. �������� ���Կ� �����ϴ� ���
        if (item != null)
        {
            // ������ ���
            SetItemIcon(index, item.Data.IconSprite);
            if (item is CountableItem ci)
            {
                // 1-1-1. ������ 0�� ���, ������ ����
                if (ci.IsEmpty)
                {
                    _quickSlotItems[index] = null;
                    RemoveIcon();
                    return;
                }
                // 1-1-2. ���� �ؽ�Ʈ ǥ��
                else
                {
                    quickslot[index].SetItemAmount(ci.Amount);
                }
            }
        }
        // 2. �� ������ ��� : ������ ����
        else
        {
            RemoveIcon();
        }

        // ���� : ������ �����ϱ�
        void RemoveIcon()
        {
            RemoveItem(index);
        }
    }
    public void SetItemIcon(int index, Sprite icon)
    {
        if (quickslot[index] != null)
        {
            quickslot[index].SetItem(icon);
        }
    }

    public void RemoveItem(int index)
    {
        quickslot[index].RemoveItem();
        _quickSlotItems[index] = null;
    }
    #endregion
    //������ ������
    #region .
    private void HandleQuickSlotInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseQuickSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseQuickSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseQuickSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseQuickSlot(3);
        }
    }

    private void UseQuickSlot(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < _quickSlotItems.Length)
        {
            Use(slotIndex);

        }
    }

    public void Use(int index)
    {
        if (_quickSlotItems[index] == null) return;

        // ��� ������ �������� ���
        if (_quickSlotItems[index] is IUsableItem uItem)
        {
            // ������ ���
            bool succeeded = uItem.Use();

            if (_quickSlotItems[index].Data.Name.Contains("potion"))
            {
                FindObjectOfType<PotionEffect>().UseEffect(_quickSlotItems[index].Data.Name); // ���� ȿ�� ����
            }

            if (_quickSlotItems[index].Data.Name.Contains("portal"))
            {
                FindObjectOfType<PortalUse>().SpawnPortal(); // ��Ż ����
            }

            if (succeeded)
            {
                Updateslot(index);
            }
        }
    }
    #endregion
    //������ ���� ����
    #region .
    private void UnequipQuickSlotItem()
    {
        if (Input.GetMouseButtonDown(_rightClick))
        {
            //Debug.Log("1�������� �Դ°�");
            ItemSlotUI slot = RaycastAndGetFirstComponent<ItemSlotUI>();
            if (slot != null && slot.HasItem && slot.isQuickSlot)
            {
                //Debug.Log("if�������� ���Դ°�");
                //TryUseItem(slot.Index);
                ItemData QuickslotItemData = _quickSlotItems[slot.Index].Data;
                if (_quickSlotItems[slot.Index] is CountableItem CItem)
                {
                    int QuickSlotItemAmount;
                    QuickSlotItemAmount = CItem.Amount;
                    inventory.Add(QuickslotItemData, QuickSlotItemAmount);
                    Debug.Log(QuickslotItemData.Name);
                    RemoveItem(slot.Index);
                }
            }
            else if (slot == null)
            {
                Debug.Log("slot null��");
            }
        }
    }
    /*
    private T RaycastAndGetFirstComponent<T>() where T : Component
    {
        Debug.Log("�����Խ�ũ��Ʈ�� �����ɽ�Ʈ");
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
            return null;
        Debug.Log(_rrList[0]);
        return _rrList[0].gameObject.GetComponent<T>();
    }*/
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
    }
    #endregion
    //������ �����Կ� �ֱ�
    #region .
    /// <summary>
    /// ������ ������ �ֱ�. ������ �����͸� �ް� �����ϸ� �̹� ������ �������� ������ �� ������ �����ְ� ������ null��ȯ
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public CountableItem QuickSlotAddItem(CountableItemData itemData, int slotindex, int amount = 0)
    {
        CountableItemData Cdata = itemData;
        Rito.InventorySystem.Item ReturnItem = Cdata.CreateItem();
        CountableItem _ReturnItem = ReturnItem as CountableItem;
        _ReturnItem.AddAmountAndGetExcess(amount);

        if (itemData is PortionItemData PItemdata)
        {
            int index = slotindex;
            if (_quickSlotItems[index] == null)
            {
                _quickSlotItems[index] = PItemdata.CreateItem();
                CountableItem _QI = _quickSlotItems[index] as CountableItem;
                _QI.AddAmountAndGetExcess(amount);
                Updateslot(index);
                //Debug.Log("����� �;��ϴµ�");
                return null;
            }
            else
            {
                CountableItem forReturnItem = _quickSlotItems[index] as CountableItem;
                PortionItemData pdata = PItemdata;
                _quickSlotItems[index] = pdata.CreateItem();
                CountableItem _QI = _quickSlotItems[index] as CountableItem;
                _QI.AddAmountAndGetExcess(amount);
                Updateslot(index);
                //Debug.Log("���ΰ��ų�1");
                return forReturnItem;
            }
        }
        else
        {
            Updateslot(slotindex);
            //Debug.Log("���ΰ��ų�2");
            return _ReturnItem;
        }


    }
    #endregion
    //������ ����
    #region .
    /*
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
                Debug.Log(curSlot.Index);
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
            if (_showHighlight)
                curSlot.Highlight(true);
        }
        void OnPrevExit()
        {
            prevSlot.Highlight(false);
        }
    }*/
    /// <summary> ���Կ� Ŭ���ϴ� ��� </summary>
    private void OnPointerDown()
    {
        // Left Click : Begin Drag
        if (Input.GetMouseButtonDown(_leftClick))
        {
            _beginDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();
            //������, ��񽽷��̾ƴϸ� ����
            if (_beginDragSlot != null)
            {
                if (!_beginDragSlot.isQuickSlot)
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
                //�������϶��� Ȱ��ȭ
                if (!_beginDragSlot.isQuickSlot) return;

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
        if (endDragSlot != null && endDragSlot.isQuickSlot)
        {

            TrySwapItems(_beginDragSlot, endDragSlot);

            return;
        }
        //�κ��丮�� ���� ���
        else if (endDragSlot != null && endDragSlot.IsAccessible && !endDragSlot.isEquipmentSlot && !endDragSlot.isQuickSlot)
        {
            Rito.InventorySystem.Item MovedItem = _quickSlotItems[_beginDragSlot.Index];
            int amount = 0;
            if(MovedItem is CountableItem citem)
            {
                amount = citem.Amount;
            }
            inventory.Add(MovedItem.Data, amount);
            _quickSlotItems[_beginDragSlot.Index] = null;
            Updateslot(_beginDragSlot.Index);
        }
        // ������ �ƴ� �ٸ� UI ���� ���� ���
        else
        {
            Debug.Log("�ƹ��͵� ����");
        }

    }
    private void TrySwapItems(ItemSlotUI from, ItemSlotUI to)
    {
        from.SwapOrMoveIcon(to);
        Swap(from.Index, to.Index);
    }
    /// <summary> �� �ε����� ������ ��ġ�� ���� ��ü </summary>
    public void Swap(int indexA, int indexB)
    {

        Rito.InventorySystem.Item itemA = _quickSlotItems[indexA];
        Rito.InventorySystem.Item itemB = _quickSlotItems[indexB];

        // 1. �� �� �ִ� �������̰�, ������ �������� ���
        //    indexA -> indexB�� ���� ��ġ��
        if (itemA != null && itemB != null &&
            itemA.Data == itemB.Data &&
            itemA is CountableItem ciA && itemB is CountableItem ciB)
        {
            int maxAmount = ciB.MaxAmount;
            int sum = ciA.Amount + ciB.Amount;

            if (sum <= maxAmount)
            {
                ciA.SetAmount(0);
                ciB.SetAmount(sum);
            }
            else
            {
                ciA.SetAmount(sum - maxAmount);
                ciB.SetAmount(maxAmount);
            }
        }
        // 2. �Ϲ����� ��� : ���� ��ü
        else
        {
            _quickSlotItems[indexA] = itemB;
            _quickSlotItems[indexB] = itemA;
        }

        // �� ���� ���� ����
        Updateslot(indexA);
        Updateslot(indexB);
    }
    #endregion

}

