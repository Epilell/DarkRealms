using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
[Serializable]
public class SaveData
{
    public int amount;
    public int id;
}

public class GameManager : MonoBehaviour
{
    public ItemDB idb;
    public List<SaveData> saveDatas;
    private static GameManager instance;
    [SerializeField]
    private GameObject Inventory;
    [SerializeField]
    private GameObject? Warehouse;

    //unitys
    #region .

    private void Awake()
    {
        LoadInven();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(Inventory);
        DontDestroyOnLoad(this.gameObject);
    }
    /*
    int counter = 0;
    public void FixedUpdate()
    {
        counter++;
        if (counter < 150)
        {
            SaveInven();
            counter = 0;
        }
    }
    public void OnDestroy()
    {
        Inventory warehouse = GameObject.FindWithTag("Warehouse").GetComponent<Inventory>();
        Save(warehouse);
    }*/
    #endregion
    //Public Methods
    #region .
    public void Save(Inventory warehouse)
    {
        SaveInven();
        SaveWarehouse(warehouse);
    }
    public void Load(Inventory Inventory)
    {
        LoadInven();
        LoadWarehouse();
    }
    // ���� Ŭ������ ����Ʈ�� ��Ƽ� �����ϸ� ��ġ ���̺�ó�� ����� �� �ֽ��ϴ�. 
    public void SaveInven()
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();
        Inventory _inventroy = Inventory.GetComponent<Inventory>();
        if (_inventroy != null && _inventroy._Items != null)
        {
            for (int i = 0; i < _inventroy._Items.Length; i++)
            {
                if (_inventroy._Items[i] != null)
                {
                    if (_inventroy._Items[i].Data != null)
                    {
                        int _id = _inventroy._Items[i].Data.ID;
                        if (_inventroy._Items[i] is CountableItem ci)
                        {
                            saveDatas[i].id = _id;
                            saveDatas[i].amount = ci.Amount;
                        }
                        else
                        {
                            saveDatas[i].id = _id;
                            saveDatas[i].amount = 1;
                        }
                    }

                }
                else
                {
                    // ��Ұ� null�� ��� ó���� ���� �߰�
                }
            }
        }
        else
        {
            // inventory �Ǵ� _Items�� null�� ��� ó���� ���� �߰�
        }
        binaryFormatter.Serialize(memoryStream, saveDatas);
        PlayerPrefs.SetString("SaveInven", Convert.ToBase64String(memoryStream.GetBuffer()));
    }
    /// <summary>
    /// â�� ����
    /// </summary>
    /// <param name="warehouse">������ â��</param>
    public void SaveWarehouse(Inventory warehouse)
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();

        // items�� ����Ʈ �迭�� ��ȯ�ؼ� �����մϴ�.
        binaryFormatter.Serialize(memoryStream, warehouse._Items);

        // �װ��� �ٽ� �ѹ� ���ڿ� ������ ��ȯ�ؼ� 
        // 'SaveWarehouse'��� ��Ʈ�� Ű������ PlayerPrefs�� �����մϴ�.
        PlayerPrefs.SetString("SaveWarehouse", Convert.ToBase64String(memoryStream.GetBuffer()));
    }

    public void LoadInven()
    {
        // 'SaveInven' ��Ʈ�� Ű������ �����͸� �����ɴϴ�.

        var data = PlayerPrefs.GetString("SaveInven");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // ������ �����͸� ����Ʈ �迭�� ��ȯ�ϰ�
            // ����ϱ� ���� �ٽ� ����Ʈ�� ĳ�������ݴϴ�.
            saveDatas = (List<SaveData>)binaryFormatter.Deserialize(memoryStream);
            //������DB���� ã�Ƽ� add
            for (int i = 0; i < saveDatas.Count; i++)
            {
                for (int j = 0; j < idb.itemDB.Count; j++)
                {
                    if (saveDatas[i].id == idb.itemDB[j].ID)
                    {
                        ItemData idata = idb.itemDB[j];
                        Inventory _inventory = Inventory.GetComponent<Inventory>();
                        _inventory.Add(idata,saveDatas[i].amount);
                    }
                }
            }
        }

    }
    public Rito.InventorySystem.Item[] LoadWarehouse()
    {
        // 'SaveInven' ��Ʈ�� Ű������ �����͸� �����ɴϴ�.
        var data = PlayerPrefs.GetString("SaveWarehouse");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // ������ �����͸� ����Ʈ �迭�� ��ȯ�ϰ�
            // ����ϱ� ���� �ٽ� ����Ʈ�� ĳ�������ݴϴ�.
            return (Rito.InventorySystem.Item[])binaryFormatter.Deserialize(memoryStream);
        }
        else
            return null;
    }
    #endregion
}
