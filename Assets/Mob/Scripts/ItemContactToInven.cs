using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;
using TMPro;

public class ItemContactToInven : MonoBehaviour
{
    public GameObject ItemSpriteObj;
    SpriteRenderer sr;
    public Rito.InventorySystem.ItemData itemData;
    public int itemCount=1;

    bool isDataON = false;
    private TMP_Text DropItemTooltiptext;
    private GameObject canvas;
    private RectTransform rectTransform;
    private GameObject DropItemTooltips;


    private ItemDropMove idm;

    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        sr = ItemSpriteObj.GetComponent<SpriteRenderer>();
        idm = GetComponent<ItemDropMove>();
    }
    private void Start()
    {
        DropItemTooltips = GameObject.Find("DropItemTooltips");//test
        idm.DropMoveStart();
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
