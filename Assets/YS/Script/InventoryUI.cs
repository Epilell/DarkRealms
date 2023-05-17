using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    protected Inventory inventory;  // Inventory 클래스의 인스턴를 저장하는 변수
    [SerializeField]
    private GameObject SkillGroup;  // 스킬 UI-----------------------이거랑 
    //[SerializeField]
    //private GameObject profile_in, profile_out;  // 프로필 UI------이거는 UI 열림닫힘을 따로 코드만들면 빼도되는부분임
    [SerializeField]
    private GameObject profile_out;
    public GameObject inventoryPanel;  // 인벤토리 UI 패널 변수

    //여기부터
    protected bool activeInventory = false;  // 인벤토리 UI 패널이 열려있는지?
    protected bool activeProfile = false;
    protected bool activeSkillGroup = false;
    //여기까지도 UI 열림 닫힘을 따로 코드 만들면 필요없는부분임

    protected Slot[] slots;  // 인벤토리의 슬롯들을 저장하는 배열 생성
    public Transform slotHolder;  // 슬롯들을 담고 있는 부모 오브젝트 생성

    public void Start()
    {
        inventory = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();  // 슬롯 초기화
        inventory.onSlotCountChange += SlotChange;  // 슬롯 개수가 변경될 때마다 SlotChange() 함수 호출
        inventory.onChangeItem += RedrawSlotUI;  // 아이템이 추가되거나 제거될 때마다 RedrawSlotUI() 함수 호출
        inventoryPanel.SetActive(activeInventory);  // 인벤토리 비활성화
        //profile_in.SetActive(activeProfile);
    }

    public void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;  // 슬롯 번호 설정

            if (i < inventory.SlotCount)
                slots[i].GetComponent<Button>().interactable = true;  // 슬롯 활성화
            else
                slots[i].GetComponent<Button>().interactable = false;  // 슬롯 비활성화
        }
    }

    public void Update()
    {
        //여기 닫힘기능 같은건 따로 스크립트를 만드시오
        if (Input.GetKeyDown(KeyCode.Tab))  // Tab 키가 눌리면
        {
            // activeInventory 반전(닫혀있으면 열고, 열려있으면 닫기)
            activeInventory = !activeInventory;
            activeProfile = !activeProfile;
            activeSkillGroup = !activeSkillGroup;

            inventoryPanel.SetActive(activeInventory);
            //profile_in.SetActive(activeProfile);
            profile_out.SetActive(!activeProfile);
            SkillGroup.SetActive(!activeSkillGroup);
        }
    }

    public void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();  // 슬롯 초기화
        }

        for (int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].item = inventory.items[i];  // 슬롯에 아이템 추가
            slots[i].UpdateSlotUI();  // 슬롯 UI 업데이트
        }
    }
}