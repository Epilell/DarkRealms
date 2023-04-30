using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CloseMobItemUI : MonoBehaviour
{
    public GameObject mobItemUI;
    void Start()
    {
        Button closeButton = GetComponent<Button>(); // ��ư�� �����մϴ�
        closeButton.onClick.AddListener(CloseUI); // ��ư Ŭ�� �� ClosePanel �Լ��� ȣ���մϴ�
    }
    void CloseUI()
    {
        mobItemUI.SetActive(false); // UI ������Ʈ ��Ȱ��ȭ
    }
}
