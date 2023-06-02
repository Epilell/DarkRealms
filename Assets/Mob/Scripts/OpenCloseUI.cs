using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseUI : MonoBehaviour
{
    public GameObject inven;
    public GameObject warehouse;
    [Header("Buttons")]
    public Button InvenCloseButton;
    public Button WarehouseOpenButton;
    public Button WarehouseCloseButton;
    [Header("Tester")]
    public GameObject Tester;
    void Start()
    {
        //closeButton = GetComponent<Button>(); // 버튼을 참조합니다
        InvenCloseButton.onClick.AddListener(CloseInvenUI);// 버튼 클릭 시 ClosePanel 함수를 호출합니다
        WarehouseCloseButton.onClick.AddListener(CloseWarehouseUI); // 버튼 클릭 시 ClosePanel 함수를 호출합니다
        WarehouseOpenButton.onClick.AddListener(OpenWarehouseUI);
    }
    private void Update()
    {
        // 탭 키를 눌렀을 때 인벤토리를 엽니다.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inven.SetActive(!inven.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Tester.SetActive(!Tester.activeSelf);
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
}
