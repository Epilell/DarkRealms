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
    /// <summary>
    /// â�� �ε�
    /// </summary>
    /// <param name="warehouse">�ҷ��� â��</param>
    public void LoadWarehouse(Inventory warehouse)
    {
        // 'SaveInven' ��Ʈ�� Ű������ �����͸� �����ɴϴ�.
        var data = PlayerPrefs.GetString("SaveWarehouse");
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
                        warehouse.Add(idata, saveDatas[i].amount);
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
    }
    #endregion
}
