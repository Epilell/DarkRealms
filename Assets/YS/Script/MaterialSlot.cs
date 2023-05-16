using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSlot : MonoBehaviour
{
    public int slotNum;  // ���� ��ȣ
    public Item item;
    public Image itemIcon;  // ������

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;  // ������ �̹��� ����
        itemIcon.enabled = true;  // ������ Ȱ��ȭ
        itemIcon.gameObject.SetActive(true);  // ������ ������Ʈ Ȱ��ȭ
    }

    public void RemoveSlot()
    {
        item = null;  // ���� �� ������ ����
        itemIcon.gameObject.SetActive(false);  // ������ ������Ʈ ��Ȱ��ȭ
    }
}