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
        //��Ӿ����� ������ ����
        GameObject clone =Instantiate(DropitemTooltips);
        //parent("Canvas" ������Ʈ)�� �ڽ����� ���� ��, UI�� ĵ������ �ڽ����� �����Ǿ� �־�� ȭ�鿡 ����
        clone.transform.SetParent(canvas.transform);
        //���� �������� �ٲ� ũ�⸦ �缳��
        clone.transform.localScale = Vector3.one;
        //������ ��ġ�� �°� UI����
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        clone.GetComponent<RectTransform>().position = screenPosition;



        //�ؽ�Ʈ �ٲٱ�
        DropItemTooltiptext = clone.GetComponent<TMP_Text>();
        DropItemTooltiptext.text = itemData.Name;
    }
}
