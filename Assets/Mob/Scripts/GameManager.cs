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
    // 만든 클래스를 리스트에 담아서 관리하면 마치 테이블처럼 사용할 수 있습니다. 
    public void SaveInven()
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();
        for (int i = 0; i < Inventory.GetComponent<Inventory>()._Items.Length; i++)
        {
            // items를 바이트 배열로 변환해서 저장합니다.
            binaryFormatter.Serialize(memoryStream, Inventory.GetComponent<Inventory>()._Items[i].Data);

            // 그것을 다시 한번 문자열 값으로 변환해서 
            // 'SaveInven'라는 스트링 키값으로 PlayerPrefs에 저장합니다.
            PlayerPrefs.SetString("SaveInven" + i, Convert.ToBase64String(memoryStream.GetBuffer()));
        }
    }
    /// <summary>
    /// 창고 저장
    /// </summary>
    /// <param name="warehouse">저장할 창고</param>
    public void SaveWarehouse(Inventory warehouse)
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();

        // items를 바이트 배열로 변환해서 저장합니다.
        binaryFormatter.Serialize(memoryStream, warehouse._Items);

        // 그것을 다시 한번 문자열 값으로 변환해서 
        // 'SaveWarehouse'라는 스트링 키값으로 PlayerPrefs에 저장합니다.
        PlayerPrefs.SetString("SaveWarehouse", Convert.ToBase64String(memoryStream.GetBuffer()));
    }

    public void LoadInven(Inventory _inventory)
    {
        // 'SaveInven' 스트링 키값으로 데이터를 가져옵니다.
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

                // 가져온 데이터를 바이트 배열로 변환하고
                // 사용하기 위해 다시 리스트로 캐스팅해줍니다.
                Rito.InventorySystem.ItemData itemN = (Rito.InventorySystem.ItemData)binaryFormatter.Deserialize(memoryStream);

                _inventory.Add(itemN);

            }
        }

    }
    public Rito.InventorySystem.Item[] LoadWarehouse()
    {
        // 'SaveInven' 스트링 키값으로 데이터를 가져옵니다.
        var data = PlayerPrefs.GetString("SaveWarehouse");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // 가져온 데이터를 바이트 배열로 변환하고
            // 사용하기 위해 다시 리스트로 캐스팅해줍니다.
            return (Rito.InventorySystem.Item[])binaryFormatter.Deserialize(memoryStream);
        }
        else
            return null;
    }
    #endregion
}
