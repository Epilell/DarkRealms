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
        _ped.position = Input.mousePosition;
        HandleQuickSlotInput();
        UnequipQuickSlotItem();
        //OnPointerEnterAndExit();
        OnPointerDown();
        OnPointerDrag();
        OnPointerUp();
    }
    //데이타 관련
    #region .
    private void QuickSlotInit()
    {
        _ped = new PointerEventData(EventSystem.current);
        _rrList = new List<RaycastResult>(10);

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
        // 1. 아이템이 슬롯에 존재하는 경우
        if (item != null)
        {
            // 아이콘 등록
            SetItemIcon(index, item.Data.IconSprite);
            if (item is CountableItem ci)
            {
                // 1-1-1. 수량이 0인 경우, 아이템 제거
                if (ci.IsEmpty)
                {
                    _quickSlotItems[index] = null;
                    RemoveIcon();
                    return;
                }
                // 1-1-2. 수량 텍스트 표시
                else
                {
                    quickslot[index].SetItemAmount(ci.Amount);
                }
            }
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
        _quickSlotItems[index] = null;
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
            //Debug.Log("1번까지는 왔는가");
            ItemSlotUI slot = RaycastAndGetFirstComponent<ItemSlotUI>();
            if (slot != null && slot.HasItem && slot.isQuickSlot)
            {
                //Debug.Log("if문까지는 들어왔는가");
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
                Debug.Log("slot null임");
            }
        }
    }
    /*
    private T RaycastAndGetFirstComponent<T>() where T : Component
    {
        Debug.Log("퀵슬롯스크립트의 레이케스트");
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
            return null;
        Debug.Log(_rrList[0]);
        return _rrList[0].gameObject.GetComponent<T>();
    }*/
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
    }
    #endregion
    //아이템 퀵슬롯에 넣기
    #region .
    /// <summary>
    /// 퀵슬롯 아이템 넣기. 아이템 데이터를 받고 장착하며 이미 장착한 아이템이 있으면 그 데이터 돌려주고 없으면 null반환
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
                //Debug.Log("여기로 와야하는데");
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
                //Debug.Log("어디로간거냐1");
                return forReturnItem;
            }
        }
        else
        {
            Updateslot(slotindex);
            //Debug.Log("어디로간거냐2");
            return _ReturnItem;
        }


    }
    #endregion
    //포인터 관련
    #region .
    /*
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
    /// <summary> 슬롯에 클릭하는 경우 </summary>
    private void OnPointerDown()
    {
        // Left Click : Begin Drag
        if (Input.GetMouseButtonDown(_leftClick))
        {
            _beginDragSlot = RaycastAndGetFirstComponent<ItemSlotUI>();
            //퀵슬롯, 장비슬롯이아니면 리턴
            if (_beginDragSlot != null)
            {
                if (!_beginDragSlot.isQuickSlot)
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
                //퀵슬롯일때만 활성화
                if (!_beginDragSlot.isQuickSlot) return;

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
        if (endDragSlot != null && endDragSlot.isQuickSlot)
        {

            TrySwapItems(_beginDragSlot, endDragSlot);

            return;
        }
        //인벤토리에 놓은 경우
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
        // 슬롯이 아닌 다른 UI 위에 놓은 경우
        else
        {
            Debug.Log("아무것도 안함");
        }

    }
    private void TrySwapItems(ItemSlotUI from, ItemSlotUI to)
    {
        from.SwapOrMoveIcon(to);
        Swap(from.Index, to.Index);
    }
    /// <summary> 두 인덱스의 아이템 위치를 서로 교체 </summary>
    public void Swap(int indexA, int indexB)
    {

        Rito.InventorySystem.Item itemA = _quickSlotItems[indexA];
        Rito.InventorySystem.Item itemB = _quickSlotItems[indexB];

        // 1. 셀 수 있는 아이템이고, 동일한 아이템일 경우
        //    indexA -> indexB로 개수 합치기
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
        // 2. 일반적인 경우 : 슬롯 교체
        else
        {
            _quickSlotItems[indexA] = itemB;
            _quickSlotItems[indexB] = itemA;
        }

        // 두 슬롯 정보 갱신
        Updateslot(indexA);
        Updateslot(indexB);
    }
    #endregion

}

