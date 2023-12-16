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

    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;

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
        HandleQuickSlotInput();
        UnequipQuickSlotItem();
    }
    //����Ÿ ����
    #region .
    private void QuickSlotInit()
    {
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
        inventory= GameObject.FindWithTag("GameController").GetComponentInChildren<Inventory>();
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
            ItemSlotUI slot = RaycastAndGetFirstComponent<ItemSlotUI>();

            if (slot != null && slot.HasItem && slot.IsAccessible)
            {
                //TryUseItem(slot.Index);
                ItemData QuickslotItemData = _quickSlotItems[slot.Index].Data;
                int QuickSlotItemAmount;
                if (_quickSlotItems[slot.Index] is CountableItem CItem)
                {
                    QuickSlotItemAmount = CItem.Amount;
                    inventory.Add(QuickslotItemData, QuickSlotItemAmount);
                    RemoveItem(slot.Index);
                }
            }
        }
    }
    private T RaycastAndGetFirstComponent<T>() where T : Component
    {
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
            return null;

        return _rrList[0].gameObject.GetComponent<T>();
    }
    #endregion
    //������ �����Կ� �ֱ�
    /// <summary>
    /// ������ ������ �ֱ�. ������ �����͸� �ް� �����ϸ� �̹� ������ �������� ������ �� ������ �����ְ� ������ null��ȯ
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public CountableItem QuickSlotAddItem(CountableItemData itemData, int slotindex, int amount=1)
    {
        CountableItemData Cdata = itemData;
        Rito.InventorySystem.Item ReturnItem = Cdata.CreateItem();
        CountableItem _ReturnItem = ReturnItem as CountableItem;
        _ReturnItem.AddAmountAndGetExcess(amount);

        if (itemData is IUsableItem UItem)
        {
            if (UItem is PortionItem PItem)
            {
                int index = slotindex;
                if (_quickSlotItems[index] == null)
                {
                    PortionItemData pdata = PItem.Data as PortionItemData;
                    _quickSlotItems[index] = pdata.CreateItem();
                    CountableItem _QI = _quickSlotItems[index] as CountableItem;
                    _QI.AddAmountAndGetExcess(amount);
                    Updateslot(index);
                    return null;
                }
                else
                {
                    CountableItem forReturnItem = _quickSlotItems[index] as CountableItem;
                    PortionItemData pdata = PItem.Data as PortionItemData;
                    _quickSlotItems[index] = pdata.CreateItem();
                    CountableItem _QI = _quickSlotItems[index] as CountableItem;
                    _QI.AddAmountAndGetExcess(amount);
                    Updateslot(index);
                    return forReturnItem;
                }
            }
            else
            {
                return _ReturnItem;
            }

        }
        else
        {
            return _ReturnItem;
        }
    }
}

