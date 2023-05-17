using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageInvenUI : MonoBehaviour
{
    private StorageInven Storage;
    public GameObject StoragePanel;//창고 UI

    protected Slot[] slots;  // 인벤토리의 슬롯들을 저장하는 배열 생성
    public Transform slotHolder;  // 슬롯들을 담고 있는 부모 오브젝트 생성

    public Button openButton; // 열림 버튼
    public Button closeButton; // 닫기 버튼
    public void Start()
    {
        Storage = StorageInven.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();  // 슬롯 초기화
        Storage.slotCountChange += SlotChange;  // 슬롯 개수가 변경될 때마다 SlotChange() 함수 호출
        Storage.onChangeItem += RedrawSlotUI;  // 아이템이 추가되거나 제거될 때마다 RedrawSlotUI() 함수 호출
        StoragePanel.SetActive(false);
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;  // 슬롯 번호 설정

            if (i < Storage.SlotCount)
                slots[i].GetComponent<Button>().interactable = true;  // 슬롯 활성화
            else
                slots[i].GetComponent<Button>().interactable = false;  // 슬롯 비활성화
        }
    }

    private void Update()
    {
        //인게임에서 쓸 시 
        // 마우스 클릭 시 UI 패널을 활성화/비활성화
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(clickPosition);

            if (collider != null && collider.gameObject == gameObject)
            {
                StoragePanel.SetActive(!StoragePanel.activeSelf);
            }
        }

    }

    public void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();  // 슬롯 초기화
        }

        for (int i = 0; i < Storage.items.Count; i++)
        {
            slots[i].item = Storage.items[i];  // 슬롯에 아이템 추가
            slots[i].UpdateSlotUI();  // 슬롯 UI 업데이트
        }
    }

    private void Awake()
    {
        openButton.onClick.AddListener(OpenUI);
        closeButton.onClick.AddListener(CloseUI);
    }

    private void OpenUI()
    {
        StoragePanel.SetActive(true);
    }

    private void CloseUI()
    {
        StoragePanel.SetActive(false);
    }
}