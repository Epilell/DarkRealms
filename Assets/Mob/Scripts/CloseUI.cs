using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CloseUI : MonoBehaviour
{
    [Header("Inven1")]
    public Button closeButton1;
    public GameObject Obj1;
    [Header("Inven2")]
    public Button closeButton2;
    public GameObject Obj2;
    void Start()
    {
        //closeButton = GetComponent<Button>(); // ��ư�� �����մϴ�
        closeButton1.onClick.AddListener(CloseUI1); // ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
        closeButton2.onClick.AddListener(CloseUI2); // ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
    }
    public void CloseUI1()
    {
        Obj1.SetActive(false); // UI ������Ʈ ��Ȱ��ȭ
    }
    public void CloseUI2()
    {
        Obj2.SetActive(false); // UI ������Ʈ ��Ȱ��ȭ
    }
}
