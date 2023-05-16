using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageInvenUI : EscapeInventoryUI
{
    public Button openButton; // ���� ��ư
    public Button closeButton; // �ݱ� ��ư
    new protected void Update()
    {
        //�ΰ��ӿ��� �� �� 
        // ���콺 Ŭ�� �� UI �г��� Ȱ��ȭ/��Ȱ��ȭ
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
