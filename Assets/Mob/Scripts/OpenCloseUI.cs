using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OpenCloseUI : MonoBehaviour
{
    [Header("Inven1")]
    public Button closeButton1;
    public Button OpenButton1;
    public GameObject Obj1;
    [Header("Inven2")]
    public Button closeButton2;
    public Button OpenButton2;
    public GameObject Obj2;
    [Header("Tester")]
    public GameObject Obj3;
    void Start()
    {
        //closeButton = GetComponent<Button>(); // ��ư�� �����մϴ�
        closeButton1.onClick.AddListener(CloseUI1); // ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
        closeButton2.onClick.AddListener(CloseUI2); // ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
        OpenButton1.onClick.AddListener(OpenUI1);
        OpenButton2.onClick.AddListener(OpenUI2);
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
    public void CloseUI1()
    {
        Obj1.SetActive(false); // UI ������Ʈ ��Ȱ��ȭ
    }
    public void CloseUI2()
    {
        Obj2.SetActive(false); // UI ������Ʈ ��Ȱ��ȭ
    }
    public void OpenUI1()
    {
        Obj1.SetActive(true);
    }
    public void OpenUI2()
    {
        Obj2.SetActive(true);
    }
}
