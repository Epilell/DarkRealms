using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CloseMobItemUI : MonoBehaviour
{
    public GameObject mobItemUI;
    void Start()
    {
        Button closeButton = GetComponent<Button>(); // 버튼을 참조합니다
        closeButton.onClick.AddListener(CloseUI); // 버튼 클릭 시 ClosePanel 함수를 호출합니다
    }
    void CloseUI()
    {
        mobItemUI.SetActive(false); // UI 오브젝트 비활성화
    }
}
