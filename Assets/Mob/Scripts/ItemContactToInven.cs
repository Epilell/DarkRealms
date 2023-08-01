using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;
using TMPro;

public class ItemContactToInven : MonoBehaviour
{
    SpriteRenderer sr;
    public Rito.InventorySystem.ItemData itemData;
    public int itemCount=1;

    bool isDataON = false;
    private TMP_Text DropItemTooltiptext;
    private GameObject canvas;
    private RectTransform rectTransform;
    private GameObject DropItemTooltips;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        sr = this.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        DropItemTooltips = GameObject.Find("DropItemTooltips");//test
    }
    private void Update()
    {
        if (itemData != null&&isDataON==false)
        {
            isDataON = true;
        }
        if (isDataON)
        {
            sr.sprite = itemData.IconSprite;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Inventory inventoryComponent = GameObject.Find("GameManager").GetComponentInChildren<Inventory>();
            inventoryComponent.Add(this.itemData, itemCount);
            //MakeDropItemTooltips();
            ChangeDropItemTooltips();
            Destroy(this.gameObject);
        }
    }
    private void ChangeDropItemTooltips()
    {
        DropItemTooltips.GetComponent<TMP_Text>().text = itemData.name +" * " +itemCount;
        DropItemTooltips.GetComponent<DropItemTooltips>().CallText();
    }
}
