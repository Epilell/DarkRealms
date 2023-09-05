using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rito.InventorySystem;

public class OpenCloseUI : MonoBehaviour
{
    [Header("Invens")]
    public GameObject inven;
    public GameObject eqInven;
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

    private bool firstOff=true;
    void Start()
    {
        //closeButton = GetComponent<Button>(); // 버튼을 참조합니다
        if (inven!=null&& InvenCloseButton != null)
        {
            InvenCloseButton.onClick.AddListener(CloseInvenUI);// 버튼 클릭 시 ClosePanel 함수를 호출합니다
        }
        if (warehouse != null)
        {
            if (WarehouseCloseButton != null)
            {
                WarehouseCloseButton.onClick.AddListener(CloseWarehouseUI); // 버튼 클릭 시 ClosePanel 함수를 호출합니다
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
        if(inven != null&& eqInven!=null)
        {
            if (firstOff)
            {
                firstOff = false;
                inven.SetActive(false);
                eqInven.SetActive(false);
            }
            // 탭 키를 눌렀을 때 인벤토리를 엽니다.
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inven.SetActive(!inven.activeSelf);
                eqInven.SetActive(!eqInven.activeSelf);
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
        inven.SetActive(false); // UI 오브젝트 비활성화
    }
    public void CloseWarehouseUI()
    {
        warehouse.SetActive(false); // UI 오브젝트 비활성화
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
