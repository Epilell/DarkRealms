using System;
using UnityEngine;

public class EquipmentInventory : MonoBehaviour
{
    protected OldInventory oldInventory;

    public static EquipmentInventory instance;  // 인벤토리 인스턴스
    private EquipmentSlot[] slots;
    public EquipmentData Eqdata;
    public Transform slotHolder;

    public Equip equip;

    private void Awake()
    {
        if (instance != null)  // 인벤토리 인스턴스가 존재하면
        {
            Destroy(gameObject);  // 중복 생성 방지를 위해 현재 게임 오브젝트를 파괴
            return;  // 종료
        }
        instance = this;  // 인스턴스가 존재하지 않으면 현재 인스턴스를 할당
    }

    private void Start() // 장비 슬롯 초기화
    {
        oldInventory = OldInventory.instance;
        
        slots = slotHolder.GetComponentsInChildren<EquipmentSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                slots[i].item = Eqdata.EqItems[i];
                slots[i].item = null;
            }
        }
    }

    public Slot FindEmptySlot(String itemName)  // 빈 슬롯 찾기
    {
        if (itemName == "helmet")
        {
            return slots[0];
        }
        else if (itemName == "armor")
        {
            return slots[1];
        }
        else if (itemName == "knee")
        {
            return slots[2];
        }
        else if (itemName == "shoes")
        {
            return slots[3];
        }
        else if (itemName == "rifle")
        {
            return slots[4];
        }
        else if (itemName == "shotgun")
        {
            return slots[5];
        }
        else return null;
    }

    public void AddItems(Item item, Slot targetSlot, int slotNum)  // 빈 슬롯에 넣음
    {
        // image가 null이면 아이템이 없는 걸로, 이미지가 다르면 다른 아이템으로 간주
        if (targetSlot != null && /*targetSlot.item.itemImage != item.itemImage || */targetSlot.item == null /*targetSlot.item.itemName == ""*/) // 해당 슬롯에 같은 아이템이 있으면 추가하지 않음
        {
            /*if (targetSlot.item != null && targetSlot.item.itemImage != null)
            {
                equip.RemoveEquipmentEffect(targetSlot.item.itemName, targetSlot.item.effectPoint);
                oldInventory.AddItem(targetSlot.item); // 다른 아이템이 존재하면 아이템 교체
            }
            */

            // 장비 슬롯에 아이템 추가
            targetSlot.item = item;
            targetSlot.UpdateSlotUI();

            RemoveItem(slotNum); // 임시로 추가

            // 종류 별로 효과 부여
            /*if (item.itemName == "helmet")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }
            else if (item.itemName == "armor")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }
            else if (item.itemName == "knee")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }
            else if (item.itemName == "shoes")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }
            else if (item.itemName == "rifle")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }
            else if (item.itemName == "shotgun")
            {
                equip.ApplyEquipmentEffect(item.itemName, item.effectPoint);
                RemoveItem(slotNum);
            }*/
        }
        else { }
    }

    public void RemoveItem(int _index)
    {
        OldInventory.instance.items.RemoveAt(_index);  // 아이템 리스트에서 해당 인덱스의 아이템 제거
        OldInventory.instance.onChangeItem.Invoke();  // 아이템 변경 이벤트 호출
    }
}