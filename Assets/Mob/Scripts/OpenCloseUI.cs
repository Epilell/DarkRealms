using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Invens
{
    public Button closeButton;
    public Button OpenButton;
    public GameObject Obj;
}

public class OpenCloseUI : MonoBehaviour
{
    /*
    public List<Invens> invens;
    [Header("Tester")]
    public GameObject Obj3;
    void Start()
    {
        //closeButton = GetComponent<Button>(); // ��ư�� �����մϴ�
        invens[0].closeButton.onClick.AddListener(CloseUI(Invens[0].obj)); // ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
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
    public void init()
    {
        for(int i = 0; i < invens.Count; i++)
        {
            invens[i].closeButton.onClick.AddListener(CloseUI(invens[i].Obj));
        }

    }
    public void CloseUI(GameObject obj)
    {
        obj.SetActive(false); // UI ������Ʈ ��Ȱ��ȭ
    }
    public void OpenUI1()
    {
        Obj1.SetActive(true);
    }
    public void OpenUI2()
    {
        Obj2.SetActive(true);
    }*/
}
