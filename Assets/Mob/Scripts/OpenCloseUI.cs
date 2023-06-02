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
        //closeButton = GetComponent<Button>(); // ��ư�� �����մϴ�
        InvenCloseButton.onClick.AddListener(CloseInvenUI);// ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
        WarehouseCloseButton.onClick.AddListener(CloseWarehouseUI); // ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
        WarehouseOpenButton.onClick.AddListener(OpenWarehouseUI);
    }
    private void Update()
    {
        // �� Ű�� ������ �� �κ��丮�� ���ϴ�.
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
}
