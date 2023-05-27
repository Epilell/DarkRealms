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
        //closeButton = GetComponent<Button>(); // 버튼을 참조합니다
        closeButton1.onClick.AddListener(CloseUI1); // 버튼 클릭 시 ClosePanel 함수를 호출합니다
        closeButton2.onClick.AddListener(CloseUI2); // 버튼 클릭 시 ClosePanel 함수를 호출합니다
        OpenButton1.onClick.AddListener(OpenUI1);
        OpenButton2.onClick.AddListener(OpenUI2);
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
    public void CloseUI1()
    {
        Obj1.SetActive(false); // UI 오브젝트 비활성화
    }
    public void CloseUI2()
    {
        Obj2.SetActive(false); // UI 오브젝트 비활성화
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
