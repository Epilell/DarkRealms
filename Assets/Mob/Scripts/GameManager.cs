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
        Debug.Log("�κ� �ε�");
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
        Debug.Log("�κ� ���̺�");
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
        Debug.Log("â�� �ε�");
        for (int i = 0; i < WarehouseData.invenitems.Count; i++)
        {
            inven.Add(WarehouseData.invenitems[i], WarehouseData.count[i]);
        }
    }
    public void SaveW(Inventory inven)
    {
        Debug.Log("â�� ���̺�");
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
        Debug.Log("�κ� �ε�");
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
        Debug.Log("�κ� ���̺�");
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
    // ���� Ŭ������ ����Ʈ�� ��Ƽ� �����ϸ� ��ġ ���̺�ó�� ����� �� �ֽ��ϴ�. 
    /*
    public void SaveInven()
    {
        Debug.Log("�κ� ���̺�");
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

    public void SaveInven()
    {
        Debug.Log("�κ� ���̺�");
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
    /// â�� ����
    /// </summary>
    /// <param name="warehouse">������ â��</param>
    public void SaveWarehouse(Inventory warehouse)
    {
        Debug.Log("â���̺�");
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
                    // ��Ұ� null�� ��� ó���� ���� �߰�
                }
            }
        }
        else
        {
            // inventory �Ǵ� _Items�� null�� ��� ó���� ���� �߰�
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
                    // ��Ұ� null�� ��� ó���� ���� �߰�
                }
            }
        }
        else
        {
            // inventory �Ǵ� _Items�� null�� ��� ó���� ���� �߰�
        }
        binaryFormatter.Serialize(memoryStream, saveEqDatas);
        PlayerPrefs.SetString("SaveEquipment", Convert.ToBase64String(memoryStream.GetBuffer()));
    }
    /*
    public void LoadInven()
    {
        // 'SaveInven' ��Ʈ�� Ű������ �����͸� �����ɴϴ�.
        Debug.Log("�κ� �ε�");
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
        Debug.Log("�κ� �ε�");
        string json = PlayerPrefs.GetString("SaveInventory");
        if (!string.IsNullOrEmpty(json))
        {
            saveDatas = JsonUtility.FromJson<List<SaveData>>(json);
            ItemData idata;
            // saveDatas�� �̿��Ͽ� �κ��丮 �����͸� �����ϴ� �ڵ� �߰�
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
    /// â�� �ε�
    /// </summary>
    /// <param name="warehouse">�ҷ��� â��</param>
    public void LoadWarehouse(Inventory warehouse)
    {
        Debug.Log("â�� �ε�");
        // 'SaveInven' ��Ʈ�� Ű������ �����͸� �����ɴϴ�.
        var data = PlayerPrefs.GetString("SaveWarehouse");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // ������ �����͸� ����Ʈ �迭�� ��ȯ�ϰ�
            // ����ϱ� ���� �ٽ� ����Ʈ�� ĳ�������ݴϴ�.
            saveWarehouseDatas = (List<SaveData>)binaryFormatter.Deserialize(memoryStream);
            //������DB���� ã�Ƽ� add
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
        // 'SaveInven' ��Ʈ�� Ű������ �����͸� �����ɴϴ�.
        var data = PlayerPrefs.GetString("SaveEquipment");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // ������ �����͸� ����Ʈ �迭�� ��ȯ�ϰ�
            // ����ϱ� ���� �ٽ� ����Ʈ�� ĳ�������ݴϴ�.
            saveEqDatas = (List<SaveData>)binaryFormatter.Deserialize(memoryStream);
            //������DB���� ã�Ƽ� add
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
