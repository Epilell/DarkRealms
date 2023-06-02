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
        //closeButton = GetComponent<Button>(); // ��ư�� �����մϴ�
        InvenCloseButton.onClick.AddListener(CloseInvenUI)// ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
        WarehouseCloseButton.onClick.AddListener(CloseWarehouseUI); // ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
        OpenButton1.onClick.AddListener(OpenUI1);
        WarehouseOpenButton.onClick.AddListener(OpenUI2);
    }
    private void Update()
    {
        // �� Ű�� ������ �� �κ��丮�� ���ϴ�.
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
        inven.SetActive(false); // UI ������Ʈ ��Ȱ��ȭ
    }
    public void CloseWarehouseUI()
    {
        warehouse.SetActive(false); // UI ������Ʈ ��Ȱ��ȭ
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
