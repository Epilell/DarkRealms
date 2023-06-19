using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;
using TMPro;

public class ItemContactToInven : MonoBehaviour
{
    SpriteRenderer sr;
    public Rito.InventorySystem.ItemData itemData;
    bool isDataON = false;
    [SerializeField] private GameObject DropitemTooltips;
    private TMP_Text DropItemTooltiptext;
    private GameObject canvas;
    private RectTransform rectTransform;
    private void Awake()
    {
        canvas = GameObject.Find("Canvas");
        sr = this.GetComponent<SpriteRenderer>();
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
            inventoryComponent.Add(this.itemData);
            MakeDropItemTooltips();
            Destroy(this.gameObject);
        }
    }
    private void MakeDropItemTooltips()
    {
        //드롭아이템 툴팁스 생성
        GameObject clone =Instantiate(DropitemTooltips);
        //parent("Canvas" 오브젝트)의 자식으로 설정 단, UI는 캔버스의 자식으로 설정되어 있어야 화면에 보임
        clone.transform.SetParent(canvas.transform);
        //계층 설정으로 바뀐 크기를 재설정
        clone.transform.localScale = Vector3.one;
        //아이템 위치에 맞게 UI생성
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        clone.GetComponent<RectTransform>().position = screenPosition;



        //텍스트 바꾸기
        DropItemTooltiptext = clone.GetComponent<TMP_Text>();
        DropItemTooltiptext.text = itemData.Name;
    }
}
