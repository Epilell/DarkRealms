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
    private static GameManager instance;
    [SerializeField]
    private GameObject Inventory;
    [SerializeField]
    private GameObject? Warehouse;

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
        SaveInven();
    }
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
        SaveEquipment();
    }
    public void Load(Inventory warehouse, EquipmentInventory eqinven)
    {
        LoadInven();
        LoadWarehouse(warehouse);
        LoadEquibment(eqinven);
    }
    // 만든 클래스를 리스트에 담아서 관리하면 마치 테이블처럼 사용할 수 있습니다. 
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
    /// <summary>
    /// 창고 저장
    /// </summary>
    /// <param name="warehouse">저장할 창고</param>
    public void SaveWarehouse(Inventory warehouse)
    {
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

    public void LoadInven()
    {
        // 'SaveInven' 스트링 키값으로 데이터를 가져옵니다.

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
                        ItemData idata = idb.itemDB[j];
                        Inventory _inventory = Inventory.GetComponent<Inventory>();
                        _inventory.Add(idata,saveDatas[i].amount);
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
        // 'SaveInven' 스트링 키값으로 데이터를 가져옵니다.
        var data = PlayerPrefs.GetString("SaveWarehouse");
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
                        ItemData idata = idb.itemDB[j];
                        warehouse.Add(idata, saveDatas[i].amount);
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
    }
    #endregion
}
