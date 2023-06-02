using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rito.InventorySystem;

public class OpenCloseUI : MonoBehaviour
{
    [Header("Invens")]
    public GameObject inven;
    public GameObject ?warehouse;
    [Header("Buttons")]
    public Button ?InvenCloseButton;
    public Button ?WarehouseOpenButton;
    public Button ?WarehouseCloseButton;
    public Button? UpgradeOpenButton;
    public Button? UpgradeCloseButton;
    [Header("Tester")]
    public GameObject Tester;
    public GameObject Upgrade;
    void Start()
    {
        //closeButton = GetComponent<Button>(); // ��ư�� �����մϴ�
        if (inven!=null&& InvenCloseButton != null)
        {
            InvenCloseButton.onClick.AddListener(CloseInvenUI);// ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
        }
        if (warehouse != null)
        {
            if (WarehouseCloseButton != null)
            {
                WarehouseCloseButton.onClick.AddListener(CloseWarehouseUI); // ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
            }
            if (WarehouseOpenButton != null)
            {
                WarehouseOpenButton.onClick.AddListener(OpenWarehouseUI);
            }
        }
        if (Upgrade != null)
        {
            if (UpgradeOpenButton != null)
            {
                UpgradeOpenButton.onClick.AddListener(OpenCloseUpgrade); 
            }
            if (UpgradeCloseButton != null)
            {
                UpgradeCloseButton.onClick.AddListener(OpenCloseUpgrade);
            }
        }
    }
    private void Update()
    {
        if(inven != null)
        {
            // �� Ű�� ������ �� �κ��丮�� ���ϴ�.
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inven.SetActive(!inven.activeSelf);
                /*Inventory _inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
                _inventory.SaveInven();
                _inventory.LoadInven();*/
                InventoryUI _inventoryUI = inven.GetComponent<InventoryUI>();
                _inventoryUI.UpdateAllSlotFilters();
            }
        }
        if (Tester != null)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Tester.SetActive(!Tester.activeSelf);
            }
        }

    }
    public void CloseInvenUI()
    {
        inven.SetActive(false); // UI ������Ʈ ��Ȱ��ȭ
    }
    public void CloseWarehouseUI()
    {
        warehouse.SetActive(false); // UI ������Ʈ ��Ȱ��ȭ
    }
    public void OpenWarehouseUI()
    {
        warehouse.SetActive(true);
    }
    public void OpenCloseUpgrade()
    {
        Upgrade.SetActive(!Upgrade.activeSelf);
    }
}
