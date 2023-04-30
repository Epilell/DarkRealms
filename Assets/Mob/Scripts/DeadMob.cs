using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMob : MonoBehaviour
{
    public GameObject mobItemUI; // 상자 내용물 UI
    private MobDropItem dropItem;
    private bool itemUIOpen = false;
    void Awake()
    {
        dropItem = GetComponent<MobDropItem>();
    }
    private void OnMouseDown()
    {
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collider = Physics2D.OverlapPoint(clickPos);
        if (itemUIOpen == false)
        {
            if (collider != null && collider.gameObject == gameObject)
            {
                itemUIOpen = true;
                mobItemUI.SetActive(true); // 상자 내용물 UI 활성화
            }
        }
    }
}
