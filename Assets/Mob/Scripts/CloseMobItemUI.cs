using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CloseMobItemUI : MonoBehaviour
{
    public GameObject mobItemUI;

    public void CloseUI()
    {
        mobItemUI.SetActive(false); // UI ������Ʈ ��Ȱ��ȭ
    }
}
