using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField]
    private GameObject Inventory;
    [SerializeField]
    private GameObject? Warehouse;
    //singelton
    #region .
    /*
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }*/
    #endregion
    //unitys
    #region .

    private void Awake()
    {
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
        LoadInven(Inventory);
        LoadWarehouse();
    }
    // ���� Ŭ������ ����Ʈ�� ��Ƽ� �����ϸ� ��ġ ���̺�ó�� ����� �� �ֽ��ϴ�. 
    public void SaveInven()
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();
        for (int i = 0; i < Inventory.GetComponent<Inventory>()._Items.Length; i++)
        {
            // items�� ����Ʈ �迭�� ��ȯ�ؼ� �����մϴ�.
            binaryFormatter.Serialize(memoryStream, Inventory.GetComponent<Inventory>()._Items[i].Data);

            // �װ��� �ٽ� �ѹ� ���ڿ� ������ ��ȯ�ؼ� 
            // 'SaveInven'��� ��Ʈ�� Ű������ PlayerPrefs�� �����մϴ�.
            PlayerPrefs.SetString("SaveInven" + i, Convert.ToBase64String(memoryStream.GetBuffer()));
        }
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

    public void LoadInven(Inventory _inventory)
    {
        // 'SaveInven' ��Ʈ�� Ű������ �����͸� �����ɴϴ�.
        int Invenleanth = 0;
        var invendata = ("null");
        for (int i = 0; i < 300; i++)
        {
            if (invendata != null)
            {
                invendata = PlayerPrefs.GetString("SaveInven" + i);
                Invenleanth++;
            }
            else
            {
                i = 300;
            }
        }
        for (int i = 0; i < Invenleanth; i++)
        {

            var data = PlayerPrefs.GetString("SaveInven" + i);
            if (!string.IsNullOrEmpty(data))
            {
                var binaryFormatter = new BinaryFormatter();
                var memoryStream = new MemoryStream(Convert.FromBase64String(data));

                // ������ �����͸� ����Ʈ �迭�� ��ȯ�ϰ�
                // ����ϱ� ���� �ٽ� ����Ʈ�� ĳ�������ݴϴ�.
                Rito.InventorySystem.ItemData itemN = (Rito.InventorySystem.ItemData)binaryFormatter.Deserialize(memoryStream);

                _inventory.Add(itemN);

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
