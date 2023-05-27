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
        //closeButton = GetComponent<Button>(); // 버튼을 참조합니다
        closeButton1.onClick.AddListener(CloseUI1); // 버튼 클릭 시 ClosePanel 함수를 호출합니다
        closeButton2.onClick.AddListener(CloseUI2); // 버튼 클릭 시 ClosePanel 함수를 호출합니다
    }
    public void CloseUI1()
    {
        Obj1.SetActive(false); // UI 오브젝트 비활성화
    }
    public void CloseUI2()
    {
        Obj2.SetActive(false); // UI 오브젝트 비활성화
    }
}
