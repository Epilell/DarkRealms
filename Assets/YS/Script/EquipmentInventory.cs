using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rito.InventorySystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EquipmentInventory : MonoBehaviour
{
    public Inventory _inventory;
    public static EquipmentInventory instance;  // 장비 인벤토리 인스턴스
    public P_Data playerData;

    private Rito.InventorySystem.Item[] _eqitems;
    public Rito.InventorySystem.Item[] EqItems { get => _eqitems; }


    private List<RaycastResult> _rrList;
    private GraphicRaycaster _gr;
    private PointerEventData _ped;


    private ItemSlotUI[] slots;
    public Transform slotHolder;
    public GameManager gm;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.Find("InventoryB") != null)
        {
            _eqitems = new Rito.InventorySystem.Item[5];
            slotHolder = GameObject.Find("InventoryB").transform;
            slots = slotHolder.GetComponentsInChildren<ItemSlotUI>();

            if (slots != null)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    slots[i].SetSlotIndex(i);
                    Updateslot(i);
                }
            }

            gm = GetComponentInParent<GameManager>();
            gm.LoadEq(this);

            if (slots != null)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    Updateslot(i);
                }
            }
        }
    }
    private void Awake()
    {
        _eqitems = new Rito.InventorySystem.Item[5];
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        //gm.SaveEquipment();

        if (instance != null)  // 인벤토리 인스턴스가 존재하면
        {
            Destroy(gameObject);  // 중복 생성 방지를 위해 현재 게임 오브젝트를 파괴
            return;  // 종료
        }
        instance = this;  // 인스턴스가 존재하지 않으면 현재 인스턴스를 할당
        
        slots = slotHolder.GetComponentsInChildren<ItemSlotUI>();
        if (slots != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetSlotIndex(i);
                Updateslot(i);
            }
        }
        if (GameObject.Find("InventoryB").transform != null)
        {
            slotHolder = GameObject.Find("InventoryB").transform;
            gm = GetComponentInParent<GameManager>();
            gm.LoadEq(this);
        }


        Init();
    }
    private void OnDestroy()
    {
        //gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        //gm.SaveEq(this);
    }
    private void Start() // 장비 슬롯 초기화
    {
        StartCoroutine("SaveEveryMinute", 5f);
    }
    private void Update()
    {
        _ped.position = Input.mousePosition;
        OnPointerDown();
    }

    private void Init()
    {
        TryGetComponent(out _gr);
        if (_gr == null)
            _gr = gameObject.AddComponent<GraphicRaycaster>();
        // Graphic Raycaster
        _ped = new PointerEventData(EventSystem.current); // 이 줄을 추가하여 _ped를 초기화합니다.
        _rrList = new List<RaycastResult>(10);
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
                UnEquip(slot.Index);

            }
        }

    }
    /// <summary>
    /// 장착해제
    /// </summary>
    /// <param name="index"></param>
    public void UnEquip(int index)
    {
        _inventory.Add(_eqitems[index].Data);
        _eqitems[index] = null;
        slots[index].RemoveItem();
    }
    private IEnumerator SaveEveryMinute(float minute)
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        gm.SaveEq(this);
        yield return new WaitForSeconds(minute);
        StartCoroutine("SaveEveryMinute", minute);
    }
    /// <summary>
    /// 장비하기. 아이템 데이터를 받고 장비시키며 이미 장비한 아이템이 있으면 그 데이터 돌려주고 없으면 null반환
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public ItemData Add(ItemData itemData)
    {
        if (itemData is EquipmentItemData eqData)
        {
            int index = FindSlot(eqData);
            if (index == 5)
            {
                return itemData;
            }
            else
            {
                if (_eqitems[index] == null)
                {
                    _eqitems[index] = itemData.CreateItem();
                    Updateslot(index);
                    //아이템 장비 효과
                    ItemEffect(_eqitems[index].Data as EquipmentItemData);
                    return null;
                }
                else
                {
                    ItemData forReturnItemData = _eqitems[index].Data;
                    _eqitems[index] = itemData.CreateItem();
                    Updateslot(index);
                    //아이템 장비 효과
                    ItemEffect(_eqitems[index].Data as EquipmentItemData);
                    return forReturnItemData;
                }
            }

        }
        else
        {
            return itemData;
        }
    }

    public int FindSlot(EquipmentItemData eqdata)  // 빈 슬롯 찾기
    {
        if (eqdata.itemType == "helmet")
        {
            return 0;
        }
        else if (eqdata.itemType == "leg")
        {
            return 1;
        }
        else if (eqdata.itemType == "weapon")
        {
            return 2;
        }
        else if (eqdata.itemType == "armor")
        {
            return 3;
        }
        else if (eqdata.itemType == "shoes")
        {
            return 4;
        }
        else return 5;
    }

    private void Updateslot(int index)
    {
        Rito.InventorySystem.Item? item;
        if (_eqitems[index] != null)
        {
            item = _eqitems[index];
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
        if (slots[index] != null)
        {
            slots[index].SetItem(icon);
        }
    }

    public void RemoveItem(int index)
    {
        slots[index].RemoveItem();
    }
    /// <summary>
    /// 장비 아이템 장착 효과
    /// </summary>
    /// <param name="eqdata"></param>
    private void ItemEffect(EquipmentItemData eqdata)
    {
        int itemNum = FindSlot(eqdata);
        if (eqdata is ArmorItemData armdata)
        {
            switch (itemNum)
            {
                case 0:
                    playerData.Helmet = armdata.Defence;
                    break;
                case 1:
                    playerData.Leg = armdata.Defence;
                    break;
                case 3:
                    playerData.Body = armdata.Defence;
                    break;
                case 4:
                    playerData.Shoes = armdata.Defence;
                    break;
            }
        }
        else if (eqdata is WeaponItemData wdata)
        {
            //playerData.Damage = wdata.Damage;
            //playerData.Rpm = wdata.Rpm;
            //playerData.PelletNum = wdata.PelletNum
        }
        else
        {
            Debug.Log("장비아이템이 아님");
        }
    }
}