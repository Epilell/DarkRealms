using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem; // PortionItemData �������� ���� �߰�
using System.Text.RegularExpressions; // ���� ǥ���� ����ϱ� ���� �߰�

public class PotionEffect : MonoBehaviour
{
    public Player player; // �÷��̾� ��ü
    public List<PortionItemData> portionItemDataList; // ������ ������ ��� ����Ʈ

    public void UseEffect(string itemName)
    {
        PortionItemData targetItemData = null; // ���ϴ� ������ �����͸� ������ ����

        // ������ ������ ����Ʈ�� ���鼭 �Ű������� ���� itemName�� �̸��� ��ġ�ϴ� ������ ã��
        foreach (PortionItemData itemData in portionItemDataList)
        {
            if (itemData.Name == itemName) // �Ű������� ���� itemName�� �̸��� ��ġ�ϴ� �������� �ִ��� Ȯ��
            {
                targetItemData = itemData; // ã�� ������ ������ ����
                break; // ������ ã������ ����
            }
        }

        if (targetItemData != null) // ã�� ������ �����Ͱ� �ִٸ�
        {
            // ������ ���� ���� ȿ�� �ֱ�
            // e.g. �̸��� hp�� ���ԵǸ� ü�� ȸ�� ������

            // ȿ�� ���� ���� �ϱ�
            string containWord = Regex.Match(itemName, "hp||power||armor||cooldown||blood||immunity||release||undying", RegexOptions.IgnoreCase).Value.ToLower();

            switch (containWord)
            {
                // ���� ���� ����: ���ʿ��� �͵��� ���� ����
                case "hp": // ü�� ȸ��
                    player.P_Heal(targetItemData.Value);
                    break;
                case "power": // ���ݷ� ����
                    Debug.Log("power!");
                    break;
                case "armor": // ���� ����
                    Debug.Log("armor!");
                    break;
                case "cooldown": // ��Ÿ�� ����
                    Debug.Log("cooldown!");
                    break;
                case "blood": // ����
                    Debug.Log("blood!");
                    break;
                case "immunity": // �鿪
                    Debug.Log("immunity!");
                    break;
                case "release": // �̻� ����
                    Debug.Log("release!");
                    break;
                case "undying": // �һ�
                    Debug.Log("undying!");
                    break;
                default: break;
            }
        }
        else { Debug.Log("��� �Ұ�"); }
    }
}
/*
    - �����Ϸ��� ���� ���� -

    public int ID => _id;
    public string Name => _name;
    public string Tooltip => _tooltip;
    public Sprite IconSprite => _iconSprite;

    [SerializeField] private int      _id;
    [SerializeField] private string   _name;    // ������ �̸�
    [Multiline]
    [SerializeField] private string   _tooltip; // ������ ����
    [SerializeField] private Sprite   _iconSprite; // ������ ������
    [SerializeField] private GameObject _dropItemPrefab; // �ٴڿ� ������ �� ������ ������

    CountableItemData : ItemData ����
    {
        public int MaxAmount => _maxAmount;
        [SerializeField] private int _maxAmount = 99;
    }

    PortionItemData : CountableItemData ����
    {
        ȿ����(ȸ���� ��)

        public float Value => _value;
        [SerializeField] private float _value;
        public override Item CreateItem()
        {
            return new PortionItem(this);
        }
    }


    - ���� ǥ�������� itemName�� ���Ե� Ű���� ã�� -

    Match(�˻��� ���ڿ�, ã�� ����, RegexOptions: ��ġ �ɼ��� �����ϴ� ������ ���� ��Ʈ ����);
    RegexOptions.IgnoreCase: ��ҹ��ڸ� ���� ���� ��ġ �׸��� ã���� ����
    .Value.ToLower(); : ���ڿ��� �ҹ��ڷ� ��ȯ ����
    Regex.Match(itemName, "hp", RegexOptions.IgnoreCase).Value.ToLower()
    : itemName���� "hp"��� ���ϰ� ��ҹ��� ���� ���� ��ġ�ϴ� ���ڿ��� ã�Ƽ� �ҹ��ڷ� �ްڴ�.
*/