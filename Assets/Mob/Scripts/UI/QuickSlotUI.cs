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
        _quickSlotItems = new Rito.InventorySystem.Item[4];//슬롯초기화
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();//게임 매니저 찾기

        if (instance != null)  // 인벤토리 인스턴스가 존재하면
        {
            Destroy(gameObject);  // 중복 생성 방지를 위해 현재 게임 오브젝트를 파괴
            return;  // 종료
        }
        instance = this;  // 인스턴스가 존재하지 않으면 현재 인스턴스를 할당
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
    //데이타 관련
    #region .
    private void QuickSlotInit()
    {
        Transform QuickSlotHolder = this.transform;
        quickslot = QuickSlotHolder.GetComponentsInChildren<ItemSlotUI>();//slots설정
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
        // 1. 아이템이 슬롯에 존재하는 경우
        if (item != null)
        {
            // 아이콘 등록
            SetItemIcon(index, item.Data.IconSprite);
        }
        // 2. 빈 슬롯인 경우 : 아이콘 제거
        else
        {
            RemoveIcon();
        }

        // 로컬 : 아이콘 제거하기
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
    //아이템 사용관련
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

        // 사용 가능한 아이템인 경우
        if (_quickSlotItems[index] is IUsableItem uItem)
        {
            // 아이템 사용
            bool succeeded = uItem.Use();

            if (_quickSlotItems[index].Data.Name.Contains("potion"))
            {
                FindObjectOfType<PotionEffect>().UseEffect(_quickSlotItems[index].Data.Name); // 포션 효과 적용
            }

            if (_quickSlotItems[index].Data.Name.Contains("portal"))
            {
                FindObjectOfType<PortalUse>().SpawnPortal(); // 포탈 생성
            }

            if (succeeded)
            {
                Updateslot(index);
            }
        }
    }
    #endregion
    //아이템 장착 해제
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
    //아이템 퀵슬롯에 넣기
    /// <summary>
    /// 퀵슬롯 아이템 넣기. 아이템 데이터를 받고 장착하며 이미 장착한 아이템이 있으면 그 데이터 돌려주고 없으면 null반환
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

