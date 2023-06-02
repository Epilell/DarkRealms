using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseUI : MonoBehaviour
{
    public GameObject inven;
    public GameObject warehouse;
    [Header("Buttons")]
    public Button InvenOpenButton;
    public Button InvenCloseButton;
    public Button WarehouseOpenButton;
    public Button WarehouseCloseButton;
    [Header("Tester")]
    public GameObject Obj3;
    void Start()
    {
        //closeButton = GetComponent<Button>(); // 버튼을 참조합니다
        InvenCloseButton.onClick.AddListener(CloseInvenUI)// 버튼 클릭 시 ClosePanel 함수를 호출합니다
        WarehouseCloseButton.onClick.AddListener(CloseWarehouseUI); // 버튼 클릭 시 ClosePanel 함수를 호출합니다
        OpenButton1.onClick.AddListener(OpenUI1);
        WarehouseOpenButton.onClick.AddListener(OpenUI2);
    }
    private void Update()
    {
        // 탭 키를 눌렀을 때 인벤토리를 엽니다.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Obj1.SetActive(!Obj1.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Obj3.SetActive(!Obj3.activeSelf);
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
    public void OpenUI1()
    {
        Obj1.SetActive(true);
    }
    public void OpenWarehouseUI2()
    {
        warehouse.SetActive(true);
    }
}
