using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageInvenUI : MonoBehaviour
{
    private StorageInven Storage;
    public GameObject StoragePanel;//â�� UI

    protected Slot[] slots;  // �κ��丮�� ���Ե��� �����ϴ� �迭 ����
    public Transform slotHolder;  // ���Ե��� ��� �ִ� �θ� ������Ʈ ����

    public Button openButton; // ���� ��ư
    public Button closeButton; // �ݱ� ��ư
    public void Start()
    {
        Storage = StorageInven.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();  // ���� �ʱ�ȭ
        Storage.slotCountChange += SlotChange;  // ���� ������ ����� ������ SlotChange() �Լ� ȣ��
        Storage.onChangeItem += RedrawSlotUI;  // �������� �߰��ǰų� ���ŵ� ������ RedrawSlotUI() �Լ� ȣ��
        StoragePanel.SetActive(false);
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;  // ���� ��ȣ ����

            if (i < Storage.SlotCount)
                slots[i].GetComponent<Button>().interactable = true;  // ���� Ȱ��ȭ
            else
                slots[i].GetComponent<Button>().interactable = false;  // ���� ��Ȱ��ȭ
        }
    }

    private void Update()
    {
        //�ΰ��ӿ��� �� �� 
        // ���콺 Ŭ�� �� UI �г��� Ȱ��ȭ/��Ȱ��ȭ
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
            slots[i].RemoveSlot();  // ���� �ʱ�ȭ
        }

        for (int i = 0; i < Storage.items.Count; i++)
        {
            slots[i].item = Storage.items[i];  // ���Կ� ������ �߰�
            slots[i].UpdateSlotUI();  // ���� UI ������Ʈ
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