using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito.InventorySystem;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData
{
    public int amount;
    public int id;
}

public class GameManager : MonoBehaviour
{
    public EquipmentInventory eqinven;
    public ItemDB idb;
    public List<SaveData> saveDatas;
    public List<SaveData> saveEqDatas;
    public List<SaveData> saveWarehouseDatas;
    private static GameManager instance;
    [SerializeField]
    private GameObject Inventory;
    [SerializeField]
    private GameObject? Warehouse;
    public InvenData InvenData;
    public InvenData WarehouseData;
    public InvenData EQData;

    //unitys
    #region .
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //SaveInven();
        //LoadInven();
        if (GameObject.Find("WareHouse") != null)
        {
            Warehouse = GameObject.Find("WareHouse");
        }

        
    }
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
            //LoadInven();
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion
    //Public Methods
    #region .
    public void Load(Inventory inven)
    {
        Debug.Log("인벤 로드");
        if (inven == null)
        {
            Debug.LogError("Inventory is null. Cannot load.");
            return;
        }

        for (int i = 0; i < InvenData.invenitems.Count; i++)
        {
            if (i >= InvenData.invenitems.Count || i >= InvenData.count.Count)
            {
                Debug.LogError("Invalid index for loading inventory data.");
                return;
            }

            inven.Add(InvenData.invenitems[i], InvenData.count[i]);
        }
    }

    public void Save(Inventory inven)
    {
        Debug.Log("인벤 세이브");
        if (inven == null)
        {
            Debug.LogError("Inventory is null. Cannot save.");
            return;
        }

        for (int i = 0; i < inven._Items.Length; i++)
        {
            if (inven._Items[i] == null)
            {
                InvenData.invenitems[i] = null;
                InvenData.count[i] = 0;
            }
            else
            {
                InvenData.invenitems[i] = inven._Items[i].Data;
                if (inven._Items[i] is CountableItem ci)
                {
                    InvenData.count[i] = ci.Amount;
                }
                else
                {
                    InvenData.count[i] = 1;
                }
            }
        }
    }

    public void LoadW(Inventory inven)
    {
        Debug.Log("창고 로드");
        for (int i = 0; i < WarehouseData.invenitems.Count; i++)
        {
            inven.Add(WarehouseData.invenitems[i], WarehouseData.count[i]);
        }
    }
    public void SaveW(Inventory inven)
    {
        Debug.Log("창고 세이브");
        if (inven == null)
        {
            Debug.LogError("Inventory is null. Cannot save.");
            return;
        }

        for (int i = 0; i < inven._Items.Length; i++)
        {
            if (inven._Items[i] == null)
            {
                WarehouseData.invenitems[i] = null;
                WarehouseData.count[i] = 0;
            }
            else
            {
                WarehouseData.invenitems[i] = inven._Items[i].Data;
                if (inven._Items[i] is CountableItem ci)
                {
                    WarehouseData.count[i] = ci.Amount;
                }
                else
                {
                    WarehouseData.count[i] = 1;
                }
            }
        }
    }
    public void LoadEq(EquipmentInventory eqinven)
    {
        Debug.Log("인벤 로드");
        if (eqinven == null)
        {
            Debug.LogError("Inventory is null. Cannot load.");
            return;
        }

        for (int i = 0; i < EQData.invenitems.Count; i++)
        {
            if (i >= EQData.invenitems.Count || i >= EQData.count.Count)
            {
                Debug.LogError("Invalid index for loading inventory data.");
                return;
            }
            if (EQData.invenitems[i] != null)
            {
                eqinven.Add(EQData.invenitems[i]);
            }
            else
            {
                Debug.Log("EQData.invenitems[i] ==null");
            }
        }
    }

    public void SaveEq(EquipmentInventory eqinven)
    {
        Debug.Log("인벤 세이브");
        if (eqinven == null)
        {
            Debug.LogError("Inventory is null. Cannot save.");
            return;
        }

        for (int i = 0; i < eqinven.EqItems.Length; i++)
        {
            if (eqinven.EqItems[i] == null)
            {
                EQData.invenitems[i] = null;
                EQData.count[i] = 0;
            }
            else
            {
                EQData.invenitems[i] = eqinven.EqItems[i].Data;
                if (eqinven.EqItems[i] is CountableItem ci)
                {
                    EQData.count[i] = ci.Amount;
                }
                else
                {
                    EQData.count[i] = 1;
                }
            }
        }
    }
    // 만든 클래스를 리스트에 담아서 관리하면 마치 테이블처럼 사용할 수 있습니다. 
    /*
    public void SaveInven()
    {
        Debug.Log("인벤 세이브");
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
                    // 요소가 null인 경우 처리할 내용 추가
                }
            }
        }
        else
        {
            // inventory 또는 _Items가 null인 경우 처리할 내용 추가
        }
        binaryFormatter.Serialize(memoryStream, saveDatas);
        PlayerPrefs.SetString("SaveInven", Convert.ToBase64String(memoryStream.GetBuffer()));
    }

    public void SaveInven()
    {
        Debug.Log("인벤 세이브");
        Inventory inventory = Inventory.GetComponent<Inventory>();
        if (inventory != null && inventory._Items != null)
        {
            saveDatas.Clear();
            for (int i = 0; i < inventory._Items.Length; i++)
            {
                if (inventory._Items[i] != null && inventory._Items[i].Data != null)
                {
                    SaveData saveData = new SaveData();
                    saveData.id = inventory._Items[i].Data.ID;
                    saveData.amount = (inventory._Items[i] is CountableItem ci) ? ci.Amount : 1;
                    saveDatas.Add(saveData);
                }
            }
        }

        string json = JsonUtility.ToJson(saveDatas);
        PlayerPrefs.SetString("SaveInventory", json);
    }
    /// <summary>
    /// 창고 저장
    /// </summary>
    /// <param name="warehouse">저장할 창고</param>
    public void SaveWarehouse(Inventory warehouse)
    {
        Debug.Log("창고세이브");
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();
        if (warehouse != null && warehouse._Items != null)
        {
            for (int i = 0; i < warehouse._Items.Length; i++)
            {
                if (warehouse._Items[i] != null)
                {
                    if (warehouse._Items[i].Data != null)
                    {
                        int _id = warehouse._Items[i].Data.ID;
                        if (warehouse._Items[i] is CountableItem ci)
                        {
                            saveWarehouseDatas[i].id = _id;
                            saveWarehouseDatas[i].amount = ci.Amount;
                        }
                        else
                        {
                            saveWarehouseDatas[i].id = _id;
                            saveWarehouseDatas[i].amount = 1;
                        }
                    }

                }
                else
                {
                    // 요소가 null인 경우 처리할 내용 추가
                }
            }
        }
        else
        {
            // inventory 또는 _Items가 null인 경우 처리할 내용 추가
        }
        binaryFormatter.Serialize(memoryStream, saveDatas);
        PlayerPrefs.SetString("SaveWarehouse", Convert.ToBase64String(memoryStream.GetBuffer()));
    }
    public void SaveEquipment()
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();
        if (eqinven != null && eqinven.EqItems != null)
        {
            for (int i = 0; i < eqinven.EqItems.Length; i++)
            {
                if (eqinven.EqItems[i] != null)
                {
                    if (eqinven.EqItems[i].Data != null)
                    {
                        int _id = eqinven.EqItems[i].Data.ID;
                        if (eqinven.EqItems[i] is CountableItem ci)
                        {
                            saveEqDatas[i].id = _id;
                            saveEqDatas[i].amount = ci.Amount;
                        }
                        else
                        {
                            saveEqDatas[i].id = _id;
                            saveEqDatas[i].amount = 1;
                        }
                    }

                }
                else
                {
                    // 요소가 null인 경우 처리할 내용 추가
                }
            }
        }
        else
        {
            // inventory 또는 _Items가 null인 경우 처리할 내용 추가
        }
        binaryFormatter.Serialize(memoryStream, saveEqDatas);
        PlayerPrefs.SetString("SaveEquipment", Convert.ToBase64String(memoryStream.GetBuffer()));
    }
    /*
    public void LoadInven()
    {
        // 'SaveInven' 스트링 키값으로 데이터를 가져옵니다.
        Debug.Log("인벤 로드");
        var data = PlayerPrefs.GetString("SaveInven");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // 가져온 데이터를 바이트 배열로 변환하고
            // 사용하기 위해 다시 리스트로 캐스팅해줍니다.
            saveDatas = (List<SaveData>)binaryFormatter.Deserialize(memoryStream);
            //아이템DB에서 찾아서 add
            for (int i = 0; i < saveDatas.Count; i++)
            {
                for (int j = 0; j < idb.itemDB.Count; j++)
                {
                    if (saveDatas[i].id == idb.itemDB[j].ID)
                    {
                        if (idb.itemDB[j].IconSprite != null)
                        {
                            ItemData idata = idb.itemDB[j];
                            Inventory _inventory = Inventory.GetComponent<Inventory>();
                            _inventory.Add(idata, saveDatas[i].amount);
                        }

                    }
                }
            }
        }

    }
    public void Addidb()
    {
        Inventory ti = Inventory.GetComponent<Inventory>();
        ti.Add(idb.itemDB[0], 1);
    }
    public void LoadInven()
    {
        Debug.Log("인벤 로드");
        string json = PlayerPrefs.GetString("SaveInventory");
        if (!string.IsNullOrEmpty(json))
        {
            saveDatas = JsonUtility.FromJson<List<SaveData>>(json);
            ItemData idata;
            // saveDatas를 이용하여 인벤토리 데이터를 복원하는 코드 추가
            for (int i = 0; i < saveDatas.Count; i++)
            {
                for (int j = 0; j < idb.itemDB.Count; j++)
                {
                    if (saveDatas[i].id == idb.itemDB[j].ID)
                    {
                        Debug.Log(idb.itemDB[j].ID);
                        idata = idb.itemDB[j];
                        Inventory _inventory = Inventory.GetComponent<Inventory>();
                        if (_inventory.MaxCapacity < i)
                        {
                            _inventory.Add(idata, saveDatas[i].amount);
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// 창고 로드
    /// </summary>
    /// <param name="warehouse">불러올 창고</param>
    public void LoadWarehouse(Inventory warehouse)
    {
        Debug.Log("창고 로드");
        // 'SaveInven' 스트링 키값으로 데이터를 가져옵니다.
        var data = PlayerPrefs.GetString("SaveWarehouse");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // 가져온 데이터를 바이트 배열로 변환하고
            // 사용하기 위해 다시 리스트로 캐스팅해줍니다.
            saveWarehouseDatas = (List<SaveData>)binaryFormatter.Deserialize(memoryStream);
            //아이템DB에서 찾아서 add
            for (int i = 0; i < saveWarehouseDatas.Count; i++)
            {
                for (int j = 0; j < idb.itemDB.Count; j++)
                {
                    if (saveWarehouseDatas[i].id == idb.itemDB[j].ID)
                    {
                        if (idb.itemDB[j].IconSprite != null)
                        {
                            ItemData idata = idb.itemDB[j];
                            if (warehouse.MaxCapacity < i)
                            {
                                warehouse.Add(idata, saveWarehouseDatas[i].amount);
                            }
                        }
                    }
                }
            }
        }
    }
    public void LoadEquibment(EquipmentInventory eqinven)
    {
        // 'SaveInven' 스트링 키값으로 데이터를 가져옵니다.
        var data = PlayerPrefs.GetString("SaveEquipment");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // 가져온 데이터를 바이트 배열로 변환하고
            // 사용하기 위해 다시 리스트로 캐스팅해줍니다.
            saveEqDatas = (List<SaveData>)binaryFormatter.Deserialize(memoryStream);
            //아이템DB에서 찾아서 add
            for (int i = 0; i < saveEqDatas.Count; i++)
            {
                for (int j = 0; j < idb.itemDB.Count; j++)
                {
                    if (saveEqDatas[i].id == idb.itemDB[j].ID)
                    {
                        ItemData idata = idb.itemDB[j];
                        eqinven.Add(idata);

                    }
                }
            }
        }
    }*/
    #endregion
}
