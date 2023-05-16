using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageInvenUI : EscapeInventoryUI
{
    public Button openButton; // 열림 버튼
    public Button closeButton; // 닫기 버튼
    new protected void Update()
    {
        //인게임에서 쓸 시 
        // 마우스 클릭 시 UI 패널을 활성화/비활성화
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapPoint(clickPosition);

            if (collider != null && collider.gameObject == gameObject)
            {
                inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            }
        }

    }


    private void Awake()
    {
        openButton.onClick.AddListener(OpenUI);
        closeButton.onClick.AddListener(CloseUI);
    }

    private void OpenUI()
    {
        inventoryPanel.SetActive(true);
    }

    private void CloseUI()
    {
        inventoryPanel.SetActive(false);
    }
}
