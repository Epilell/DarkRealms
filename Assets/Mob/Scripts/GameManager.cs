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
    private GameObject Warehouse;
    //singelton
    #region .
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
    }
    #endregion
    //unitys
    #region .
    private void Awake()
    {
        //Load(Inventory.GetComponent<Inventory>());
    }
    private void Start()
    {
        DontDestroyOnLoad(Inventory);
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(Warehouse);
    }
    /*public void OnDestroy()
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

        // items�� ����Ʈ �迭�� ��ȯ�ؼ� �����մϴ�.
        binaryFormatter.Serialize(memoryStream, Inventory.GetComponent<Inventory>()._Items);

        // �װ��� �ٽ� �ѹ� ���ڿ� ������ ��ȯ�ؼ� 
        // 'SaveInven'��� ��Ʈ�� Ű������ PlayerPrefs�� �����մϴ�.
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

    public void LoadInven(Inventory _inventory)
    {
        // 'SaveInven' ��Ʈ�� Ű������ �����͸� �����ɴϴ�.
        var data = PlayerPrefs.GetString("SaveInven");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // ������ �����͸� ����Ʈ �迭�� ��ȯ�ϰ�
            // ����ϱ� ���� �ٽ� ����Ʈ�� ĳ�������ݴϴ�.
            Rito.InventorySystem.Item[] itemN = (Rito.InventorySystem.Item[])binaryFormatter.Deserialize(memoryStream);
            _inventory.SetItems(itemN);
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
